using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smeta
{
    [Serializable]
    public partial class Smeta 
    {
        public static uint s = 0;
        
        public Smeta()
        {
            date = new Date();
            objects = new List<Material>();
            index = s;
            date.day = System.DateTime.Now.Day;
            date.month = System.DateTime.Now.Month;
            date.year = System.DateTime.Now.Year%1000;
            paid = false;
            objectName = "new object";
            id = s++;
        }
        public String[] toArray()
        {
            string [] arr = new string[7];
            StringBuilder myText = new StringBuilder();
            
            if (date.day < 9) myText.Append("0" + date.day);
            else myText.Append(date.day);
            myText.Append(".");
            if (date.month < 9) myText.Append("0" + date.month);
            else myText.Append(date.month);
            myText.Append(".");
            myText.Append(date.year);

            arr[0] = index.ToString();
            arr[1] = myText.ToString();
            arr[2] = objectName;
            arr[3] = sum.ToString();
            arr[4] = (paid?"+":"-");
            arr[5] = (complated ? "+" : "-");
            arr[6] = id.ToString();
            return arr;
        }
        public String man { get; set; }
        public String data { get; set; }
        public uint id = 0;
        public UInt32 index { get; set; }
        public Date date { get; set; }
        public String objectName { get; set; }
        public double sum
        {
            
            get
            {
                uint s = 0;
                foreach (Material m in this.objects)
                {
                    s += m.sum;
                }
                return s;
            }

            
        }
        public Boolean complated
        {
            set
            {
                foreach(Material m in this.objects)
                {
                    m.done = m.num;
                }
            }
            get
            {
         
                float d = 0;
                foreach (Material m in objects)
                {
                    d += m.complated;
                }
                
                return (d / 100.0f / (float)this.objects.Count) == 1?true:false;
            }
        }
        public Boolean paid = false;
        public List<Material> objects;
    }
     [Serializable]
    public class Date
    {
       public int day;
       public int month;
       public int year;
       public override string ToString()
       {
           StringBuilder myText = new StringBuilder();

           if (day < 9) myText.Append("0" + day);
           else myText.Append(day);
           myText.Append(".");
           if (month < 9) myText.Append("0" + month);
           else myText.Append(month);
           myText.Append(".");
           myText.Append(year);

           return myText.ToString();
       }
    };
     [Serializable]
    public class Material
    {
        public static int ids = 0;
        public Material()
        {
            this.id = ids++;
        }
        public int id=0;
        public String name;
        public uint num;
        public uint price;
        public uint sum { get{return num*price;} }
        public uint done;
        public float complated { get { if (num > 0) return (float)((float)done / (float)num * 100.0); else return 100.0f; } }
        public String[] toArray()
        {
            String[] arr = new String[7];
            arr[0] = name;
            arr[1] = num.ToString();
            arr[2] = price.ToString();
            arr[3] = sum.ToString();
            arr[4] = done.ToString();
            arr[5] = complated.ToString()+"%";
            arr[6] = id.ToString();
            return arr;
        }
        public override string ToString()
        {
            var s = toArray();
            return s[0] + " " + s[1] + " " + s[2] + " " + s[3] + " " + s[5];
        }
    };
}













