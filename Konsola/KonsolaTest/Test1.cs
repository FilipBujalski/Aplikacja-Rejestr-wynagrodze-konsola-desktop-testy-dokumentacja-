namespace KonsolaTest
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        public void ObliczZaliczkePit_GdyUlgaDlaMlodychWlaczona_PowinnaZwrocicZero()
        {
            // Arrange
            var kalkulator = new KalkulatorWynagrodzen(5000.0, "Umowa zlecenie", true);

            // Act
            double zaliczkaPit = kalkulator.ObliczZaliczkePit();

            // Assert
            Assert.AreEqual(0.00, zaliczkaPit, 0.0);
        }
    }
}