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
    public partial class FormMaterials : Form
    {
        private Smeta smeta = null;
        public FormMaterials(Smeta s)
        {
            InitializeComponent();
            smeta = s;
        }

        private void FormMaterials_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            //Add column header
            listView1.Columns.Add("Робота/материал", 200);
            listView1.Columns.Add("Кол-во", 70);
            listView1.Columns.Add("Цена", 70);
            listView1.Columns.Add("Сума", 70);
            listView1.Columns.Add("Завершено", 70);
            listView1.Columns.Add("Выполнено, %", 70);
            update();
        }
        private void update()
        {
            listView1.Items.Clear();
            parse();
        }
        private void parse()
        {
            ListViewItem itm;
            foreach (Material m in smeta.objects)
            {
                itm = new ListViewItem(m.toArray());
                listView1.Items.Add(itm);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            new FormAddMaterial(smeta.objects).ShowDialog();
            update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Material m = null;
                int id = Convert.ToInt32(listView1.SelectedItems[0].SubItems[6].Text);
                foreach (Material sm in smeta.objects)
                {
                    if (id==sm.id)
                    {
                        m = sm;
                        break;
                    }
                }
                new FormAddMaterial(smeta.objects,m).ShowDialog();
            }
            update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                for (int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    Material m = null;
                    int id = Convert.ToInt32(listView1.SelectedItems[i].SubItems[6].Text);
                    foreach (Material sm in smeta.objects)
                    {
                        if (id == sm.id)
                        {
                            m = sm;
                            break;
                        }
                    }
                    smeta.objects.Remove(m);
                }
                update();
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            button2_Click(sender, e);
        }

    }
}
