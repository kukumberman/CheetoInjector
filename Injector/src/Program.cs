using System;
using System.Diagnostics;
using System.Linq;
using System.IO;
using Cucumba.Cheeto.Injection;

namespace Cucumba.Cheeto
{
    class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Invalid arguments");
                Console.ReadKey();
                return;
            }

            var processName = args[0];
            if (processName.EndsWith(".exe"))
            {
                processName = processName.Replace(".exe", "");
            }

            var process = Process.GetProcessesByName(processName).FirstOrDefault();
            if (process == null)
            {
                Console.WriteLine("[-] Process not found");
                Console.ReadKey();
                return;
            }

            var dllPath = args[1];
            dllPath = Path.GetFullPath(dllPath);

            if (!File.Exists(dllPath))
            {
                Console.WriteLine("[-] File not found");
                Console.ReadKey();
                return;
            }

            try
            {
                Injector.InjectViaLoadLibrary(process.Id, dllPath);
                Console.WriteLine("[+] Successfully injected!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Oops! Something went wrong");
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
