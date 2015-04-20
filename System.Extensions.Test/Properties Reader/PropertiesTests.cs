using NUnit.Framework;
using System.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions.Test.Checker
{
    [TestFixture]
    class PropertiesTests
    {
        [Test]
        public void ICanLoadAPropertiesFile()
        {
            var TestFile = Directory.GetCurrentDirectory() + @"\Test.properties";
            if (File.Exists(TestFile))
                File.Delete(TestFile);

            File.WriteAllLines(TestFile, new List<string> { "key1=value1" });

            using (var stream = new FileStream(TestFile, FileMode.Open))
            {
                var reader = new JavaProperties();
                reader.Load(stream);
                Assert.AreEqual("value1", reader.GetProperty("key1"));
            }
        }

        [Test]
        public void ICanWriteAPropertiesFile()
        {
            var TestFile = Directory.GetCurrentDirectory() + @"\Test.properties";
            if (File.Exists(TestFile))
                File.Delete(TestFile);

            File.WriteAllLines(TestFile, new List<string> { "key1=value1", "key2=value2" });

            var reader = new JavaProperties();
            using (var stream = new FileStream(TestFile, FileMode.Open))
            {
                reader.Load(stream);
                reader["key1"] = "sarasa";
            }

            using (var stream = new FileStream(TestFile, FileMode.Create))
            {
                reader.Store(stream, "");
            }

            var lines = File.ReadAllLines(TestFile);
            Assert.IsTrue(lines.Any(l => l.Contains("sarasa")));

            using (var stream = new FileStream(TestFile, FileMode.Open))
            {
                reader = new JavaProperties();
                reader.Load(stream);
                Assert.AreEqual("sarasa", reader.GetProperty("key1"));
                Assert.AreEqual("value2", reader.GetProperty("key2"));
            }



        }


    }
}
