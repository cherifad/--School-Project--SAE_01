using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Gestion_Animaux
{
    public class Adoptant : Crud<Adoptant>
    {
        private int idAdoptant;
        public int IdAdoptant
        {
            get
            {
                return idAdoptant;
            }
            set
            {
                idAdoptant = value;
            }
        }
        private string nomAdoptant;
        public string NomAdoptant
        {
            get
            {
                return nomAdoptant;
            }
            set
            {
                nomAdoptant = value;
            }
        }
        private string prenomAdoptant;
        public string PrenomAdoptant
        {
            get
            {
                return prenomAdoptant;
            }
            set
            {
                prenomAdoptant = value;
            }
        }
        private string telAdoptant;
        public string TelAdoptant
        {
            get
            {
                return telAdoptant;
            }
            set
            {
                telAdoptant = value;
            }
        }
        private string mailAdoptant;
        public string MailAdoptant
        {
            get
            {
                return mailAdoptant;
            }
            set
            {
                mailAdoptant = value;
            }
        }



        public List<Adoptant> FindAll()
        {
            List<Adoptant> listeAdoptants = new List<Adoptant>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.openConnection())
                {
                    reader = access.getData("select * from Adoptant;");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Adoptant unAdoptant = new Adoptant();
                            unAdoptant.IdAdoptant = (int)reader.GetDecimal(0);
                            unAdoptant.NomAdoptant = reader.GetString(1);
                            unAdoptant.PrenomAdoptant = reader.GetString(2);
                            unAdoptant.TelAdoptant = reader.GetString(3);
                            unAdoptant.MailAdoptant = reader.GetString(4);
                            listeAdoptants.Add(unAdoptant);
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
                System.Windows.MessageBox.Show(ex.Message, "Important Message Adoptant");
            }
            return listeAdoptants;
        }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public List<Adoptant> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }
    }
}