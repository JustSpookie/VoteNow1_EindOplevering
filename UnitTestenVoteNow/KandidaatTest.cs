using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UnitTestenVoteNow.DummyDal;

namespace UnitTestenVoteNow
{
    [TestClass]
    public class KandidaatTest
    {
        [TestMethod]
        public void AddKandidaat()
        {
            KandidaatContainer kandidaatContainer = new KandidaatContainer(new DummyKandidaatDAl());

            Assert.IsTrue(kandidaatContainer.AddKandidaat(new Kandidaat("storm")));

            Assert.IsTrue(kandidaatContainer.AddKandidaat(new Kandidaat("bert")));
        }

        [TestMethod]
        public void GetKandidaat()
        {
            KandidaatContainer kandidaatContainer = new KandidaatContainer(new DummyKandidaatDAl());

            kandidaatContainer.AddKandidaat(new Kandidaat("storm"));

            kandidaatContainer.AddKandidaat(new Kandidaat("bert"));


            Assert.AreEqual(kandidaatContainer.GetKandidaat(0).KandidaatID, 1);
            Assert.AreEqual(kandidaatContainer.GetKandidaat(1).KandidaatID, 2);
            Assert.AreNotEqual(kandidaatContainer.GetKandidaat(2).KandidaatID, 3);

        }

        [TestMethod]
        public void GetKandidaten()
        {
            KandidaatContainer kandidaatContainer = new KandidaatContainer(new DummyKandidaatDAl());

            List<Kandidaat> kandidaats1 = new List<Kandidaat>();

            kandidaats1.Add(new Kandidaat("Storm"));
            kandidaats1.Add(new Kandidaat("kees"));
            kandidaats1.Add(new Kandidaat("bert"));
            kandidaats1.Add(new Kandidaat("jaap"));
            kandidaats1.Add(new Kandidaat("nick"));
            kandidaats1.Add(new Kandidaat("piet"));

            int i = 1;

            foreach(Kandidaat kandidaat in kandidaats1)
            {
                kandidaatContainer.AddKandidaat(kandidaat);
                kandidaat.KandidaatID = i;
                i++;
            }

            List<Kandidaat> kandidaats2 = kandidaatContainer.GetKandidaten(1);

            Assert.IsTrue(kandidaats2.Count == kandidaats1.Count);

            foreach(Kandidaat kandidaat1 in kandidaats1)
            {
                bool found = false;
                foreach(Kandidaat kandidaat2 in kandidaats2)
                {
                    if(kandidaat1.KandidaatID == kandidaat2.KandidaatID)
                    {
                        found = true;
                    }
                }
                Assert.IsTrue(found);
            }

        }

        [TestMethod]
        public void DeleteKandidaten()
        {
            KandidaatContainer kandidaatContainer = new KandidaatContainer(new DummyKandidaatDAl());

            List<Kandidaat> kandidaats1 = new List<Kandidaat>();

            kandidaats1.Add(new Kandidaat("Storm"));
            kandidaats1.Add(new Kandidaat("kees"));
            kandidaats1.Add(new Kandidaat("bert"));
            kandidaats1.Add(new Kandidaat("jaap"));
            kandidaats1.Add(new Kandidaat("nick"));
            kandidaats1.Add(new Kandidaat("piet"));

            int i = 1;
            foreach (Kandidaat kandidaat in kandidaats1)
            {
                kandidaatContainer.AddKandidaat(kandidaat);
                kandidaat.KandidaatID = i;
                i++;
            }


            List<Kandidaat> delete = new List<Kandidaat>();
            delete.Add(kandidaats1[1]);
            delete.Add(kandidaats1[3]);

            foreach(Kandidaat kandidaat in delete)
            {
                Assert.IsTrue(kandidaatContainer.DeleteKandidaat(kandidaat));
            }

            foreach(Kandidaat kandidaat in kandidaatContainer.GetKandidaten(1))
            {
                foreach(Kandidaat kandidaat1 in delete)
                {
                    Assert.IsFalse(kandidaat1.KandidaatID == kandidaat.KandidaatID);
                }
            }



        }

        [TestMethod]
        public void GetKandidatenFromVerkiezing()
        {
            KandidaatContainer kandidaatContainer = new KandidaatContainer(new DummyKandidaatDAl());

            List<Kandidaat> list = new List<Kandidaat>();
            list.Add(new Kandidaat("bert", 1));
            list.Add(new Kandidaat("tio", 2));
            list.Add(new Kandidaat("kees", 3));
            list.Add(new Kandidaat("nick", 4));

            List<Kandidaat> list2 = kandidaatContainer.GetKandidatenFromVerkiezing(1);

            Assert.AreEqual(list.Count, list2.Count);

            foreach(Kandidaat kandidaat in list)
            {
                Assert.IsTrue(list2.Contains(kandidaat));
            }

        }


    }
}