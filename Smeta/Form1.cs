using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace Smeta
{
    public partial class Form1 : Form
    {
        public static List<Smeta> smetaList;
        public Form1()
        {
            InitializeComponent();
            smetaList = new List<Smeta>();
        }
        public static void add(Smeta s)
        {
            smetaList.Add(s);
        }
        public static void remove(Smeta s)
        {
            smetaList.Remove(s);
        }
        private void searchBox_MouseClick(object sender, MouseEventArgs e)
        {
            searchBox.Text = "";
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            new FormAdd().ShowDialog();
            update();
            all_save();
        }
        private void update()
        {
            listView1.Items.Clear();
            parse();
            label1.Text = "Документов: " + smetaList.ToArray().Length;
            if (smetaList.ToArray().Length == 0)
                Smeta.s = 0;
            double sum = 0;
            foreach(Smeta sm in smetaList)
                sum += sm.sum;
            label2.Text = "Сума: " + sum;
        }
        private void overrideButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[6].Text);
                Smeta s = null;
                foreach (Smeta sm in smetaList)
                {
                    if (sm.id == id)
                    {
                        s = sm;
                        break;
                    }
                }
                new FormAdd(s).ShowDialog();
                update();
            }
            all_save();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            //Add column header
            listView1.Columns.Add("Номер", 60);
            listView1.Columns.Add("Дата", 60);
            listView1.Columns.Add("Обьект", 300);
            listView1.Columns.Add("Сума", 70);
            listView1.Columns.Add("Оплата", 50);
            listView1.Columns.Add("Выконання", 50);
            all_load();
            update();
        }
        private void all_load()
        {
            try
            {
                if (System.IO.File.Exists("data.dat"))
                {
                    smetaList = Serializer.deserialize("data.dat").listSmeta;
                    Smeta.s = Serializer.deserialize("data.dat").ID;
                    Material.ids = Serializer.deserialize("data.dat").IDm;
                }
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
                System.IO.File.Delete("data.dat");
            }
        }
        private void all_save()
        {
            ObjectToSerialize o = new ObjectToSerialize();
            o.listSmeta = smetaList;
            o.ID = Smeta.s;
            Serializer.serialize("data.dat",o);
        }
        private void parse()
        { 
            ListViewItem itm;
            foreach(Smeta sm in smetaList)
            {
                itm = new ListViewItem(sm.toArray());
                listView1.Items.Add(itm);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    int id = Convert.ToInt32(listView1.SelectedItems[i].SubItems[6].Text);
                    Smeta s = null;
                    foreach (Smeta sm in smetaList)
                    {
                        if (sm.id == id)
                        {
                            s = sm;
                            break;
                        }
                    }
                    remove(s);
                }
                update();
            }
            all_save();
        }

        private void sortButton_Click(object sender, EventArgs e)
        {
            smetaList.Sort((s1,s2)=>string.Compare(s1.objectName,s2.objectName,true));
            update();  
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[6].Text);
                Smeta s = null;
                foreach (Smeta sm in smetaList)
                {
                    if (sm.id == id)
                    {
                        s = sm;
                        break;
                    }
                }
                s.paid = true;
                update();
            }
            all_save();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[6].Text);
                Smeta s = null;
                foreach (Smeta sm in smetaList)
                {
                    if (sm.id == id)
                    {
                        s = sm;
                        break;
                    }
                }
                s.complated = true;
                update();
            }
            all_save();
        }
        private bool testFind(String s, String f)
        {
            if (f.Length == 0) return true;
           
            if (s.Contains(f))
            {
                return true;
            }
            return false;
        }
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            
            listView1.Items.Clear();
            string tofind = searchBox.Text;
            ListViewItem itm;
            List<Smeta> ls = smetaList.FindAll(delegate(Smeta s) {
                if (testFind(s.date.ToString(),tofind))
                    return true;
                else if (testFind(s.objectName, tofind))
                {
                    return true;
                }
                else if (testFind(s.index.ToString(), tofind))
                {
                    return true;
                }
                else if (testFind(s.sum.ToString(), tofind))
                {
                    return true;
                }else{
                    return false;
                }
            });
            foreach (Smeta sm in ls)
            {
                itm = new ListViewItem(sm.toArray());
                listView1.Items.Add(itm);
            }
           if(tofind.Length==0)
           {
               update();
           }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button_Word_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[6].Text);
                Smeta s = null;
                foreach (Smeta sm in smetaList)
                {
                    if (sm.id == id)
                    {
                        s = sm;
                        break;
                    }
                }
                saveFileDialog1.ShowDialog();
                toWord(s,saveFileDialog1.FileName,true);
            }
        }
        private void toWord(Smeta s,string path,bool v)
        {
            Word.Application word=null;
            Word.Document doc=null;
            try
            {
                word = new Word.Application();
                word.Visible = false;
                doc = word.Documents.Open(@"D:\temp.docx");
                replace("{date}", s.date.ToString(), doc);
                replace("{data}", s.data, doc);
                replace("{complated}", s.complated ? "Выполнено" : "Не выполнено", doc);
                replace("{index}", s.index.ToString(), doc);
                replace("{man}", s.man, doc);
                replace("{name}", s.objectName, doc);
                replace("{sum}", s.sum.ToString(), doc);
                var materials = s.objects.ToArray();
                for (int i = 0; i < materials.Length; i++)
                {
                    var mat = materials[i].toArray();
                    replace("{m1}", mat[0] + "^p" + "{m1}", doc);
                    replace("{m2}", mat[1] + "^p" + "{m2}", doc);
                    replace("{m3}", mat[2] + "^p" + "{m3}", doc);
                    replace("{m4}", mat[3] + "^p" + "{m4}", doc);
                    replace("{m5}", mat[5] + "^p" + "{m5}", doc);
                }
                replace("{m1}", "", doc);
                replace("{m2}", "", doc);
                replace("{m3}", "", doc);
                replace("{m4}", "", doc);
                replace("{m5}", "", doc);
                word.Visible = v;
                if (v) doc.SaveAs2(@path);
                if (!v) doc.PrintOut();
            }catch(Exception e)
            {
                if (doc != null) doc.Close();
                MessageBox.Show(e.Message);
            }
        }
        private void replace(string stub,string text,Word.Document doc)
        {
            var range = doc.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stub, ReplaceWith: text);
        }
        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            overrideButton_Click(sender,e);
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[6].Text);
                Smeta s = null;
                foreach (Smeta sm in smetaList)
                {
                    if (sm.id == id)
                    {
                        s = sm;
                        break;
                    }
                }
                toWord(s, "temp.docx", false);
            }
        }
    }
}
