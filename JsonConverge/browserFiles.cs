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
    class BrowserFiles
    {
        JsonFile file;
        List<TabResults> tabResults;

        public BrowserFiles()
        {
            file = new JsonFile();
            tabResults = new List<TabResults>();
            createFile();
        }
        public List<TabResults> createFile()
        {
            int newMsg;
            for (int i = 1; i <= 10; i++)
            {
                file = JObject.Parse(File.ReadAllText(@"C:\Users\Fouby\Desktop\ManonCourreges_w_aJxFlVQg\message_" + i+".json")).ToObject<JsonFile>();
                if (tabResults.Count == 0)
                {
                    foreach (var item in file.participants)
                    {
                        tabResults.Add(new TabResults { nom = item.name, nbMessages = 0 });
                    }
                }
                foreach (var participant in file.participants)
                {
                    newMsg = file.messages.Where(m => m.sender_name == participant.name && m.timestamp_ms > (long)1577982612000 && m.content != null).Count();
                    tabResults.FirstOrDefault(t => t.nom == participant.name).nbMessages += newMsg;
                }
            }
            Console.WriteLine("Nombre total : ");
            foreach (var item in tabResults.OrderByDescending(x => x.nbMessages))
            {
                item.nom = item.nom.Replace("Ã©", "é");
                Console.WriteLine(item.nom + ":" + item.nbMessages);
            }
            return tabResults;
        }
    }

    public class TabResults
    {
        public string nom { get; set; }
        public int nbMessages { get; set; }
    }
}
