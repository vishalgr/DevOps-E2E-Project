using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevOps.Tests.Production;

namespace DevOps.Tests.ProductionTests
{
    [TestFixture]
    public class Tester
    {
        Helper help = new Helper();
        randomnumbergenerator generator = new randomnumbergenerator();

        [Test]
        public void TestThatAdds_AlwaysPass()
        {

            int r = help.add(20, 30);
            Assert.AreEqual(50, r);
        }
        [Test]
        public void TestThatAdds_AlwaysPass_2()
        {

            int r = help.add(10, 40);
            Assert.AreEqual(50, r);
        }
        [Test]
        public void TestThatAddsFails()
        {

            int r = help.add(50, 30);
            Assert.AreEqual(70, r);
        }
        [Test]
        public void TestThatAddsFails_2()
        {

            int r = help.add(50, 30);
            Assert.AreEqual(60, r);
        }
        [Test]
        public void TestThatCheckesEqualAlwaysFails_1()
        {

            int r = help.add(50, 30);
            Assert.AreEqual(70, r);
        }
        [Test]
        public void TestThatCheckesEqualAlwaysPass_1()
        {

            int r = help.add(50, 30);
            Assert.AreEqual(80, r);
        }
        [Test]
        public void TestThatCheckesEqualAlwaysFails_2()
        {

            int r = help.add(50, 60);
            Assert.AreEqual(80, r);
        }
        [Test]
        public void TestThatCheckesEqualAlwaysPass_2()
        {
            int r = help.add(30, 30);
            Assert.AreEqual(60, r);
        }
        [Test]
        public void TestThatCheckesEqualAlwaysPass_3()
        {

            int r = help.add(90, 30);
            Assert.AreEqual(120, r);
        }
        [Test]
        public void TestThatCheckesEqualAlwaysPass_4()
        {

            int r = help.add(160, 30);
            Assert.AreEqual(190, r);
        }

        [Test]
        public void TestThatPassEvenNumberOfTime()
        {
            int ra = generator.RandomNumber();
            int z = ra % 2;
            Assert.AreEqual(0, z);
        }
        [Test]
        public void TestThatPassOddNumberOfTime()
        {
            int ra = generator.RandomNumber();
            int z = ra % 2;
            Assert.AreEqual(1, z);
        }

        [Test]
        public void TestPassWhenNumberIsGreater()
        {
            int ra = generator.RandomNumber();
            Assert.That(ra, Is.GreaterThan(15));
        }
        [Test]
        public void TestWithRandomNumberEqualTo()
        {
            int ra = generator.RandomNumber();
            Assert.That(ra, Is.EqualTo(15));
        }
        [Test]
        public void TestWithRandomNumberLessThanOrEqualTo()
        {
            int ra = generator.RandomNumber();
            Assert.That(ra, Is.LessThanOrEqualTo(20));
        }

        [Test]
        public void TestWithRandomNumberNotEqualTo()
        {
            int ra = generator.RandomNumber();
            Assert.That(ra, Is.Not.EqualTo(30));
        }
        [Test]
        public void TestWithRandomNumberLessThan()
        {
            int ra = generator.RandomNumber();
            Assert.That(ra, Is.LessThan(40));
        }

        [Test]
        public void TestWithRandomNumberEqualTo_2()
        {
            int r = help.add(50, 30);
            Assert.AreEqual(80, r);
        }
        [Test]
        public void TestWithRandomNumberEqualTo_3()
        {
            int r = help.add(90, 10);
            Assert.AreEqual(100, r);
        }
        [Test]
        public void TestWithRandomNumberNotNull()
        {
            int ra = generator.RandomNumber();
            Assert.That(ra, Is.Not.Null);

        }
        [Test]
        public void TestWithRandomNumberNegative()
        {
            int ra = generator.RandomNumber();
            Assert.That(ra, Is.Negative);
        }
        [Test]
        public void TestWithRandomNumberPositive()
        {
            int ra = generator.RandomNumber();
            Assert.That(ra, Is.Positive);
            Console.WriteLine(ra);
        }
        [Test]
        public void TestWithRandomNumberLessThanOrEqualTo_2()
        {
            int ra = generator.RandomNumber();
            Assert.That(ra, Is.LessThanOrEqualTo(35));
        }
        [Test]
        public void TestWithRandomNumberGreaterThanOrEqualTo()
        {
            int ra = generator.RandomNumber();
            Assert.That(ra, Is.GreaterThanOrEqualTo(45));
        }
        [Test]
        public void TestWithRandomNumberLessThanOrEqualTo_3()
        {
            int ra = generator.RandomNumber();
            Assert.That(ra, Is.LessThanOrEqualTo(25));
        }
    }
}

