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
    public partial class mainscherm : Form
    {
        public mainscherm()
        {
            InitializeComponent();
        }
        VerkiezingContainer verkiezingContainer = new VerkiezingContainer(new VerkiezingDAL());

        private void button1_Click(object sender, EventArgs e)
        {
            Maakverkiezing maakverkiezing = new Maakverkiezing();
            maakverkiezing.ShowDialog();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Verkiezing verkiezing = (Verkiezing)listBox1.SelectedItem;

            stemscherm stemscherm = new stemscherm();
            stemscherm.GetVerkiezingID(verkiezing.VerkiezingID);
            stemscherm.ShowDialog();
            this.Hide();
        }

        public void LaadVerkiezingen()
        {
            List<Verkiezing> verkiezingsList = verkiezingContainer.GetAllVerkiezingen();

            listBox1.DataSource = null;
            listBox1.DataSource = verkiezingsList;
            listBox1.DisplayMember = "VerkiezingNaam";

        }

        private void mainscherm_Load(object sender, EventArgs e)
        {
            LaadVerkiezingen();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            LaadVerkiezingen();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Verkiezing verkiezing = (Verkiezing)listBox1.SelectedItem;
            verkiezingContainer.DeleteVerkiezing(verkiezing);
            LaadVerkiezingen();
        }
    }
}
