using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Smeta
{
    [Serializable]
    public class ObjectToSerialize 
    {
        public uint ID = Smeta.s;
        public int IDm = Material.ids;
        private List<Smeta> list;

        public List<Smeta> listSmeta
        {
            get { return this.list; }
            set { this.list = value; }
        }
        
    }
}
