using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WcfClientService.DespatchService.EFinans
{
    enum BelgeNoTipleri
    {
        YEREL,
        OID,
        ETTN
    }

    public class EFinansTools
    {
        public Dictionary<string, byte[]> Decompress(byte[] fileData)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Stream targFileStream = new MemoryStream(fileData);

            Dictionary<string, byte[]> files = new Dictionary<string, byte[]>();

            using (ZipFile z = ZipFile.Read(targFileStream))
            {
                foreach (ZipEntry zEntry in z)
                {
                    MemoryStream tempS = new MemoryStream();
                    zEntry.Extract(tempS);

                    files.Add(zEntry.FileName, tempS.ToArray());
                }
            }

            return files;
        }

        public DateTime TextToDate(string date)
        {
            return DateTime.Now;
        }

        public string DateToText(DateTime date)
        {
            string textDate = "";

            textDate += date.Year.ToString();
            textDate += date.Month <= 9 ? "0" + date.Month.ToString() : date.Month.ToString();
            textDate += date.Day <= 9 ? "0" + date.Day.ToString() : date.Day.ToString();

            return textDate;
        }

        public string GetMD5Hash(byte[] gelen)
        {
            MD5 md5Hash = new MD5CryptoServiceProvider();
            byte[] data = md5Hash.ComputeHash(gelen);
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
