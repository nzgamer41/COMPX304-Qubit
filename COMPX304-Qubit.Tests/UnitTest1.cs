using NUnit.Framework;
using COMPX304_Qubit;

namespace COMPX304_Qubit.Tests
{
    public class Tests
    {
        private Qubit _qubit;
        [SetUp]
        public void Setup()
        {
            //inits a new qubit
            _qubit = new Qubit(0, 0);
        }

        [Test]
        public void QubitExceptionTest()
        {
            //It should throw an exception when values are set that are not 0 or 1.
            Assert.Throws<QubitPolarizationInvalidException>(delegate { _qubit.set(2, 2); });
        }

        [Test]
        public void QubitFailTest()
        {
            //It however SHOULDN'T throw an exception if the values are correct
            Assert.DoesNotThrow(delegate { _qubit.set(1, 1); });
        }

        [Test]
        public void QubitPolarTest1()
        {
            //This should return the value if the polarization is equal
            _qubit.set(1, 0);
            Assert.AreEqual(1, _qubit.measure(0));
        }

        [Test]
        public void EncryptionTest()
        {
            string encData = XOR.Encrypt("I'm a big four-eyed lame-o and I wear the same stupid sweater everyday and...", "THE SPRINGFIELD RIVER!");
            string decData = XOR.Decrypt(encData, "THE SPRINGFIELD RIVER!");
            Assert.AreEqual("I'm a big four-eyed lame-o and I wear the same stupid sweater everyday and...", decData);
        }
    }
}