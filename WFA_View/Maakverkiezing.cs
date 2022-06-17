using BusinessLogic;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFA_View
{
    public partial class Maakverkiezing : Form
    {
        KandidaatContainer kandidaatContainer = new KandidaatContainer(new KandidaatDAL());
        VerkiezingContainer verkiezingContainer = new VerkiezingContainer(new VerkiezingDAL());
        List<Kandidaat> kandidaatListInVerkiezing = new List<Kandidaat>();

        public Maakverkiezing()
        {
            InitializeComponent();
        }

        private void Maakverkiezing_Load(object sender, EventArgs e)
        {
            Laadkandidaten();
        }

        public void Laadkandidaten()
        {
            List<Kandidaat> kandidaatList = kandidaatContainer.GetKandidaten();
            listBox1.DataSource = null;
            listBox1.DataSource = kandidaatList;
            listBox1.DisplayMember = "KandidaatNaam";

            listBox2.DataSource = null;
            listBox2.DataSource = kandidaatListInVerkiezing;
            listBox2.DisplayMember = "KandidaatNaam";


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                kandidaatContainer.AddKandidaat(new Kandidaat(textBox1.Text));
                Laadkandidaten();
                textBox1.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem is Kandidaat)
            {
                Kandidaat temp = (Kandidaat)listBox1.SelectedItem;
                if(kandidaatContainer.DeleteKandidaat(temp) == false)
                {
                    MessageBox.Show("Kan niet wegens verkiezing!!!!");
                }
                Laadkandidaten();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem is Kandidaat)
            {
                kandidaatListInVerkiezing.Add((Kandidaat)listBox1.SelectedItem);
            }
            Laadkandidaten();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(listBox2.SelectedItem is Kandidaat)
            {
                kandidaatListInVerkiezing.Remove((Kandidaat)listBox2.SelectedItem);
            }
            Laadkandidaten();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if((kandidaatListInVerkiezing.Count > 1) && (textBox2 != null))
            {
                if(verkiezingContainer.AddVerkiezing(new Verkiezing(textBox2.Text), kandidaatListInVerkiezing))
                {
                    this.Hide();
                    mainscherm mainscherm = new mainscherm();
                    mainscherm.Show();

                }

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                if (listBox1.SelectedItem is Kandidaat)
                {
                    Kandidaat temp = (Kandidaat)listBox1.SelectedItem;
                    temp.KandidaatNaam = textBox1.Text;
                    temp.UpdateKandidaat(new KandidaatDAL());
                }
                Laadkandidaten();
                textBox1.Text = "";
            }
        }
    }
}
