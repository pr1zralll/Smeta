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
    public partial class FormAddMaterial : Form
    {
        List<Material> objects = null;
        public FormAddMaterial(List<Material> m)
        {
            InitializeComponent();
            objects = m;
        }
        Material mat=null;
        public FormAddMaterial(List<Material> ms,Material m)
        {
            InitializeComponent();
            objects = ms;
            mat = m;
        }

        private void FormAddMaterial_Load(object sender, EventArgs e)
        {
            if (mat == null)
            {
                this.textBox1.Text = "name";
                this.textBox2.Text = "0";
                this.textBox3.Text = "0";
                this.textBox4.Text = "0";
            } else
            {
                this.textBox1.Text = mat.name;
                this.textBox2.Text = mat.num.ToString();
                this.textBox3.Text = mat.price.ToString();
                this.textBox4.Text = mat.done.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (mat != null)
                {
                    objects.Remove(mat);
                }
                objects.Add(new Material()
                {
                    name = textBox1.Text,
                    num = Convert.ToUInt32(textBox2.Text),
                    price = Convert.ToUInt32(textBox3.Text),
                    done = Convert.ToUInt32(textBox4.Text)
                });
                this.Dispose();
            }
            catch(Exception eq)
            {
                MessageBox.Show(eq.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

     

    }
}
