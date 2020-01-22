using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public JsonFile createObject(List<string> pathList)
        {
            foreach (var path in pathList)
            {
                partialFile = JsonConvert.DeserializeObject<JsonFile>(File.ReadAllText(@path));
                if (file.participants is null) file = partialFile;
                else file.messages.AddRange(partialFile.messages);
            }

            file.messages.RemoveAll(m => m.timestamp_ms < 1501300800000);

            return file;
        }

        public void exportJson(string user)
        {
            string path = @"C:/Users/" + user + "/Documents/StatsMessengerJson/messages.json";
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            string json = JsonConvert.SerializeObject(file, Formatting.Indented, settings);
            if (File.Exists(path)) File.Delete(path);
            File.WriteAllText(path, json);
            Console.WriteLine("All done !");
        }
    }

    public class TabResults
    {
        public string nom { get; set; }
        public int nbMessages { get; set; }
    }
}
