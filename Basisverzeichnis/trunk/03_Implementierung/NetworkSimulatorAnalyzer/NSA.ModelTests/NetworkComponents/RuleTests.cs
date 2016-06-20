using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSA.Model.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSA.Model.BusinessLogic.Tests
{
    [TestClass()]
    public class RuleTests
    {
        [TestMethod()]
        public void SingleEndNodeTest()
        {
            var rule = Rule.Parse("A|(Z)|TRUE", null);

            Assert.AreEqual(rule.StartNodeString, "A");
            CollectionAssert.AreEqual(rule.EndNodesString, new string[] { "Z" });
            Assert.AreEqual(rule.ExpectedResult, true);
            Assert.AreEqual(rule.SimulType, SimulationType.Simple);
        }

        [TestMethod()]
        public void SingleEndNodeWithOptionTest()
        {
            var rule = Rule.Parse("A|(Z)|{TTL:22}|TRUE", null);

            Assert.AreEqual(rule.StartNodeString, "A");
            CollectionAssert.AreEqual(rule.EndNodesString, new string[] { "Z" });

            int value;
            rule.Options.TryGetValue("TTL", out value);
            Assert.AreEqual(22, value);

            Assert.AreEqual(rule.ExpectedResult, true);
            Assert.AreEqual(rule.SimulType, SimulationType.Simple);
        }

        [TestMethod()]
        public void SingleEndNodeWithManyOptionTest()
        {
            var rule = Rule.Parse("A|(Zara,Klara)|{TTL:5,SSL:TRUE}|FALSE", null);

            Assert.AreEqual(rule.StartNodeString, "A");
            CollectionAssert.AreEqual(rule.EndNodesString, new string[] { "Zara", "Klara" });

            int value;
            rule.Options.TryGetValue("TTL", out value);
            Assert.AreEqual(5, value);
            rule.Options.TryGetValue("SSL", out value);
            Assert.AreEqual(1, value);

            Assert.AreEqual(rule.ExpectedResult, false);
            Assert.AreEqual(rule.SimulType, SimulationType.Simple);
        }

        [TestMethod()]
        public void ManyEndNodesTest()
        {
            var rule = Rule.Parse("A|(B,C,D,E,F,G,H,I,J,K,Mama,Nana)|FALSE", null);

            Assert.AreEqual(rule.StartNodeString, "A");
            CollectionAssert.AreEqual(rule.EndNodesString, new string[] { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "Mama", "Nana" });
            Assert.AreEqual(rule.ExpectedResult, false);
            Assert.AreEqual(rule.SimulType, SimulationType.Simple);
        }

        [TestMethod()]
        public void HasInternetTest()
        {
            var rule = Rule.Parse("A|HAS_INTERNET|TRUE", null);

            Assert.AreEqual(rule.StartNodeString, "A");
            Assert.AreEqual(rule.ExpectedResult, true);
            Assert.AreEqual(rule.SimulType, SimulationType.HasInternet);
        }



        [TestMethod()]
        public void OnlyTest()
        {
            var rule = Rule.Parse("A|ONLY(B,C,E,G)|{TTL:64}|FALSE", null);
            Assert.AreEqual(rule.StartNodeString, "A");
            CollectionAssert.AreEqual(rule.EndNodesString, new string[] { "B", "C", "E", "G" });

            int value;
            rule.Options.TryGetValue("TTL", out value);
            Assert.AreEqual(64, value);
            Assert.AreEqual(rule.ExpectedResult, false);
        }

        [TestMethod()]
        public void InvalidOptionTest()
        {
            bool valid_rule = true;
            try
            {
                var rule = Rule.Parse("A|(Z)|{TRRL:64}|TRUE", null);
            }
            catch (ArgumentException)
            {
                valid_rule = false;
            }

            Assert.IsFalse(valid_rule);
        }
    }
}