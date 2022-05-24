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
        }/*
        public static List<Professeur> listeProfesseurs
        {
            get;
            set;
        }*/
        public static List<TypeAnimal> listeTypeAnimal
        {
            get;
            set;
        }
        /*public static List<EstNote> listeEstNotes
        {
            get;
            set;
        }*/
        public static void loadApplicationData()
        {
            //chargement des tables
            Adoptant unAdoptant = new Adoptant();
            //Professeur unProfesseur = new Professeur();
            TypeAnimal unGroupe = new TypeAnimal();
            //EstNote unEstNote = new EstNote();
            listeAdoptants = unAdoptant.FindAll();
            //listeProfesseurs = unProfesseur.FindAll();
            listeTypeAnimal = unGroupe.FindAll();
            //listeEstNotes = unEstNote.FindAll();
            //mapping des relations en mode déconnecté
            //relation bi-directionnelle entre eleve et groupe
            
        //relation eleve -> note
        //relation note -> professeur
        }
    }
}
