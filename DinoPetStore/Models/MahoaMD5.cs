using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using DinoPetStore.EF;
using Azure.Core.GeoJson;
using System.Windows.Input;

namespace DinoPetStore.Models
{
    public class MahoaMD5
    {
        //public static String GetMD5(String password)
        //{
        //    MD5 md5 = new MD5CryptoServiceProvider();
        //    byte[] mahoamd5;
        //    UTF8Encoding encode = new UTF8Encoding();
        //    mahoamd5 = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
        //    StringBuilder data = new StringBuilder();
        //    for (int i = 0; i < mahoamd5.Length; i++)
        //    {
        //        data.Append(mahoamd5[i].ToString("x2"));
        //    }
        //    return data.ToString();
        //}
        //

        public static String EncryptMD5(String password)
        {
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(password);
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(password));
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }


        public static String DecryptMD5(String password)
        {

            byte[] toEncryptArray = Convert.FromBase64String(password);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(password));

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }


    }
}