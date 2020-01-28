using Newtonsoft.Json;
using Newtonsoft;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

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
            var unescapeText = System.Text.RegularExpressions.Regex.Unescape(text);
            return Encoding.UTF8.GetString(targetEncoding.GetBytes(unescapeText));
        }

        public JsonFile createObject(List<string> pathList)
        {
            foreach (var path in pathList)
            {
                string p = File.ReadAllText(@path);
                

                partialFile = JsonConvert.DeserializeObject<JsonFile>(p);
                if (file.messages is null) file = partialFile;
                else file.messages.AddRange(partialFile.messages);
            }

            file.messages.RemoveAll(m => m.timestamp_ms < 1501300800000);

            return file;
        }

        public void exportJson(string user)
        {
            string path = @"C:/Users/" + user + "/Documents/StatsMessengerJson/messages.json";
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            ;
            string json = JsonConvert.SerializeObject(file, Formatting.Indented, settings);
            if (File.Exists(path)) File.Delete(path);
            File.WriteAllText(path, DecodeString(json));
            Console.WriteLine("All done !");
        }
    }

    public class TabResults
    {
        public string nom { get; set; }
        public int nbMessages { get; set; }
    }
}
