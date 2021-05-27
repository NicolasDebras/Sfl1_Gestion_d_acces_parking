using Microsoft.VisualStudio.TestTools.UnitTesting;
using APP_C_PARKING;

namespace Test_Uni
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]

        //test unitaire class Connexion 
        public void TestMethod_recup_info_badge()
        {
            Connexion co = new Connexion();
            co.recup_info_badge();
            Assert.AreEQual("");
        }
        public void TestMethod_verification()
        {
            string badge = "";

            Connexion co = new Connexion();
            co.verification(badge)
            Assert.AreEqual(true);
        }
    }
}
