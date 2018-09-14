using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace DXRFID.Class
{
    public class MD5_Encoding
    {
        public string pwd;
        public string Encoding()
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] pwds = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pwd));
            string pwd_encoding = "";
            foreach (byte pwd_en in pwds)
            {
                pwd_encoding += pwd_en;
            }
            return pwd_encoding;
        }
    }
}