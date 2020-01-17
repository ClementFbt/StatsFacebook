using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsonConverge
{

    class BrowserFiles
    {   
        public List<string> browsing()
        {
            List<string> pathList = new List<string>();
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                foreach (string path in Directory.GetFiles(fbd.SelectedPath, "*.json"))
                {
                    pathList.Add(path);
                }
            }
            return pathList;
        }

    }
    
}
