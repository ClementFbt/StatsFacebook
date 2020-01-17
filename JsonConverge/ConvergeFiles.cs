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

        public List<TabResults> createObject(List<string> pathList)
        {
            int newMsg;
            foreach (var path in pathList)
            {
                partialFile = JsonConvert.DeserializeObject<JsonFile>(File.ReadAllText(@path));
                if (file.participants is null) file = partialFile;
                else file.messages.AddRange(partialFile.messages);
            }

            foreach (var participant in file.participants)
            {
                if (tabResults.Exists(t => t.nom == participant.name) is false) tabResults.Add(new TabResults { nom = participant.name, nbMessages = 0 });
                newMsg = file.messages.Where(m => m.sender_name == participant.name).Count();
                tabResults.FirstOrDefault(t => t.nom == participant.name).nbMessages += newMsg;
            }
            return tabResults;
        }

        public void displayResults()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Nombre total de message par personne : ");
            Console.WriteLine("---------------------------------------");
            foreach (var item in tabResults.OrderByDescending(x => x.nbMessages))
            {
                item.nom = Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.nom));
                Console.WriteLine(item.nom + " : " + item.nbMessages);
            }
            Console.WriteLine("---------------------------------------");
            int choix = 0;
            if (choix.Equals(1))
            {
                exportJson();
            }
        }

        public void exportJson()
        {
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            string file2 = JsonConvert.SerializeObject(file, settings);
        }
    }

    public class TabResults
    {
        public string nom { get; set; }
        public int nbMessages { get; set; }
    }
}
