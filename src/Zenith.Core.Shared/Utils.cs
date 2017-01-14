using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Core.Shared
{
    public static class Utils
    {
        public static string ConvertToUnsecureString(SecureString securePassword)
        {
            if (securePassword == null)
            {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;

            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static int SleepTime(DateTime now)
        {
            List<int> seconds = new List<int>() { 10, 20, 30, 40, 50, 60 };

            int next = seconds.Find(x => x > now.Second);

            return next - now.Second;
        }
    }
}
