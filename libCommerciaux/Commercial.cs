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

        public string Nom
        {
            get {  return nom; }
            set { nom = value; }
        }
        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }

        public char getCategorie()
        {
           return categorie; 
        }
        public int getPuissanceVoiture() 
        {
            return puissanceVoiture;
        }

        public List<NoteFrais> MesNotes
        {
            get { return mesNotes; }
            set {mesNotes = value; }
        }
       

        public override string ToString()
        {
            return $"Nom : {nom} Prénom : {prenom} Puissance voiture : {puissanceVoiture} Categorie : {categorie}";
        }

        public void AjouterNoteFrais(NoteFrais f) 
        {
            this.mesNotes.Add(f);
        }
        public int PuissanceVoiture
        {
            get { return puissanceVoiture; }
            set { puissanceVoiture = value; }
        }
        public List<NoteFrais> getMesNoteFrais()
        {
            return mesNotes;
        }

    }
}

