using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Gestion_Animaux
{
    public class TypeAnimal : Crud<TypeAnimal>
    {
        private int idType;
        public int IdType
        {
            get
            {
                return idType;
            }
            set
            {
                idType = value;
            }
        }
        private string libelleType;
        public string LibelleType
        {
            get
            {
                return libelleType;
            }
            set
            {
                libelleType = value;
            }
        }


        public List<TypeAnimal> FindAll()
        {
            List<TypeAnimal> listeTypes = new List<TypeAnimal>();
            DataAccess access = new DataAccess();
            SqlDataReader reader;
            try
            {
                if (access.openConnection())
                {
                    reader = access.getData("select * from TypeAnimal;");
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TypeAnimal unType = new TypeAnimal();
                            unType.IdType = (int)reader.GetDecimal(0);
                            unType.LibelleType = reader.GetString(1);
                            listeTypes.Add(unType);
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
                System.Windows.MessageBox.Show(ex.Message, "Important Message TypeAnimal");
            }
            return listeTypes;
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

        public List<TypeAnimal> FindBySelection(string criteres)
        {
            throw new NotImplementedException();
        }
    }
}