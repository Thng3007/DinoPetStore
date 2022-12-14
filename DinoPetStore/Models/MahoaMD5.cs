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
        public static String GetMD5(String password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] mahoamd5;
            UTF8Encoding encode = new UTF8Encoding();
            mahoamd5 = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder data = new StringBuilder();
            for (int i = 0; i < mahoamd5.Length; i++)
            {
                data.Append(mahoamd5[i].ToString("x2"));
            }
            return data.ToString();
        }





    }
}