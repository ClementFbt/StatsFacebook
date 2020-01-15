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
        }
        public List<TabResults> createObject(string path)
        {
            int newMsg;
            for (int i = 1; i <= 10; i++)
            {
                file = JObject.Parse(File.ReadAllText(@"" + path + "message_" + i + ".json")).ToObject<JsonFile>();

                foreach (var participant in file.participants)
                {
                    if (tabResults.Exists(t => t.nom == participant.name) is false) tabResults.Add(new TabResults { nom = participant.name, nbMessages = 0 });
                    newMsg = file.messages.Where(m => m.sender_name == participant.name).Count();
                    tabResults.FirstOrDefault(t => t.nom == participant.name).nbMessages += newMsg;
                }
            }
            return tabResults;
        }

        public void displayResults(string path)
        {
            List<TabResults> resultats = createObject(path);
            Console.WriteLine("Nombre total : ");
            foreach (var item in tabResults.OrderByDescending(x => x.nbMessages))
            {
                item.nom = item.nom.Replace("Ã©", "é");
                Console.WriteLine(item.nom + ":" + item.nbMessages);
            }
        }
    }

    public class TabResults
    {
        public string nom { get; set; }
        public int nbMessages { get; set; }
    }
}
