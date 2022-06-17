using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UnitTestenVoteNow.DummyDal;

namespace UnitTestenVoteNow
{
    [TestClass]
    public class VerkiezingTest
    {
        [TestMethod]
        public void AddVerkiezing()
        {
            VerkiezingContainer verkiezingContainer = new VerkiezingContainer(new DummyVerkiezingDAL());

            List<Kandidaat> kandidaatList = new List<Kandidaat>();
            kandidaatList.Add(new Kandidaat("Storm", 8));
            kandidaatList.Add(new Kandidaat("bert", 1));
            kandidaatList.Add(new Kandidaat("peter", 3));

            Assert.IsTrue(verkiezingContainer.AddVerkiezing(new Verkiezing("TestVerkiezing"), kandidaatList));

        }

        [TestMethod]
        public void GetVerkiezing()
        {
            VerkiezingContainer verkiezingContainer = new VerkiezingContainer(new DummyVerkiezingDAL());

            List<Kandidaat> kandidaatList = new List<Kandidaat>();
            kandidaatList.Add(new Kandidaat("Storm", 8));
            kandidaatList.Add(new Kandidaat("bert", 1));
            kandidaatList.Add(new Kandidaat("peter", 3));

            List<Kandidaat> kandidaatList1 = new List<Kandidaat>();
            kandidaatList.Add(new Kandidaat("kees", 7));
            kandidaatList.Add(new Kandidaat("piet", 2));
            kandidaatList.Add(new Kandidaat("peter", 3));

            verkiezingContainer.AddVerkiezing(new Verkiezing(3, "TestVerkiezing"), kandidaatList);
            verkiezingContainer.AddVerkiezing(new Verkiezing(4, "TestVerkiezing1"), kandidaatList1);

            Assert.AreEqual(verkiezingContainer.GetVerkiezing(3).VerkiezingNaam, "TestVerkiezing");

        }

        [TestMethod]
        public void GetVerkiezingen()
        {
            VerkiezingContainer verkiezingContainer = new VerkiezingContainer(new DummyVerkiezingDAL());

            List<Kandidaat> kandidaatList = new List<Kandidaat>();
            kandidaatList.Add(new Kandidaat("Storm", 8));
            kandidaatList.Add(new Kandidaat("bert", 1));
            kandidaatList.Add(new Kandidaat("peter", 3));

            List<Kandidaat> kandidaatList1 = new List<Kandidaat>();
            kandidaatList.Add(new Kandidaat("kees", 7));
            kandidaatList.Add(new Kandidaat("piet", 2));
            kandidaatList.Add(new Kandidaat("peter", 3));

            Verkiezing verkiezing = new Verkiezing(3, "TestVerkiezing");
            Verkiezing verkiezing1 = new Verkiezing(4, "TestVerkiezing");

            verkiezingContainer.AddVerkiezing(new Verkiezing(3, "TestVerkiezing"), kandidaatList);
            verkiezingContainer.AddVerkiezing(new Verkiezing(4, "TestVerkiezing1"), kandidaatList1);

            List<Verkiezing> verkiezings1 = verkiezingContainer.GetAllVerkiezingen(1);

            bool found1 = false;
            bool found2 = false;
            foreach (Verkiezing verkiezing2 in verkiezings1)
            {
                if (verkiezing2.VerkiezingID == 3)
                {
                    found1 = true;
                }
                if (verkiezing2.VerkiezingID == 4)
                {
                    found2 = true;
                }
            }
            Assert.IsTrue(found1 && found2);
        }
        [TestMethod]
        public void DeleteVerkiezing()
        {
            VerkiezingContainer verkiezingContainer = new VerkiezingContainer(new DummyVerkiezingDAL());

            List<Kandidaat> kandidaatList = new List<Kandidaat>();
            kandidaatList.Add(new Kandidaat("Storm", 8));
            kandidaatList.Add(new Kandidaat("bert", 1));
            kandidaatList.Add(new Kandidaat("peter", 3));

            List<Kandidaat> kandidaatList1 = new List<Kandidaat>();
            kandidaatList.Add(new Kandidaat("kees", 7));
            kandidaatList.Add(new Kandidaat("piet", 2));
            kandidaatList.Add(new Kandidaat("peter", 3));

            Verkiezing verkiezing1 = new Verkiezing(3, "TestVerkiezing");
            Verkiezing verkiezing2 = new Verkiezing(4, "TestVerkiezing1");

            verkiezingContainer.AddVerkiezing(verkiezing1, kandidaatList);
            verkiezingContainer.AddVerkiezing(verkiezing2, kandidaatList1);

            bool found = false;
            foreach(Verkiezing verkiezing in verkiezingContainer.GetAllVerkiezingen(1))
            {
                if(verkiezing.VerkiezingID == verkiezing2.VerkiezingID)
                {
                    found = true;
                }
            }
            Assert.IsTrue(found);

            verkiezingContainer.DeleteVerkiezing(verkiezing2);

            foreach (Verkiezing verkiezing in verkiezingContainer.GetAllVerkiezingen(1))
            {
                Assert.AreNotEqual(verkiezing.VerkiezingID, verkiezing2.VerkiezingID);
            }

        }
        [TestMethod]
        public void UpdateVerkiezing()
        {
            DummyVerkiezingDAL dummyVerkiezingDAL = new DummyVerkiezingDAL();
            VerkiezingContainer verkiezingContainer = new VerkiezingContainer(dummyVerkiezingDAL);

            List<Kandidaat> kandidaatList = new List<Kandidaat>();
            kandidaatList.Add(new Kandidaat("Storm", 8));
            kandidaatList.Add(new Kandidaat("bert", 1));
            kandidaatList.Add(new Kandidaat("peter", 3));

            List<Kandidaat> kandidaatList1 = new List<Kandidaat>();
            kandidaatList.Add(new Kandidaat("kees", 7));
            kandidaatList.Add(new Kandidaat("piet", 2));
            kandidaatList.Add(new Kandidaat("peter", 3));

            Verkiezing verkiezing1 = new Verkiezing(3, "TestVerkiezing");
            Verkiezing verkiezing2 = new Verkiezing(4, "TestVerkiezing2");
            Verkiezing verkiezing1_2 = verkiezing1;

            verkiezingContainer.AddVerkiezing(verkiezing1, kandidaatList);
            verkiezingContainer.AddVerkiezing(verkiezing2, kandidaatList1);

            verkiezing1.VerkiezingNaam = "UpdatedVerkiezing";

            verkiezing1.UpdateVerkiezing(dummyVerkiezingDAL);

            Assert.AreNotEqual(verkiezingContainer.GetVerkiezing(3), verkiezing1_2);
        }
        [TestMethod]
        public void UpdateVerkiezingKandidaten()
        {
            Verkiezing verkiezing = new Verkiezing();

            


            
        }

    }

    

}