using libCommerciaux;

namespace TestlibCommerciaux
{
    [TestClass]
    public class TestsEcrire
    {
        [TestMethod]
        public void TestajouterNoteFrais()
        {
            //Arranger
            Commercial c = new Commercial("Jean", "Dupond", 25, 'A');
            //Act
            NoteFrais f0 = new NoteFrais(1, new DateTime(2022, 11, 12), 10.0, false, c);
            NoteFrais f1 = new NoteFrais(2, new DateTime(2022, 11, 15), 20.0, false, c);
            //Assert
            Assert.AreEqual(2, c.getMesNoteFrais().Count, "La liste de notes du commercial doit contenir 2 éléments après la création des deux notes de frais.");

        }
    }
}