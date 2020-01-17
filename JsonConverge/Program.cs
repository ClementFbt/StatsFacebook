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

            convergeFiles.createObject(browserFiles.browsing());
            convergeFiles.displayResults();
            Console.Read();
        }
    }
}
