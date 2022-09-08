using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source_Script_Manager
{
    internal class JsonFile:FileObject
    {
        public JsonFile(string path)
        {
            Read(path);
        }
    }
}
