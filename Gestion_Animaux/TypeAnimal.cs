using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Gestion_Animaux
{
	public class TypeAnimal
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

		public bool AjouterType(int typeID, string libelleType)
		{
			throw new System.NotImplementedException("Not implemented");
		}
		public bool SupprimerType(ref object typeID)
		{
			throw new System.NotImplementedException("Not implemented");
		}
		public bool ModifierType(ref object typeID)
		{
			throw new System.NotImplementedException("Not implemented");
		}
		public TypeAnimal AfficherType(object typeID)
		{
			throw new System.NotImplementedException("Not implemented");
		}
		public List<TypeAnimal> FindAll()
		{
			List<TypeAnimal> listeGroupes = new List<TypeAnimal>();
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
							TypeAnimal unGroupe = new TypeAnimal();
							unGroupe.IdType = (int)reader.GetDecimal(0);
							unGroupe.LibelleType = reader.GetString(1);
							listeGroupes.Add(unGroupe);
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
				System.Windows.MessageBox.Show(ex.Message, "Important Message");
			}
			return listeGroupes;
		}

	}
}