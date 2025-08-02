using System;
using System.Runtime.InteropServices;

namespace Eduegate.ERP.Admin.Helpers
{
    public static class WkHtmlToPdfLoader
    {
        public static void LoadLibrary()
        {
            string libraryPath = GetLibraryPath();

            if (!string.IsNullOrEmpty(libraryPath))
            {
                // Load the library using P/Invoke
                NativeLibrary.Load(libraryPath);
            }
            else
            {
                throw new Exception("WkHtmlToPdf library not found for this OS or architecture.");
            }
        }

        private static string GetLibraryPath()
        {
            string basePath = Environment.Is64BitOperatingSystem ?
                Path.Combine(AppContext.BaseDirectory, "Libs", "x64") : // 64-bit version
                Path.Combine(AppContext.BaseDirectory, "Libs", "x86"); // 32-bit version

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Path.Combine(basePath, "libwkhtmltox.dll");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return Path.Combine(basePath, "libwkhtmltox.so");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return Path.Combine(basePath, "libwkhtmltox.dylib");
            }

            return null; // Unsupported OS
        }

    }
}