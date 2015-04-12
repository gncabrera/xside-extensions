using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions.Test.Checker
{
    [TestFixture]
    class PropertiesFileEncrypterTests
    {

        public string CurrentFolder { get { return Directory.GetCurrentDirectory() + @"\"; } }
        public string TestFile { get { return Directory.GetCurrentDirectory() + @"\PropertiesFileEncrypterTest.key"; } }
        public List<string> TestFileLines { get { return File.ReadAllLines(TestFile).ToList(); } }

       
        [SetUp]
        public void Setup()
        {
            if (File.Exists(TestFile))
                File.Delete(TestFile);
        }

        private void SetupTestFile(params string[] lines)
        {
            File.WriteAllLines(TestFile, lines);
        }

        [Test]
        public void SetupSucceeds()
        {
            Assert.Pass();
        }

        [Test]
        [ExpectedException(typeof(EncryptionException))]
        public void AnInexistingKeyFileBreaks()
        {
            new PropertiesFileEncrypter(TestFile);
        }

        [Test]
        public void AnEmptyKeyFileDoesNotFail()
        {
            SetupTestFile();
            var encrypter = new PropertiesFileEncrypter(TestFile);
            encrypter.LoadProperties();

            Assert.IsEmpty(encrypter.Properties);
        }

        [Test]
        public void AKeyFileWithValidPropertyReturnsValue()
        {
            SetupTestFile("prop1=foo", "prop2=bar");
            var encrypter = new PropertiesFileEncrypter(TestFile);
            encrypter.LoadProperties();
            Assert.IsTrue(encrypter.Properties.ContainsKey("prop1"));
            Assert.IsTrue(encrypter.Properties.ContainsKey("prop2"));

            Assert.AreEqual("foo", encrypter.Properties["prop1"]);
            Assert.AreEqual("bar", encrypter.Properties["prop2"]);
        }

        [Test]
        [ExpectedException(typeof(EncryptionException))]
        public void LinesWithInvalidFormatWillBreak()
        {
            SetupTestFile("prop1=foo", "no-formaaaa  ttt !!!");
            PropertiesFileEncrypter encrypter = null;
            try
            {
                encrypter = new PropertiesFileEncrypter(TestFile);
            }
            catch (Exception)
            {
                Assert.Fail("Unexpected exception was thrown");
            }
            encrypter.LoadProperties();
        }

        [Test]
        [ExpectedException(typeof(EncryptionException))]
        public void RepeatedKeysInFileWillBreak()
        {
            SetupTestFile("prop1=foo", "prop1=bar");
            PropertiesFileEncrypter encrypter = null;
            try
            {
                encrypter = new PropertiesFileEncrypter(TestFile);
            }
            catch (Exception)
            {
                Assert.Fail("Unexpected exception was thrown");
            }
            encrypter.LoadProperties();
        }

        [Test]
        public void EncryptingLinesWorks()
        {
            SetupTestFile("my-prop=foo", "my-second-prop=bar");
            var encrypter = new PropertiesFileEncrypter(TestFile);
            encrypter.LoadProperties();
            Assert.AreEqual(2, TestFileLines.Count);
            Assert.IsFalse(TestFileLines.Any(l => l.Contains("my-prop=foo")));
            Assert.IsFalse(TestFileLines.Any(l => l.Contains("my-second-prop=bar")));
        }

        [Test]
        public void EncryptedLinesArentReencrypted()
        {
            SetupTestFile("my-prop=foo", "my-second-prop=bar");
            var encrypter = new PropertiesFileEncrypter(TestFile);
            encrypter.LoadProperties();
            Assert.IsFalse(TestFileLines.Any(l => l.Contains("my-prop=foo")));
            Assert.IsFalse(TestFileLines.Any(l => l.Contains("my-second-prop=bar")));

            encrypter = new PropertiesFileEncrypter(TestFile);

            encrypter.LoadProperties();
            Assert.IsFalse(TestFileLines.Any(l => l.Contains("my-prop=foo")));
            Assert.IsFalse(TestFileLines.Any(l => l.Contains("my-second-prop=bar")));
            Assert.AreEqual("foo", encrypter.Properties["my-prop"]);
            Assert.AreEqual("bar", encrypter.Properties["my-second-prop"]);
        }

        [Test]
        public void WrongMasterPasswordWillHaveWrongValues()
        {
            SetupTestFile("my-prop=foo", "my-second-prop=bar");
            var encrypter = new PropertiesFileEncrypter(TestFile);
            encrypter = new PropertiesFileEncrypter(TestFile, "mypass");
            encrypter.LoadProperties();

            encrypter = new PropertiesFileEncrypter(TestFile, "wrong-pass");

            encrypter.LoadProperties();
            Assert.AreNotEqual("foo", encrypter.Properties["my-prop"]);
            Assert.AreNotEqual("bar", encrypter.Properties["my-second-prop"]);
        }

    }
}
