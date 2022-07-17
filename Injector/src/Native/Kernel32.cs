using System;
using System.Runtime.InteropServices;

namespace Cucumba.Cheeto.Native
{
    static class Kernel32
    {
        public const string DLL_NAME = "kernel32.dll";

        public const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        public const int PROCESS_QUERY_INFORMATION = 0x400;
        public const int PROCESS_CREATE_THREAD = 0x2;
        public const int PROCESS_VM_OPERATION = 0x8;
        public const int PROCESS_VM_READ = 0x10;
        public const int PROCESS_VM_WRITE = 0x20;

        public const int MEM_COMMIT = 0x1000;
        public const int MEM_RESERVE = 0x2000;
        public const int MEM_RELEASE = 0x8000;
        public const int PAGE_READWRITE = 0x4;

        [DllImport(DLL_NAME)]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport(DLL_NAME)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport(DLL_NAME)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport(DLL_NAME)]
        public static extern IntPtr VirtualAllocEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            int dwSize,
            int flAllocationType,
            int flProtect
        );

        [DllImport(DLL_NAME)]
        public static extern bool VirtualFreeEx(
            IntPtr hProcess,
            IntPtr lpAddress,
            int dwSize,
            int dwFreeType
        );

        [DllImport(DLL_NAME)]
        public static extern IntPtr CreateRemoteThread(
            IntPtr hProcess,
            IntPtr lpThreadAttributes,
            int dwStackSize,
            IntPtr lpStartAddress,
            IntPtr lpParameter,
            int dwCreationFlags,
            IntPtr lpThreadId
        );

        [DllImport(DLL_NAME)]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport(DLL_NAME)]
        public static extern bool ReadProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [Out, MarshalAs(UnmanagedType.AsAny)] object lpBuffer,
            int dwSize,
            out int lpNumberOfBytesRead
        );

        [DllImport(DLL_NAME)]
        public static extern bool WriteProcessMemory(
            IntPtr hProcess,
            IntPtr lpBaseAddress,
            [MarshalAs(UnmanagedType.AsAny)] object lpBuffer,
            int dwSize,
            out int lpNumberOfBytesWritten
        );

        [DllImport(DLL_NAME)]
        public static extern int WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);
    }
}
