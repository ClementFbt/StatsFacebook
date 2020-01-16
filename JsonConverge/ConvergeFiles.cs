﻿using Newtonsoft.Json;
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
        JsonFile file;
        List<TabResults> tabResults;

        public ConvergeFiles()
        {
            file = new JsonFile();
            tabResults = new List<TabResults>();
        }

        public List<TabResults> createObject(string path)
        {
            int newMsg;
            int nbJsonFiles = Directory.GetFiles(@"" + path, "*.json").Length;
            for (int i = 1; i <= nbJsonFiles; i++)
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
                item.nom = Encoding.UTF8.GetString(Encoding.Default.GetBytes(item.nom));
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
