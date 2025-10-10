using System.ComponentModel;

namespace libCommerciaux
{
    public class Commercial
    {
        private string nom;
        private string prenom;
        private char categorie;
        private int puissanceVoiture;
        private List<NoteFrais> mesNotes;

        public Commercial(string nom, string prenom, int puissanceVoiture, char categorie)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.puissanceVoiture = puissanceVoiture;
            this.categorie = categorie;
            this.mesNotes = new List<NoteFrais>();
        }

        public string getNom()
        {
            return nom;    
        }
        public string getPrenom()
        {
            return prenom;
        }

        public char getCategorie()
        {
           return categorie; 
        }
        public int getPuissanceVoiture() 
        {
            return puissanceVoiture;
        }//hello
        public void ajout_note(NoteFrais note_fr1)
        {
            mesNotes.Add(note_fr1);
        }

        public override string ToString()
        {
            return $"Nom : {nom} Prénom : {prenom} Puissance voiture : {puissanceVoiture} Categorie : {categorie}";
        }

        public void ajouterNoteFrais(NoteFrais f) 
        {
            this.mesNotes.Add(f);
        }

        public List<NoteFrais> getMesNoteFrais()
        {
            return mesNotes;
        }
    }
}
