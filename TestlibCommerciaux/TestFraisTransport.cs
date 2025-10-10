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
    public class TestFraisTransport
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
        private Type GetFraisTransportType()
        {
            // Act
            var result = UtilsHelper.GetClassType(typeof(Commercial).Assembly, "FraisTransport");
            // Assert
            Assert.IsNotNull(result, "La classe FraisTransport n'existe pas");
            return result;
        }
        [TestMethod]
        public void TestClassExists()
        {
            // Assert
            Assert.IsTrue(UtilsHelper.ClassExists(typeof(Commercial).Assembly, "FraisTransport"), "La classe n'existe pas");

        }
  [TestMethod]
        public void TestFraisTransportConstructor()
        {
            // Arrange
            var fraisTransportType = GetFraisTransportType();
            var commercialType = GetCommercialType();
            Assert.IsNotNull(fraisTransportType, "Le type FraisTransport n'a pas été trouvé");
            Assert.IsNotNull(commercialType, "Le type Commercial n'a pas été trouvé");

            var date = new DateTime(2023, 1, 1);
            var commercial = Activator.CreateInstance(commercialType, new object[] { "Dupont", "Jean", 5, 'A' });
            int nbKm = 100;

            // Act
            ConstructorInfo constructeur = fraisTransportType.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public,
                null,
                new Type[] { typeof(DateTime), commercialType, typeof(int) },
                null);
            Assert.IsNotNull(constructeur, "Le constructeur de FraisTransport n'a pas été trouvé");

            object fraisTransport = constructeur.Invoke(new object[] { date, commercial, nbKm });

            // Assert
            Assert.IsNotNull(fraisTransport, "L'instance de FraisTransport n'a pas pu être créée");

            // Vérifier les champs hérités de la classe de base (NoteFrais)
            var baseType = fraisTransportType.BaseType;
            Assert.IsNotNull(baseType, "FraisTransport devrait hériter d'une classe de base");

            var dateField = baseType.GetField("dateNoteFrais", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(dateField, "Le champ 'dateNoteFrais' n'a pas été trouvé dans la classe de base");
            Assert.AreEqual(date, dateField.GetValue(fraisTransport), "La date n'a pas été correctement initialisée");

            var commercialField = baseType.GetField("leCommercial", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(commercialField, "Le champ 'leCommercial' n'a pas été trouvé dans la classe de base");
            Assert.AreSame(commercial, commercialField.GetValue(fraisTransport), "Le commercial n'a pas été correctement assigné");

            // Vérifier nbKm
            var nbKmField = fraisTransportType.GetField("nbKm", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(nbKmField, "Le champ 'nbKm' n'a pas été trouvé");
            Assert.AreEqual(nbKm, Convert.ToInt32(nbKmField.GetValue(fraisTransport)), "Le nombre de km n'a pas été correctement initialisé");

            // Vérifier que setMontantARembourser a été appelé
            var montantARembourserField = baseType.GetField("montantARembourser", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(montantARembourserField, "Le champ 'montantARembourser' n'a pas été trouvé dans la classe de base");
            Assert.IsTrue(Convert.ToDecimal(montantARembourserField.GetValue(fraisTransport)) > 0, "Le montant à rembourser devrait avoir été calculé et être supérieur à zéro");
        }
        //Transport - Numéro : 4 - Date : 12/11/2022 00:00:00 - Montant à rembourser: 50 euros - Non remboursé - 250 km-

        [TestMethod]
        public void TestTostring()
        {
            // Arrange
            var fraisTransportType = GetFraisTransportType();
  var commercialType = GetCommercialType();
            var date = new DateTime(2023, 1, 1);
            var commercial = Activator.CreateInstance(commercialType, new object[] { "Dupont", "Jean", 5, 'A' });
            int nbKm = 100;
            string attendu = "Transport - Numéro : 1 - Date : 01/01/2023 00:00:00 - Montant à rembourser: 20 euros - Non remboursé - 100 km-";

            // Act
            ConstructorInfo constructeur = fraisTransportType.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public,
                null,
                new Type[] { typeof(DateTime), commercialType, typeof(int) },
                null);
           
            object fraisTransport = constructeur.Invoke(new object[] { date, commercial, nbKm });
                     
            MethodInfo toStringMethod = fraisTransportType.GetMethod("ToString");
            Assert.IsNotNull(toStringMethod, "La méthode ToString n'existe pas dans la classe Commercial.");
            string resultat = (string)toStringMethod.Invoke(fraisTransport, null);

            // Assert : comparer les résultats
            Assert.AreEqual(attendu, resultat, "La méthode ToString ne retourne pas le résultat attendu.");


        }
    }
}
