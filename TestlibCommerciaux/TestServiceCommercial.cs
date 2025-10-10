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
    public class TestServiceCommercial
    {
        private Type GetServiceCommercialType()
        {
            // Act
            var result = UtilsHelper.GetClassType(typeof(Commercial).Assembly, "ServiceCommercial");
            // Assert
            Assert.IsNotNull(result, "La classe ServiceCommercial n'existe pas");
            return result;
        }

        [TestMethod]
        public void TestClassExists()
        {
            // Assert
            Assert.IsTrue(UtilsHelper.ClassExists(typeof(Commercial).Assembly, "ServiceCommercial"), "La classe n'existe pas");

        }

        [TestMethod]
        public void TestServiceCommercialConstructorInitializesCollection()
        {
            // Arrange
            var type = GetServiceCommercialType();

            // Act
            ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
            Assert.IsNotNull(constructor, "Le constructeur sans paramètres de la classe ServiceCommercial n'a pas été trouvé.");

            // Créer une instance de ServiceCommercial via reflection
            var serviceCommercialInstance = constructor.Invoke(null);

            // Assert
            Assert.IsNotNull(serviceCommercialInstance, "L'instance de la classe ServiceCommercial ne doit pas être null.");

            // Vérifier que le champ 'lesCommerciaux' est bien initialisé
            FieldInfo lesCommerciauxField = type.GetField("lesCommerciaux", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.IsNotNull(lesCommerciauxField, "Le champ 'lesCommerciaux' n'existe pas.");

            // Récupérer la valeur du champ 'lesCommerciaux' sans mentionner List<Commercial>
            var lesCommerciauxValue = lesCommerciauxField.GetValue(serviceCommercialInstance);
            Assert.IsNotNull(lesCommerciauxValue, "Le champ 'lesCommerciaux' doit être initialisé.");

            // Vérifier que le champ 'lesCommerciaux' implémente IEnumerable (collection générique)
            Assert.IsTrue(typeof(System.Collections.IEnumerable).IsAssignableFrom(lesCommerciauxValue.GetType()),
                "Le champ 'lesCommerciaux' doit être une collection.");

            // Vérifier que la collection est vide (en vérifiant la propriété Count si elle existe)
            var countProperty = lesCommerciauxValue.GetType().GetProperty("Count");
            Assert.IsNotNull(countProperty, "Le type de la collection ne possède pas de propriété 'Count'.");
            int count = (int)countProperty.GetValue(lesCommerciauxValue);
            Assert.AreEqual(0, count, "La collection 'lesCommerciaux' doit être vide au départ.");
        }
        [TestMethod]
        public void TestAjouterCommercial()
        {
            // Arrange
            var type = GetServiceCommercialType();
            //Agir
            var methodInfo = type.GetMethod("ajouterCommercial");
            //
            Assert.IsNotNull(methodInfo, "Propriété 'ajouterCommercial' non trouvée sur la classe ServiceCommercial");
            Assert.AreEqual(typeof(void), methodInfo.ReturnType, "Le retour doit être de type void");
        }
    }

}

