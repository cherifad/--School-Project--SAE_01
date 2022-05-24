using System;

namespace Gestion_Animaux
{
	public class Animal
	{
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
		private string nomAnimal;
		public string NomAnimal
		{
			get
			{
				return nomAnimal;
			}
			set
			{
				nomAnimal = value;
			}
		}
		private double poidsAnimal;
		public double PoidsAnimal
		{
			get
			{
				return poidsAnimal;
			}
			set
			{
				poidsAnimal = value;
			}
		}
		private int tailleAnimal;
		public int TailleAnimal
		{
			get
			{
				return tailleAnimal;
			}
			set
			{
				tailleAnimal = value;
			}
		}
		private TypeAnimal typeAnimal;
		public TypeAnimal TypeAnimal
		{
			get
			{
				return typeAnimal;
			}
			set
			{
				typeAnimal = value;
			}
		}

		public bool AjouterAnimal(int idAnimal, string nomAnimal, double poidsAnimal, int tailleAnimal, TypeAnimal typeAnimal)
		{
			throw new System.NotImplementedException("Not implemented");
		}
		public bool SupprimerAnimal(ref object animalID)
		{
			throw new System.NotImplementedException("Not implemented");
		}
		public bool ModifierAnimal(ref object animalID)
		{
			throw new System.NotImplementedException("Not implemented");
		}
		public Animal AfficherAnimal(object idAnimal)
		{
			throw new System.NotImplementedException("Not implemented");
		}

	}
}