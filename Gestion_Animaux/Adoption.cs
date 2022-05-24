using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Gestion_Animaux
{
	public class Adoption
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
		private string dateAdoption;
		public string DateAdoption
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

		public bool AjouterAdoption(int idAnimal, int idAdoptant, string dateAdoption, string commentaireAdoption)
		{
			throw new System.NotImplementedException("Not implemented");
		}
		public bool ModifierAdoption(ref object adoptionID)
		{
			throw new System.NotImplementedException("Not implemented");
		}
		public bool SupprimerAdoption(ref object adoptionID)
		{
			throw new System.NotImplementedException("Not implemented");
		}
		public Adoption AfficherAdoption(int idAdoptant, int idAnimal)
		{
			throw new System.NotImplementedException("Not implemented");
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
							uneAdoption.DateAdoption = reader.GetString(2);
							uneAdoption.CommentaireAdoption = reader.GetString(3);
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
				System.Windows.MessageBox.Show(ex.Message, "Important Message");
			}
			return listeAdoptions;
		}
	}
}