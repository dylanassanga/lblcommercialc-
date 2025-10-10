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
    public class TestNuite
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
        private Type GetNuiteType()
        {
            // Act
            var result = UtilsHelper.GetClassType(typeof(Commercial).Assembly, "Nuite");
            // Assert
            Assert.IsNotNull(result, "La classe Nuite n'existe pas");
            return result;
        }
        [TestMethod]
        public void TestClassExists()
        {
            // Assert
            Assert.IsTrue(UtilsHelper.ClassExists(typeof(Commercial).Assembly, "Nuite"), "La classe n'existe pas");

        }
        [TestMethod]
        public void TestNuiteConstructor()
        {
            // Arrange
            var nuiteType = GetNuiteType();
            var commercialType = GetCommercialType();

            Assert.IsNotNull(nuiteType, "Le type Nuite n'a pas été trouvé");
            Assert.IsNotNull(commercialType, "Le type Commercial n'a pas été trouvé");

            var date = new DateTime(2023, 1, 1);
            var commercial = Activator.CreateInstance(commercialType, new object[] { "Dupont", "Jean", 5, 'A' });
            double montantFacture = 100.50;
            char region = 'A';

            // Act
            var constructorInfo = nuiteType.GetConstructor(new[] { typeof(DateTime), commercialType, typeof(double), typeof(char) });
            Assert.IsNotNull(constructorInfo, "Le constructeur Nuite(DateTime, Commercial, double, char) n'a pas été trouvé");

            var nuite = constructorInfo.Invoke(new object[] { date, commercial, montantFacture, region });

            // Assert
            Assert.IsNotNull(nuite, "L'instance de Nuite n'a pas pu être créée");

            // Vérifier les champs hérités de la classe de base (supposons que c'est NoteFrais)
            var baseType = nuiteType.BaseType;
            Assert.IsNotNull(baseType, "Nuite devrait hériter d'une classe de base");

            var dateField = baseType.GetField("dateNoteFrais", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(dateField, "Le champ 'dateNoteFrais' n'a pas été trouvé dans la classe de base");
            Assert.AreEqual(date, dateField.GetValue(nuite), "La date n'a pas été correctement initialisée");

            var commercialField = baseType.GetField("leCommercial", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(commercialField, "Le champ 'leCommercial' n'a pas été trouvé dans la classe de base");
            Assert.AreSame(commercial, commercialField.GetValue(nuite), "Le commercial n'a pas été correctement assigné");

            // Vérifier montantFacture
            var montantFactureField = nuiteType.GetField("montantFacture", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(montantFactureField, "Le champ 'montantFacture' n'a pas été trouvé");
            Assert.AreEqual(montantFacture, (double)montantFactureField.GetValue(nuite), 0.001, "Le montant facturé n'a pas été correctement initialisé");

            // Vérifier region
            var regionField = nuiteType.GetField("region", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(regionField, "Le champ 'region' n'a pas été trouvé");
            Assert.AreEqual(region, (char)regionField.GetValue(nuite), "La région n'a pas été correctement initialisée");

            // Vérifier que setMontantARembourser a été appelé
            var montantARembourserField = baseType.GetField("montantARembourser", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(montantARembourserField, "Le champ 'montantARembourser' n'a pas été trouvé dans la classe de base");
            Assert.AreNotEqual(0, (double)montantARembourserField.GetValue(nuite), "Le montant à rembourser devrait avoir été calculé");
        }
        [TestMethod]
        public void TestTostring()
        {
            // Arrange
            var nuiteType = GetNuiteType();
            var commercialType = GetCommercialType();
            var date = new DateTime(2023, 1, 1);
            var commercial = Activator.CreateInstance(commercialType, new object[] { "Dupont", "Jean", 5, 'A' });
            double montantFacture = 100.50;
            char region = 'A';
            string attendu = "Nuité - Numéro : 1 - Date : 01/01/2023 00:00:00 - Montant à rembourser: 65 euros - Non remboursé - payé : 100,5 € - A -";

            // Act
            var constructorInfo = nuiteType.GetConstructor(new[] { typeof(DateTime), commercialType, typeof(double), typeof(char) });

            var nuite = constructorInfo.Invoke(new object[] { date, commercial, montantFacture, region });

            MethodInfo toStringMethod = nuiteType.GetMethod("ToString");
            Assert.IsNotNull(toStringMethod, "La méthode ToString n'existe pas dans la classe Commercial.");
            string resultat = (string)toStringMethod.Invoke(nuite, null);

            // Assert : comparer les résultats
            Assert.AreEqual(attendu, resultat, "La méthode ToString ne retourne pas le résultat attendu.");


        }
    }
}
