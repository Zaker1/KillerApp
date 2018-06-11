using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twooter.Models;
using Twooter.Repos;
using Twooter.Interfaces;
using Twooter.Context;
using Twooter.ViewModels;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Linq;

namespace UnitTestTwooter
{
    [TestClass]
    public class TwooterTest
    {
        [TestMethod]
        public void TestMaakAccountAan()
        {
            AccountContext context = new AccountContext();

            Registreren registereer = new Registreren
            {
                Email = "unittest2@mail.com",
                Wachtwoord = "wachtwoord",
                Gebruikersnaam = "unittest"
            };

            try
            {
                context.Insert(registereer);
            }
            catch (DuplicateNameException)
            {

            }

            Account account = new Account
            {
                Email = "unittest2@mail.com",
                Wachtwoord = "wachtwoord",
            };
            Account accountOphalen = context.Select(account);
            Assert.IsNotNull(accountOphalen);
        }
        [TestMethod]
        public void TestNiet2ZelfdeAccounts()
        {
            AccountContext context = new AccountContext();

            Registreren registereer = new Registreren
            {
                Email = "unittest@mail.com",
                Wachtwoord = "wachtwoord",
                Gebruikersnaam = "unittest"
            };

            Assert.ThrowsException<DuplicateNameException>(() => context.Insert(registereer));
        }
        [TestMethod]
        public void TestGeenTwootMetNullBericht()
        {
            TwootMSSQLContext context = new TwootMSSQLContext();

            Twoot twoot = new Twoot
            {
                Bericht = null,
                date = DateTime.Now,
                ProfielID = 1019
            };

            Assert.ThrowsException<Exception>(() => context.InsertTwoot(twoot));
        }
        [TestMethod]
        public void TestJuisteTwootsOphalen()
        {
            ProfielMSSQLContext profielContext = new ProfielMSSQLContext();
            TijdlijnMSSQLContext tijdlijnContext = new TijdlijnMSSQLContext();

            Profiel profiel = new Profiel
            {
                Account = new Account { ID = 17 }
            };
            Tijdlijn tijdlijn = new Tijdlijn(profiel);

            tijdlijn.Twoots = tijdlijnContext.SelectTwoot(tijdlijn);
            tijdlijn.Vrienden = tijdlijnContext.SelectVriend(tijdlijn);

            foreach (Twoot twoot in tijdlijn.Twoots)
            {
                Profiel vriend = tijdlijn.Vrienden.FirstOrDefault(m => m.ID == twoot.ID);
                Assert.IsTrue(twoot.ProfielID == tijdlijn.Profiel.ID || vriend != null);
            }
        }
        [TestMethod]
        public void TestVolgen()
        {
            ProfielMSSQLContext profielContext = new ProfielMSSQLContext();
            TijdlijnMSSQLContext tijdlijnContext = new TijdlijnMSSQLContext();
            Profiel volgendProfiel = new Profiel
            {
                ID = 1020
            };
            Profiel profiel = new Profiel
            {
                ID = 1019
            };
            
            Tijdlijn tijdlijn = new Tijdlijn(profiel);

            profielContext.Volg(volgendProfiel.ID, profiel.ID);
            tijdlijn.Vrienden = tijdlijnContext.SelectVriend(tijdlijn);

            Profiel vriendProfiel = tijdlijn.Vrienden.FirstOrDefault(m => m.ID == volgendProfiel.ID);

            Assert.IsNotNull(vriendProfiel);
        }
    }
}