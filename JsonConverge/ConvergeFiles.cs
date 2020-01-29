using Newtonsoft.Json;
using Newtonsoft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace JsonConverge
{
    class ConvergeFiles
    {
        private JsonFile partialFile;
        private JsonFile file;
        private List<TabResults> tabResults;

        public ConvergeFiles()
        {
            file = new JsonFile();
            partialFile = new JsonFile();
            tabResults = new List<TabResults>();
        }

      private string DecodeString(string text)
        {
            Encoding targetEncoding = Encoding.GetEncoding("ISO-8859-1");
            var unescapeText = Regex.Unescape(text);
            return  Encoding.UTF8.GetString(targetEncoding.GetBytes(unescapeText));            
        }

        static string EncodeNonAsciiCharacters(string value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in value)
            {
                if (c > 127)
                {
                    string encodedValue = "\\u" + ((int)c).ToString("x4");
                    sb.Append(encodedValue);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
      
        public JsonFile createObject(List<string> pathList)
        {
            foreach (var path in pathList)
            {
                string p = File.ReadAllText(@path, Encoding.UTF8);
                partialFile = JsonConvert.DeserializeObject<JsonFile>(p);
                if (file.messages is null) file = partialFile;
                else file.messages.AddRange(partialFile.messages);
            }



            file.messages.RemoveAll(m => m.timestamp_ms < 1501300800000);

            return file;
        }
        

        //export the json file to be used by another program
        public void exportJson(string user)
        {
            string path = @"C:/Users/" + user + "/Documents/StatsMessengerJson/messages.json";
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            //@"\u" + ((int)34).ToString("x4")
            file.messages.Where(m => m.content != null).ToList().ForEach(m => m.content = m.content.Replace("\n"," ")
                                                                                                    .Replace(@"\", @"\\")
                                                                                                    .Replace(@"""", @"\"""));
            string json = JsonConvert.SerializeObject(file, Formatting.Indented, settings);
            if (File.Exists(path)) File.Delete(path);
            File.WriteAllText(path, EncodeNonAsciiCharacters(DecodeString(json)));
            Console.WriteLine("All done !");
        }
    }

    public class TabResults
    {
        public string nom { get; set; }
        public int nbMessages { get; set; }
    }
}
