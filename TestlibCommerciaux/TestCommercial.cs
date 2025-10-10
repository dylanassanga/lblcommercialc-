using libCommerciaux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace TestlibCommerciaux
{
    [TestClass]
    public class TestCommercial
    {
        private Type GetCommercialType()
        {
            // Act
           var result = UtilsHelper.GetClassType(typeof(Commercial).Assembly, "Commercial");
           // var result = typeof(Commercial).Assembly.GetTypes().SingleOrDefault(t => t.Name.Equals("Commercial", StringComparison.OrdinalIgnoreCase));
            // Assert
            Assert.IsNotNull(result, "La classe Commercial n'existe pas");
            return result;
        }
        [TestMethod]
        public void TestPropGetNom()
        {
            // Arrange
            var type = GetCommercialType();
            //Agir
            var methodInfo = type.GetMethod("getNom");
            //
            Assert.IsNotNull(methodInfo, "Propriété 'getNom' non trouvée sur la classe Commercial");
            Assert.AreEqual(typeof(string), methodInfo.ReturnType, "La retour doit être de type string");

        }
        [TestMethod]
        public void TestPropGetPreNom()
        {
            // Arrange
            var type = GetCommercialType();
            //Agir
            var methodInfo = type.GetMethod("getPrenom");
            //
            Assert.IsNotNull(methodInfo, "Propriété 'getPrenom' non trouvée sur la classe Commercial");
            Assert.AreEqual(typeof(string), methodInfo.ReturnType, "Le retour doit être de type string");

        }
        [TestMethod]
        public void TestPropgetPuissanceVoiture()
        {
            // Arrange
            var type = GetCommercialType();
            //Agir
            var methodInfo = type.GetMethod("getPuissanceVoiture");
            //
            Assert.IsNotNull(methodInfo, "Propriété 'getPuissanceVoiture' non trouvée sur la classe Commercial");
            Assert.AreEqual(typeof(int), methodInfo.ReturnType, "La propriété 'getPuissanceVoiture' doit être de type int");

        }
        [TestMethod]
        public void TestPropgetCategorie()
        {
            // Arrange
            var type = GetCommercialType();
            //Agir
            var methodInfo = type.GetMethod("getCategorie");
            //
            Assert.IsNotNull(methodInfo, "Propriété 'getCategorie' non trouvée sur la classe Commercial");
            Assert.AreEqual(typeof(char), methodInfo.ReturnType, "La propriété 'getCategorie' doit être de type char");

        }
        [TestMethod]
        public void ToStringTest()
        {
            // Arrange
            var type = GetCommercialType();

            // Rechercher le constructeur avec les paramètres appropriés via réflexion
            ConstructorInfo constructor = type.GetConstructor(new Type[] { typeof(string), typeof(string), typeof(int), typeof(char) });
            Assert.IsNotNull(constructor, "Le constructeur avec paramètres n'existe pas dans la classe Commercial.");

            // Instancier l'objet Commercial avec les valeurs des paramètres
            object[] parameters = new object[] { "Jean", "Dupond", 25, 'A' };
            var commercialInstance = constructor.Invoke(parameters);

            // Attendu
            string attendu = "Nom : Jean Prénom : Dupond Puissance voiture : 25 Categorie : A";

            // Act : appeler la méthode ToString via réflexion
            MethodInfo toStringMethod = type.GetMethod("ToString");
            Assert.IsNotNull(toStringMethod, "La méthode ToString n'existe pas dans la classe Commercial.");
            string resultat = (string)toStringMethod.Invoke(commercialInstance, null);

            // Assert : comparer les résultats
            Assert.AreEqual(attendu, resultat, "La méthode ToString ne retourne pas le résultat attendu.");
        }
        [TestMethod]
        public void TestConstructeurAvecParametres()
        {
            // Arrange
            var type = GetCommercialType();

            // Récupérer le constructeur avec paramètres (string, string, int, char) via réflexion
            ConstructorInfo constructor = type.GetConstructor(new Type[] { typeof(string), typeof(string), typeof(int), typeof(char) });
            Assert.IsNotNull(constructor, "Le constructeur avec paramètres n'existe pas dans la classe Commercial.");

            // Définir les valeurs attendues
            string nomAttendu = "Jean";
            string prenomAttendu = "Dupond";
            int puissanceVoitureAttendue = 25;
            char categorieAttendue = 'A';

            // Act : invoquer le constructeur avec les paramètres
            object[] parameters = new object[] { nomAttendu, prenomAttendu, puissanceVoitureAttendue, categorieAttendue };
            var commercialInstance = constructor.Invoke(parameters);

            // Vérifier que l'instance a bien été créée
            Assert.IsNotNull(commercialInstance, "L'instance de la classe Commercial ne doit pas être null.");

            // Récupérer les champs ou propriétés via réflexion et vérifier leur valeur

            // Vérification de la propriété "nom"
            FieldInfo nomField = type.GetField("nom", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(nomField, "Le champ 'nom' n'existe pas.");
            string nom = (string)nomField.GetValue(commercialInstance);
            Assert.AreEqual(nomAttendu, nom, "La propriété 'nom' n'a pas été correctement initialisée.");

            // Vérification de la propriété "prenom"
            FieldInfo prenomField = type.GetField("prenom", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(prenomField, "Le champ 'prenom' n'existe pas.");
            string prenom = (string)prenomField.GetValue(commercialInstance);
            Assert.AreEqual(prenomAttendu, prenom, "La propriété 'prenom' n'a pas été correctement initialisée.");

            // Vérification de la propriété "puissanceVoiture"
            FieldInfo puissanceVoitureField = type.GetField("puissanceVoiture", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(puissanceVoitureField, "Le champ 'puissanceVoiture' n'existe pas.");
            int puissanceVoiture = (int)puissanceVoitureField.GetValue(commercialInstance);
            Assert.AreEqual(puissanceVoitureAttendue, puissanceVoiture, "La propriété 'puissanceVoiture' n'a pas été correctement initialisée.");

            // Vérification de la propriété "categorie"
            FieldInfo categorieField = type.GetField("categorie", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(categorieField, "Le champ 'categorie' n'existe pas.");
            char categorie = (char)categorieField.GetValue(commercialInstance);
            Assert.AreEqual(categorieAttendue, categorie, "La propriété 'categorie' n'a pas été correctement initialisée.");

            // Vérification de la liste "mesNotes"
            FieldInfo mesNotesField = type.GetField("mesNotes", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(mesNotesField, "Le champ 'mesNotes' n'existe pas.");

        }
        [TestMethod]
        public void TestGetMesNoteFrais()
        {
            // Arrange
            var type = GetCommercialType();
            //Agir
            var getMesNoteFraisMethod = type.GetMethod("getMesNoteFrais");
            //
            Assert.IsNotNull(getMesNoteFraisMethod, "Propriété 'getMesNoteFrais' non trouvée sur la classe Commercial");

            // Utilisation de reflection pour créer une instance de Commercial via le constructeur
            ConstructorInfo constructor = type.GetConstructor(new Type[] { typeof(string), typeof(string), typeof(int), typeof(char) });
            Assert.IsNotNull(constructor, "Constructeur non trouvé dans la classe Commercial.");

            // Créer une instance via reflection
            object commercialInstance = constructor.Invoke(new object[] { "Dupont", "Jean", 5, 'A' });

            // Act : Invoquer la méthode getMesNoteFrais sur l'instance créée via reflection
            var result = getMesNoteFraisMethod.Invoke(commercialInstance, null);

            // Assert
            Assert.IsNotNull(result, "La méthode 'getMesNoteFrais' ne doit pas retourner null.");

            // Vérifier que le résultat est une collection (sans spécifier List<NoteFrais>)
            Assert.IsTrue(typeof(System.Collections.IEnumerable).IsAssignableFrom(result.GetType()),
                                "La méthode 'getMesNoteFrais' doit retourner une collection.");

            }

        [TestMethod]
        public void TestAjouterNoteFraisTransport()
        {
            // Arrange
            var commercialType = typeof(Commercial);
            var commercial = Activator.CreateInstance(commercialType, new object[] { "Dupont", "Jean", 5, 'A' });

            var date = new DateTime(2023, 1, 1);
            int nbKm = 100;

            var ajouterNoteMethod = commercialType.GetMethod("ajouterNote", new Type[] { typeof(DateTime), typeof(int) });
            var getMesNoteFraisMethod = commercialType.GetMethod("getMesNoteFrais");

            Assert.IsNotNull(ajouterNoteMethod, "La méthode ajouterNote n'a pas été trouvée");
            Assert.IsNotNull(getMesNoteFraisMethod, "La méthode getMesNoteFrais n'a pas été trouvée");

            // Act
            ajouterNoteMethod.Invoke(commercial, new object[] { date, nbKm });

            // Assert
            var mesNotes = getMesNoteFraisMethod.Invoke(commercial, null) as System.Collections.IEnumerable;
            var notesList = mesNotes.Cast<object>().ToList();

            Assert.AreEqual(1, notesList.Count, "La liste des notes de frais devrait contenir un élément");

            var fraisTransport = notesList[0];
            var fraisTransportType = fraisTransport.GetType();

            Assert.AreEqual("FraisTransport", fraisTransportType.Name, "L'objet ajouté devrait être de type FraisTransport");

            // Vérification des propriétés de FraisTransport
            var dateProperty = fraisTransportType.GetProperty("Date") ?? fraisTransportType.GetProperty("date");
            var commercialProperty = fraisTransportType.GetProperty("LeCommercial") ?? fraisTransportType.GetProperty("leCommercial");
            var nbKmField = fraisTransportType.GetField("nbKm", BindingFlags.NonPublic | BindingFlags.Instance);

            //Assert.IsNotNull(dateProperty, "La propriété Date n'a pas été trouvée dans FraisTransport");
            //Assert.IsNotNull(commercialProperty, "La propriété LeCommercial n'a pas été trouvée dans FraisTransport");
            Assert.IsNotNull(nbKmField, "Le champ nbKm n'a pas été trouvé dans FraisTransport");

            //Assert.AreEqual(date, dateProperty.GetValue(fraisTransport), "La date n'a pas été correctement initialisée");
            //Assert.AreSame(commercial, commercialProperty.GetValue(fraisTransport), "Le commercial n'a pas été correctement assigné");
            Assert.AreEqual(nbKm, nbKmField.GetValue(fraisTransport), "Le nombre de kilomètres n'a pas été correctement initialisé");
        }
        [TestMethod]
        public void TestAjouterNoteRepasMidi()
        {
            // Arrange
            var commercialType = typeof(Commercial);
            var commercial = Activator.CreateInstance(commercialType, new object[] { "Dupont", "Jean", 5, 'A' });

            var date = new DateTime(2023, 1, 1);
            double montantFacture = 25.50;

            var ajouterNoteMethod = commercialType.GetMethod("ajouterNote", new Type[] { typeof(DateTime), typeof(double) });
            var getMesNoteFraisMethod = commercialType.GetMethod("getMesNoteFrais");

            Assert.IsNotNull(ajouterNoteMethod, "La méthode ajouterNote n'a pas été trouvée");
            Assert.IsNotNull(getMesNoteFraisMethod, "La méthode getMesNoteFrais n'a pas été trouvée");

            // Act
            ajouterNoteMethod.Invoke(commercial, new object[] { date, montantFacture });

            // Assert
            var mesNotes = getMesNoteFraisMethod.Invoke(commercial, null) as System.Collections.IEnumerable;
            var notesList = mesNotes.Cast<object>().ToList();

            Assert.AreEqual(1, notesList.Count, "La liste des notes de frais devrait contenir un élément");

            var repasMidi = notesList[0];
            var repasMidiType = repasMidi.GetType();

            Assert.AreEqual("RepasMidi", repasMidiType.Name, "L'objet ajouté devrait être de type RepasMidi");

            // Vérification des propriétés de RepasMidi
            var dateProperty = repasMidiType.GetProperty("Date") ?? repasMidiType.GetProperty("date");
            var commercialProperty = repasMidiType.GetProperty("LeCommercial") ?? repasMidiType.GetProperty("leCommercial");
            var montantFactureField = repasMidiType.GetField("montantFacture", BindingFlags.NonPublic | BindingFlags.Instance);

            //Assert.IsNotNull(dateProperty, "La propriété Date n'a pas été trouvée dans RepasMidi");
            //Assert.IsNotNull(commercialProperty, "La propriété LeCommercial n'a pas été trouvée dans RepasMidi");
            Assert.IsNotNull(montantFactureField, "Le champ montantFacture n'a pas été trouvé dans RepasMidi");

            //Assert.AreEqual(date, dateProperty.GetValue(repasMidi), "La date n'a pas été correctement initialisée");
            //Assert.AreSame(commercial, commercialProperty.GetValue(repasMidi), "Le commercial n'a pas été correctement assigné");
            Assert.AreEqual(montantFacture, (double)montantFactureField.GetValue(repasMidi), 0.001, "Le montant de la facture n'a pas été correctement initialisé");

            // Vérification supplémentaire : le montant à rembourser ne doit pas dépasser 25 euros
            //var montantARembourserProperty = repasMidiType.GetProperty("MontantARembourser") ?? repasMidiType.GetProperty("montantARembourser");
            //Assert.IsNotNull(montantARembourserProperty, "La propriété MontantARembourser n'a pas été trouvée dans RepasMidi");
            //var montantARembourser = (double)montantARembourserProperty.GetValue(repasMidi);
            //Assert.IsTrue(montantARembourser <= 25.0, "Le montant à rembourser ne doit pas dépasser 25 euros");
        }
        [TestMethod]
        public void TestAjouterNoteNuite()
        {
            // Arrange
            var commercialType = typeof(Commercial);
            var commercial = Activator.CreateInstance(commercialType, new object[] { "Dupont", "Jean", 5, 'A' });

            var date = new DateTime(2023, 1, 1);
            double montantFacture = 85.50;
            char region = 'P'; // Par exemple, 'P' pour Paris

            var ajouterNoteMethod = commercialType.GetMethod("ajouterNote", new Type[] { typeof(DateTime), typeof(double), typeof(char) });
            var getMesNoteFraisMethod = commercialType.GetMethod("getMesNoteFrais");

            Assert.IsNotNull(ajouterNoteMethod, "La méthode ajouterNote n'a pas été trouvée");
            Assert.IsNotNull(getMesNoteFraisMethod, "La méthode getMesNoteFrais n'a pas été trouvée");

            // Act
            ajouterNoteMethod.Invoke(commercial, new object[] { date, montantFacture, region });

            // Assert
            var mesNotes = getMesNoteFraisMethod.Invoke(commercial, null) as System.Collections.IEnumerable;
            var notesList = mesNotes.Cast<object>().ToList();

            Assert.AreEqual(1, notesList.Count, "La liste des notes de frais devrait contenir un élément");

            var nuite = notesList[0];
            var nuiteType = nuite.GetType();

            Assert.AreEqual("Nuite", nuiteType.Name, "L'objet ajouté devrait être de type Nuite");

            // Vérification des propriétés de Nuite
            var dateProperty = nuiteType.GetProperty("Date") ?? nuiteType.GetProperty("date");
            var commercialProperty = nuiteType.GetProperty("LeCommercial") ?? nuiteType.GetProperty("leCommercial");
            var montantFactureField = nuiteType.GetField("montantFacture", BindingFlags.NonPublic | BindingFlags.Instance);
            var regionField = nuiteType.GetField("region", BindingFlags.NonPublic | BindingFlags.Instance);

            //Assert.IsNotNull(dateProperty, "La propriété Date n'a pas été trouvée dans Nuite");
            //Assert.IsNotNull(commercialProperty, "La propriété LeCommercial n'a pas été trouvée dans Nuite");
            Assert.IsNotNull(montantFactureField, "Le champ montantFacture n'a pas été trouvé dans Nuite");
            Assert.IsNotNull(regionField, "Le champ region n'a pas été trouvé dans Nuite");

            //Assert.AreEqual(date, dateProperty.GetValue(nuite), "La date n'a pas été correctement initialisée");
            //Assert.AreSame(commercial, commercialProperty.GetValue(nuite), "Le commercial n'a pas été correctement assigné");
            Assert.AreEqual(montantFacture, (double)montantFactureField.GetValue(nuite), 0.001, "Le montant de la facture n'a pas été correctement initialisé");
            Assert.AreEqual(region, (char)regionField.GetValue(nuite), "La région n'a pas été correctement initialisée");

            // Vérification supplémentaire : le montant à rembourser doit être conforme aux règles de remboursement selon la région
            var montantARembourserProperty = nuiteType.GetProperty("MontantARembourser") ?? nuiteType.GetProperty("montantARembourser");
            //Assert.IsNotNull(montantARembourserProperty, "La propriété MontantARembourser n'a pas été trouvée dans Nuite");
            /*var montantARembourser = (double)montantARembourserProperty.GetValue(nuite);

            // Vérification du montant à rembourser selon la région (à ajuster selon vos règles spécifiques)
            switch (region)
            {
                case 'P': // Paris
                    Assert.IsTrue(montantARembourser <= 100.0, "Le montant à rembourser pour Paris ne doit pas dépasser 100 euros");
                    break;
                case 'G': // Grande ville
                    Assert.IsTrue(montantARembourser <= 80.0, "Le montant à rembourser pour une grande ville ne doit pas dépasser 80 euros");
                    break;
                default: // Autres régions
                    Assert.IsTrue(montantARembourser <= 60.0, "Le montant à rembourser pour les autres régions ne doit pas dépasser 60 euros");
                    break;
            }*/
        }
    }

}


