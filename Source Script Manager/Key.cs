using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Source_Script_Manager
{
    internal class Key
    {
        //a class for storing keys. each key is stored as a simple position in the array - however, it can be initialized by passing it a name or a literal key. i.e. by passing it COMMA or by passing it ,
        int key;
        public Key(int key)
        {
            this.key = key;
        }
        public int GetKeyIndex()
        {
            return key;
        }
    }
}
