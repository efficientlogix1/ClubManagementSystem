using DAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;


namespace BLL
{
    public static class Extension
    {
        public static string FetchUniquePath(this string directoryPath, string imageName)
        {
            string extension = Path.GetExtension(imageName);
            string fileName = DateTime.UtcNow.Ticks.ToString();

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            int i = 0;
            while (File.Exists(directoryPath + "/" + fileName + i + extension))
                i++;

            return (fileName + i + extension);
        }
    };
}
