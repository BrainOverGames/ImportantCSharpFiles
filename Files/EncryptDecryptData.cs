using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

public class EncryptDecryptData : MonoBehaviour
{
    static readonly string strPassword = "IamZETOwow!123";
    static readonly string strSalt = "IvmD123A12";

    public static void SetString(string key, string value)
    {
        PlayerPrefs.SetString(EncryptData(key, strPassword), EncryptData(value, strPassword));
    }

    public static string GetString(string key)
    {
        string strEncryptValue = GetRowString(key);
        return DecryptData(strEncryptValue, strPassword);
    }

    private static string GetRowString(string key)
    {
        string strEncryptKey = EncryptData(key, strPassword);
        string strEncryptValue = PlayerPrefs.GetString(strEncryptKey);
        return strEncryptValue;
    }

    static byte[] GetByteData()
    {
        byte[] byteData = Encoding.UTF8.GetBytes(strSalt);
        return byteData;
    }

    public static string EncryptData(string strPlain, string password)
    {
        try
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, GetByteData(), 555);

            byte[] key = rfc2898DeriveBytes.GetBytes(8);

            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(key, GetByteData()), CryptoStreamMode.Write))
            {
                memoryStream.Write(GetByteData(), 0, GetByteData().Length);
                byte[] plainTextByte = Encoding.UTF8.GetBytes(strPlain);
                cryptoStream.Write(plainTextByte, 0, plainTextByte.Length);
                cryptoStream.FlushFinalBlock();

                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Encrypt Exception: " + e);
            return strPlain;
        }
    }

    public static string DecryptData(string strEncrypt, string password)
    {
        try
        {
            byte[] cipherBytes = Convert.FromBase64String(strEncrypt);

            using (var memoryStream = new MemoryStream(cipherBytes))
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();

                memoryStream.Read(GetByteData(), 0, GetByteData().Length);

                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, GetByteData(), 555);

                byte[] key = rfc2898DeriveBytes.GetBytes(8);

                using (var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(key, GetByteData()), CryptoStreamMode.Read))
                using (var streamReader = new StreamReader(cryptoStream))
                {
                    string strPlain = streamReader.ReadToEnd();
                    return strPlain;
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogWarning("decrypt Exception: " + e);
            return strEncrypt;
        }
    }
}
