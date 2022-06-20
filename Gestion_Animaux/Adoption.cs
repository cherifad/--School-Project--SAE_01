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
                if (value == null)
                    throw new ArgumentNullException("IdAdoptant ne doit pas être null.");
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
                if(value==null)
                    throw new ArgumentNullException("IdAnimal ne doit pas être null.");
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

        public Adoption(int idAdoptant, int idAnimal, DateTime dateAdoption, string commentaireAdoption)
        {
            IdAdoptant = idAdoptant;
            IdAnimal = idAnimal;
            DateAdoption = dateAdoption;
            CommentaireAdoption = commentaireAdoption;
        }

        public Adoption()
        {
        }

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
            return listeAdoptions;
        }

        public void Create()
        {
            DataAccess access = new DataAccess();
            try
            {
                if (access.openConnection())
                {
                    access.setData($"insert into [iut-acy\\reydetb].Adoption (idadoptant, idanimal, dateadoption, commentaireadoption) values ('{this.IdAdoptant}', '{this.IdAnimal}', '{this.DateAdoption}', '{this.CommentaireAdoption}')");
                }
                access.closeConnection();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message Adoption");
            }
        }

        public void Read()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            int idAn = this.IdAnimal;
            int idAd = this.IdAdoptant;
            DateTime date = this.DateAdoption;
            DataAccess access = new DataAccess();
            try
            {
                if (access.openConnection())
                {
                    bool writer = access.setData($"update [iut-acy\\reydetb].Adoption set idadoptant = '{this.IdAdoptant}', idanimal = '{this.IdAnimal}', dateadoption = '{this.DateAdoption}', commentaireadoption = '{this.CommentaireAdoption}' where idAdoptant = {idAd} and idAnimal = {idAn} and dateadoption = '{date}'");
                    if (!writer)
                    {
                        string message = $"Impossible d'ajouter des données (id : {idAd}, {idAn}";
                        string title = "Erreur d'ajout";
                        var result = System.Windows.MessageBox.Show(message, title, System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Exclamation);
                    }
                    access.closeConnection();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message Adoption");
            }
        }

        public void Delete()
        {
            int idAn = this.IdAnimal;
            int idAd = this.IdAdoptant;
            DataAccess access = new DataAccess();
            try
            {
                if (access.openConnection())
                {
                    bool writer = access.setData($"delete from [iut-acy\\reydetb].Adoption where idadoptant = '{idAd}' and idAnimal = {idAn}");
                    if (!writer)
                    {
                        string message = $"Impossible de supprimer l'adoption (id : {idAd}, {idAn}";
                        string title = "Erreur de suppression";
                        var result = System.Windows.MessageBox.Show(message, title, System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Exclamation);
                    }
                    access.closeConnection();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message Adoption");
            }
        }

        public List<Adoption> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }
        public static int CompareById(Adoption value1, Adoption value2)
        {
            return value1.IdAdoptant.CompareTo(value2.IdAdoptant);
        }

        public override bool Equals(object obj)
        {
            return obj is Adoption adoption &&
                   IdAdoptant == adoption.IdAdoptant &&
                   IdAnimal == adoption.IdAnimal &&
                   DateAdoption == adoption.DateAdoption &&
                   CommentaireAdoption == adoption.CommentaireAdoption;
        }

        public static bool operator ==(Adoption left, Adoption right)
        {
            return EqualityComparer<Adoption>.Default.Equals(left, right);
        }

        public static bool operator !=(Adoption left, Adoption right)
        {
            return !(left == right);
        }
    }
}