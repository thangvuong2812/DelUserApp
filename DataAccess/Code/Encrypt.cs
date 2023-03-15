using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Code
{
    public class Encrypt
    {
        public static string EncryptPass (string key, string inputText)
        {
            //if ((uint)Microsoft.VisualBasic.CompilerServices.Operators.CompareString(key, "", false) <= 0U)
            //    return inputText;
            //string str = Strings.Len(key) >= 16 ? String.Left(key, 16) : key + Strings.Left("XXXXXXXXXXXXXXXX", checked(16 - Strings.Len(key)));
            string str = key.Length >= 16 ? key.Substring(0, 16) : key + ("XXXXXXXXXXXXXXXX").Substring(0, (16 - key.Length));
            //byte[] bytes1 = Encoding.UTF8.GetBytes(String.Left(str, 8));
            //byte[] bytes2 = Encoding.UTF8.GetBytes(String.Right(str, 8));
            byte[] bytes1 = Encoding.UTF8.GetBytes(str.Substring(0, 8));
            byte[] bytes2 = Encoding.UTF8.GetBytes(str.Substring(str.Length - 8, 8));
            byte[] bytes3 = Encoding.UTF8.GetBytes(inputText);
            DESCryptoServiceProvider cryptoServiceProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(bytes1, bytes2), CryptoStreamMode.Write);
            cryptoStream.Write(bytes3, 0, bytes3.Length);
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }
}
