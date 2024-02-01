using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace FastGithub
{
    static class ConsoleUtil
    {
        private const uint ENABLE_QUICK_EDIT = 0x0040;

        private const int STD_INPUT_HANDLE = -10;

        [DllImport("kernel32.dll", SetLastError = true)]
        [SupportedOSPlatform("windows")]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        [SupportedOSPlatform("windows")]
        private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

        [DllImport("kernel32.dll", SetLastError = true)]
        [SupportedOSPlatform("windows")]
        private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

        /// <summary>
        /// 禁用快速编辑模式
        /// </summary>
        /// <returns></returns>
        public static bool DisableQuickEdit()
        {
            if (OperatingSystem.IsWindows())
            {
                var hwnd = GetStdHandle(STD_INPUT_HANDLE);
                if (GetConsoleMode(hwnd, out uint mode))
                {
                    mode &= ~ENABLE_QUICK_EDIT;
                    return SetConsoleMode(hwnd, mode);
                }
            }

            return false;
        }
        public static void CheckQuickEditMode()
        {

            var hwnd = GetStdHandle(STD_INPUT_HANDLE);
            uint mode;
            if (!GetConsoleMode(hwnd, out mode))
            {
                // 获取控制台窗口模式失败
            }
            else if ((mode & ENABLE_QUICK_EDIT) != 0)
            {
                // 控制台窗口被其他进程重新打开
            }

            //var hwnd = GetStdHandle(STD_INPUT_HANDLE);
            //uint mode;
            //if (GetConsoleMode(hwnd, out mode))
            //{
            //    if ((mode & ENABLE_QUICK_EDIT) != 0)
            //    {
            //        // 控制台窗口被其他进程重新打开
            //    }
            //}
        }

    }
}
