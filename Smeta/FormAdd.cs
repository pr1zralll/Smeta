using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smeta
{
    public partial class FormAdd : Form
    {
        public FormAdd()
        {
            InitializeComponent();
            smeta = new Smeta();
        }
        public FormAdd(Smeta s)
        {
            InitializeComponent();
            smeta = s;
            edit = true;
        }
        private bool edit = false;
        private void FormAdd_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            //Add column header
            listView1.Columns.Add("Робота/материал", 120);
            listView1.Columns.Add("Кол-во", 70);
            listView1.Columns.Add("Цена", 70);
            listView1.Columns.Add("Сума", 70);
            if (edit)
            {
                this.textBox1.Text = smeta.index.ToString();
                StringBuilder sb = new StringBuilder();
                if (smeta.date.day < 9) sb.Append("0" + smeta.date.day);
                else sb.Append(smeta.date.day);
                sb.Append('.');
                if (smeta.date.month < 9) sb.Append("0" + smeta.date.month);
                else sb.Append(smeta.date.month);
                sb.Append('.');
                sb.Append(smeta.date.year);
                this.textBox2.Text = sb.ToString();
                this.richTextBox3.Text = smeta.objectName;
                this.richTextBox2.Text = smeta.man;
                this.richTextBox1.Text = smeta.data;
            }
            else
            {
                this.textBox1.Text = smeta.index.ToString();
                StringBuilder sb = new StringBuilder();
                if (System.DateTime.Now.Day < 9) sb.Append("0" + System.DateTime.Now.Day);
                else sb.Append(System.DateTime.Now.Day);
                sb.Append('.');
                if (System.DateTime.Now.Month < 9) sb.Append("0" + System.DateTime.Now.Month);
                else sb.Append(System.DateTime.Now.Month);
                sb.Append('.');
                sb.Append(System.DateTime.Now.Year%1000);
                this.textBox2.Text = sb.ToString();
            }
            update();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            smeta = null;
            this.Dispose();
        }
        private static Smeta smeta;
        private void buttonSave_Click(object sender, EventArgs e)
        {
                try
                {
                    parse();
                    if(edit)
                    {
                        Form1.remove(smeta);
                    }
                    Form1.add(smeta);
                }
                catch (Exception e3)
                {
                    MessageBox.Show(e3.Message);
                }
            this.Hide();
        }
        private void parse()
        {
            try
            {
                if (textBox1.Text!=null) smeta.index = Convert.ToUInt32(textBox1.Text);
                if (richTextBox3.Text!=null) smeta.objectName = richTextBox3.Text;
                if (richTextBox2.Text!=null) smeta.man = richTextBox2.Text;
                if (richTextBox1.Text!=null) smeta.data = richTextBox1.Text;
                String dat = "00.00.00";
                if(textBox2.Text!= null) dat = textBox2.Text;
               
                    smeta.date.day = Convert.ToInt32(dat.Substring(0, 2));
                    smeta.date.month = Convert.ToInt32(dat.Substring(3, 2));
                    smeta.date.year = Convert.ToInt32(dat.Substring(6, 2));
                
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Material m = null;
                int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[6].Text);
                foreach (Material sm in smeta.objects)
                {
                    if (id == sm.id)
                    {
                        m = sm;
                        break;
                    }
                }
                new FormAddMaterial(smeta.objects,m).ShowDialog();
                update();
            }
            
        }
        private void update()
        {
            listView1.Items.Clear();
           
            ListViewItem itm;
            foreach (Material m in smeta.objects)
            {
                itm = new ListViewItem(m.toArray());
                listView1.Items.Add(itm);
            }
        }
        private void buttonWorks_Click(object sender, EventArgs e)
        {
            new FormMaterials(smeta).ShowDialog();
            update();
        }
    }
}
