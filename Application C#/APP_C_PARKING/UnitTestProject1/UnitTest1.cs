using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using APP_C_PARKING;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        string badge = "4851274";
        string chaine = "CAPS                     Corentin                 4851274";

        string badge_delete = " ";
                        

        [TestMethod]
        public void TestMethod_verification()
        {
            Connexion co = new Connexion();
            bool n = co.verification(badge);
            Assert.AreEqual(n, false);
        }

        [TestMethod]

        public void TestMethod_recup_info_badge()
        {
            Connexion co = new Connexion();
            string test = co.recup_info_badge();
            Assert.AreEqual(test, chaine);
        }

        [TestMethod]

        public void TestMethod_Creation_user()
        {
            Connexion co = new Connexion();
            co.add_user("debras", "nicolas", "555555", "user");
            bool n = co.verification("555555");
            Assert.AreEqual(n, false);
        }



        [TestMethod]

        public void TestMethod_delete_user()
        {
            Connexion co = new Connexion();
            co.delete_user("555555");
            bool n = co.verification("555555");
            Assert.AreEqual(n, true);

        }
    }

}
