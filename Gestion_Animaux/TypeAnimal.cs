using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Gestion_Animaux
{
    public class TypeAnimal : Crud<TypeAnimal>
    {
        public TypeAnimal(string libelleType)
        {
            this.LibelleType = libelleType;
        }
        public TypeAnimal()
        {
        }
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
                    reader = access.getData("select * from [iut-acy\\reydetb].TypeAnimal;");
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
            DataAccess access = new DataAccess();
            //SqlDataAdapter writer;

            try
            {
                if (access.openConnection())
                {
                    access.setData("insert into [iut-acy\\reydetb].TypeAnimal (libelletype) values ('" + this.LibelleType + "')");
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Important Message TypeAnimal");
            }
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
        public static int CompareById(TypeAnimal value1, TypeAnimal value2)
        {
            return value1.IdType.CompareTo(value2.IdType);
        }
    }
}