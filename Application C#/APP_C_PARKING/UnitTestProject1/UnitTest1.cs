using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using APP_C_PARKING;

namespace UnitTestProject1
{
    [TestClass]

    /*
     * Méthode de test correspondant à La classe Connexion
     */
    public class UnitTest1
    {
        string badge = "4851274";
        string chaine = "CAPS                     Corentin                 4851274";

        Connexion co = new Connexion();

        [TestMethod]
        public void TestMethod_verification()
        {
            bool n = co.verification(badge);
            Assert.AreEqual(n, false);
        }

        [TestMethod]

        public void TestMethod_recup_info_badge()
        {
            string test = co.recup_info_badge();
            Assert.AreEqual(test, chaine);
        }

        [TestMethod]

        public void TestMethod_Creation_user()
        {
            co.add_user("debras", "nicolas", "555555", "user");
            bool n = co.verification("555555");
            Assert.AreEqual(n, false);
        }

        [TestMethod]

        public void TestMethod_delete_user()
        {
            co.delete_user("555555");
            bool n = co.verification("555555");
            Assert.AreEqual(n, true);

        }
    }

}
