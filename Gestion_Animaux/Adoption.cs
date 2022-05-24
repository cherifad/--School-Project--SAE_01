using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Gestion_Animaux
{
    public class Adoption : Crud<Adoption>
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
        private int idAnimal;
        public int IdAnimal
        {
            get
            {
                return idAnimal;
            }
            set
            {
                idAnimal = value;
            }
        }
        private DateTime dateAdoption;
        public DateTime DateAdoption
        {
            get
            {
                return dateAdoption;
            }
            set
            {
                dateAdoption = value;
            }
        }
        private string commentaireAdoption;
        public string CommentaireAdoption
        {
            get
            {
                return commentaireAdoption;
            }
            set
            {
                commentaireAdoption = value;
            }
        }


        public List<Adoption> FindAll()
        {
            List<Adoption> listeAdoptions = new List<Adoption>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.openConnection())
                {
                    reader = access.getData("select * from Adoption;");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Adoption uneAdoption = new Adoption();
                            uneAdoption.IdAdoptant = (int)reader.GetDecimal(0);
                            uneAdoption.IdAnimal = (int)reader.GetDecimal(1);
                            uneAdoption.DateAdoption = reader.GetDateTime(2);
                            if (reader[3] == null)
                                uneAdoption.CommentaireAdoption = reader.GetString(3);
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
            return listeAdoptions;
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

        public List<Adoption> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }
    }
}