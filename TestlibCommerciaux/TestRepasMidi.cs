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
    public class TestRepasMidi
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
        private Type GetRepasMidiType()
        {
            // Act
            var result = UtilsHelper.GetClassType(typeof(Commercial).Assembly, "RepasMidi");
            // Assert
            Assert.IsNotNull(result, "La classe RepasMidi n'existe pas");
            return result;
        }
        [TestMethod]
        public void TestClassExists()
        {
            // Assert
            Assert.IsTrue(UtilsHelper.ClassExists(typeof(Commercial).Assembly, "RepasMidi"), "La classe n'existe pas");

        }

        [TestMethod]
        public void TestRepasMidiConstructor()
        {
            // Arrange
            var repasMidiType = GetRepasMidiType();
            var commercialType = GetCommercialType();

            Assert.IsNotNull(repasMidiType, "Le type RepasMidi n'a pas été trouvé");
            Assert.IsNotNull(commercialType, "Le type Commercial n'a pas été trouvé");

            var date = new DateTime(2023, 1, 1);
            var commercial = Activator.CreateInstance(commercialType, new object[] { "Dupont", "Jean", 5, 'A' });
            double montantFacture = 25.50;

            // Act
            var constructorInfo = repasMidiType.GetConstructor(new[] { typeof(DateTime), commercialType, typeof(double) });
            Assert.IsNotNull(constructorInfo, "Le constructeur RepasMidi(DateTime, Commercial, double) n'a pas été trouvé");

            var repasMidi = constructorInfo.Invoke(new object[] { date, commercial, montantFacture });

            // Assert
            Assert.IsNotNull(repasMidi, "L'instance de RepasMidi n'a pas pu être créée");

            // Vérifier les champs hérités de la classe de base (supposons que c'est NoteFrais)
            var baseType = repasMidiType.BaseType;
            Assert.IsNotNull(baseType, "RepasMidi devrait hériter d'une classe de base");

            var dateField = baseType.GetField("dateNoteFrais", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(dateField, "Le champ 'dateNoteFrais' n'a pas été trouvé dans la classe de base");
            Assert.AreEqual(date, dateField.GetValue(repasMidi), "La date n'a pas été correctement initialisée");

            var commercialField = baseType.GetField("leCommercial", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(commercialField, "Le champ 'leCommercial' n'a pas été trouvé dans la classe de base");
            Assert.AreSame(commercial, commercialField.GetValue(repasMidi), "Le commercial n'a pas été correctement assigné");

            // Vérifier montantFacture
            var montantFactureField = repasMidiType.GetField("montantFacture", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(montantFactureField, "Le champ 'montantFacture' n'a pas été trouvé");
            Assert.AreEqual(montantFacture, (double)montantFactureField.GetValue(repasMidi), 0.001, "Le montant facturé n'a pas été correctement initialisé");

            // Vérifier que setMontantARembourser a été appelé
            var montantARembourserField = baseType.GetField("montantARembourser", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(montantARembourserField, "Le champ 'montantARembourser' n'a pas été trouvé dans la classe de base");
            Assert.AreNotEqual(0, (double)montantARembourserField.GetValue(repasMidi), "Le montant à rembourser devrait avoir été calculé");
        }
        [TestMethod]
        public void TestTostring()
        {
            // Arrange
            var repasMidiType = GetRepasMidiType();
            var commercialType = GetCommercialType();
            var date = new DateTime(2023, 1, 1);
            var commercial = Activator.CreateInstance(commercialType, new object[] { "Dupont", "Jean", 5, 'A' });
            double montantFacture = 25.50;
            string attendu = "Repas - Numéro : 1 - Date : 01/01/2023 00:00:00 - Montant à rembourser: 25 euros - Non remboursé - payé : 25,5 €";

            // Act
            var constructorInfo = repasMidiType.GetConstructor(new[] { typeof(DateTime), commercialType, typeof(double) });
          

            var repasMidi = constructorInfo.Invoke(new object[] { date, commercial, montantFacture });

            MethodInfo toStringMethod = repasMidiType.GetMethod("ToString");
            Assert.IsNotNull(toStringMethod, "La méthode ToString n'existe pas dans la classe Commercial.");
            string resultat = (string)toStringMethod.Invoke(repasMidi, null);

            // Assert : comparer les résultats
            Assert.AreEqual(attendu, resultat, "La méthode ToString ne retourne pas le résultat attendu.");


        }
    }
}
