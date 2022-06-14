using System;
using System.Collections.Generic;
using System.Text;

namespace Gestion_Animaux
{
    public class ApplicationData
    {
        public static List<Adoptant> listeAdoptants
        {
            get;
            set;
        }
        public static List<Adoption> listeAdoptions
        {
            get;
            set;
        }
        public static List<TypeAnimal> listeTypeAnimal
        {
            get;
            set;
        }
        public static List<Animal> listeAnimaux
        {
            get;
            set;
        }
        public static void loadApplicationData()
        {
            //chargement des tables
            Adoptant unAdoptant = new Adoptant();
            Adoption uneAdoption = new Adoption();
            TypeAnimal unType = new TypeAnimal();
            Animal unAnimal = new Animal();
            listeAdoptants = unAdoptant.FindAll();
            listeAdoptions = uneAdoption.FindAll();
            listeTypeAnimal = unType.FindAll();
            listeAnimaux = unAnimal.FindAll();
        }
        public static void UpdateTypeAnimal()
        {
            TypeAnimal unType = new TypeAnimal();
            listeTypeAnimal = unType.FindAll();
        }
    }
}
