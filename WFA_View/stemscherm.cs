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
    public partial class stemscherm : Form
    {
        public stemscherm()
        {
            InitializeComponent();
        }
        VerkiezingContainer verkiezingContainer = new VerkiezingContainer(new VerkiezingDAL());
        KandidaatContainer kandidaatContainer = new KandidaatContainer(new KandidaatDAL());
        StemContainer stemContainer = new StemContainer(new StemDAL());
        int userID = 1;

        Verkiezing verkiezing = new Verkiezing();
        int verkiezingID;

        public void GetVerkiezingID(int verkiezingID)
        {
            verkiezing = verkiezingContainer.GetVerkiezing(verkiezingID);
            this.verkiezingID = verkiezingID;
            LaadVerkiezing();
        }

        public void LaadVerkiezing()
        {
            label2.Text = verkiezing.VerkiezingID.ToString();
            label3.Text = verkiezing.VerkiezingNaam;

            List<Kandidaat> LijstKandidaten = new List<Kandidaat>();
            LijstKandidaten = kandidaatContainer.GetKandidatenFromVerkiezing(verkiezingID);

            listBox1.DataSource = null;
            listBox1.DataSource = LijstKandidaten;
            listBox1.DisplayMember = "KandidaatNaam";

            Kandidaat kandidaattemp = new Kandidaat("storm", 0);

            if(listBox1.SelectedItem is Kandidaat)
            {
                kandidaattemp = (Kandidaat)listBox1.SelectedItem;
                label5.Text = stemContainer.GetStemCount(kandidaattemp.KandidaatID, verkiezingID).ToString();
            }
        }



        private void stemscherm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kandidaat kandidaat = (Kandidaat)listBox1.SelectedItem;
            stemContainer.ADDStem(verkiezingID, kandidaat.KandidaatID, userID);
            LaadVerkiezing();
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LaadVerkiezing();
        }
    }
}
