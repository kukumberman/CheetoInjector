using System;
using Cucumba.Cheeto.Native;

namespace Cucumba.Cheeto.Injection
{
    class Injector
    {
        public static bool InjectViaLoadLibrary(int processId, string dllPath)
        {
            var dllPathBytes = System.Text.Encoding.ASCII.GetBytes(dllPath);

            var flags = Kernel32.PROCESS_QUERY_INFORMATION 
                | Kernel32.PROCESS_CREATE_THREAD 
                | Kernel32.PROCESS_VM_OPERATION 
                | Kernel32.PROCESS_VM_WRITE 
                | Kernel32.PROCESS_VM_READ;
            
            var processHandle = Kernel32.OpenProcess(flags, false, processId);
            if (processHandle == IntPtr.Zero)
            {
                Console.WriteLine("[-] OpenProcess");
                return false;
            }

            var allocatedAddress = Kernel32.VirtualAllocEx(processHandle, IntPtr.Zero, dllPathBytes.Length, Kernel32.MEM_COMMIT | Kernel32.MEM_RESERVE, Kernel32.PAGE_READWRITE);
            if (allocatedAddress == IntPtr.Zero)
            {
                Console.WriteLine("[-] VirtualAllocEx");
                return false;
            }

            if (!Kernel32.WriteProcessMemory(processHandle, allocatedAddress, dllPathBytes, dllPathBytes.Length, out var _))
            {
                Console.WriteLine("[-] WriteProcessMemory");
                return false;
            }

            var addressOfLoadLibrary = Kernel32.GetProcAddress(Kernel32.GetModuleHandle(Kernel32.DLL_NAME), "LoadLibraryA");

            var threadHandle = Kernel32.CreateRemoteThread(processHandle, IntPtr.Zero, 0, addressOfLoadLibrary, allocatedAddress, 0, IntPtr.Zero);
            if (threadHandle == IntPtr.Zero)
            {
                Console.WriteLine("[-] CreateRemoteThread");
                return false;
            }

            const uint INFINITE = uint.MaxValue;
            Kernel32.WaitForSingleObject(threadHandle, INFINITE);

            Kernel32.CloseHandle(threadHandle);

            if (!Kernel32.VirtualFreeEx(processHandle, allocatedAddress, 0, Kernel32.MEM_RELEASE))
            {
                Console.WriteLine("[-] VirtualFreeEx");
                return false;
            }

            Kernel32.CloseHandle(processHandle);

            return true;
        }
    }
}
