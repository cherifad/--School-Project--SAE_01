using System;

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

	}
}