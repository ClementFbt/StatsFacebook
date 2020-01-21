using System;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsonConverge
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ConvergeFiles convergeFiles = new ConvergeFiles();
            BrowserFiles browserFiles = new BrowserFiles();

            Console.Write("Entrez votre nom d'utilisateur : ");
            string userName = Console.ReadLine();
            convergeFiles.createObject(browserFiles.browsing());
            convergeFiles.exportJson(userName);
            Console.Read();
        }
    }
}
