using DelUserApp.Properties;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DelUserApp
{
    public static class LocalStorageManagement
    {
        public static string Protect(string data)
        {
            if (string.IsNullOrEmpty(data)) return data;
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(data);
            byte[] cipherTextBytes = ProtectedData.Protect(plainTextBytes, null, DataProtectionScope.LocalMachine);
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string UnProtect(string data)
        {
            if (string.IsNullOrEmpty(data)) return data;
            byte[] cipherTextBytes = Convert.FromBase64String(data);
            byte[] plainTextBytes = ProtectedData.Unprotect(cipherTextBytes, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(plainTextBytes);
        }

    }
}
