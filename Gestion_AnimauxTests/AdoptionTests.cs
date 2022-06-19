using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gestion_Animaux;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace Gestion_Animaux.Tests
{
    [TestClass()]
    public class AdoptionTests
    {
        [TestMethod()]
        public void AdoptionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AdoptionTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void FindAllTest()
        {
            List<Adoption> listeAdoptions = new List<Adoption>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.openConnection())
                {
                    reader = access.getData("select * from [iut-acy\\reydetb].Adoption;");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Adoption uneAdoption = new Adoption();
                            uneAdoption.IdAdoptant = (int)reader.GetDecimal(0);
                            uneAdoption.IdAnimal = (int)reader.GetDecimal(1);
                            uneAdoption.DateAdoption = reader.GetDateTime(2);
                            if (!reader.IsDBNull(3))
                            {
                                uneAdoption.CommentaireAdoption = reader.GetString(3);
                            }
                            else
                                uneAdoption.CommentaireAdoption = "";
                            listeAdoptions.Add(uneAdoption);
                        }
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("No rows found.", "Important Message");
                    }
                    reader.Close();
                    access.closeConnection();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message Adoption");
            }
            Adoption test = new Adoption();
            foreach (Adoption uneAdoption in listeAdoptions)
            {
                bool find = false;
                foreach (Adoption lAdoption in test.FindAll())
                {
                    if (uneAdoption == lAdoption)
                        find = true;
                }
                Assert.IsTrue(find);
            }
        }

        [TestMethod()]
        public void CreateTest()
        {
            Adoption test = new Adoption();
            Adoption newTest = new Adoption(10, 15, new DateTime(2021, 03, 12), "Test");

            int preCompteur = test.FindAll().Count;
            newTest.Create();
            int postCompteur = test.FindAll().Count;
            Assert.AreEqual(preCompteur + 1, postCompteur);
        }

        [TestMethod()]
        public void ReadTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Adoption test = new Adoption(2, 3, new DateTime(2020, 4, 20), "Ne pas supprimer");
            test.Create();
            bool find = false;
            foreach (Adoption lAdoption in test.FindAll())
            {
                if (test == lAdoption)
                    find = true;
            }
            Assert.IsTrue(find);
            Adoption testChange = new Adoption(2, 3, new DateTime(2021, 5, 10), "OK");
            testChange.Update();
            find = false;
            foreach (Adoption lAdoption in test.FindAll())
            {
                if (testChange == lAdoption)
                    find = true;
            }
            Assert.IsTrue(find);
            find = false;
            foreach (Adoption lAdoption in test.FindAll())
            {
                if (test == lAdoption)
                    find = true;
            }
            Assert.IsFalse(find);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Adoption test = new Adoption();
            Adoption newTest = new Adoption(11, 15, new DateTime(2021, 03, 12), "Test");
            newTest.Create();

            int preCompteur = test.FindAll().Count;
            newTest.Delete();
            int postCompteur = test.FindAll().Count;
            Assert.AreEqual(preCompteur - 1, postCompteur);
        }

        [TestMethod()]
        public void FindBySelectionTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void CompareByIdTest()
        {
            Assert.Fail();
        }
    }
}