using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ScintillaNet;

namespace Einfall.Editor
{
    [TestFixture]
    public class Test_CodeTransformer
    {
        Scintilla _s = new Scintilla();
        [Test]
        public void TestGetFirstLineEmpty()
        {

            CodeTransformer ct = new CodeTransformer();

            Assert.AreEqual("", ct.GetTextFromStartTo("b", ""));
        }

        [Test]
        public void TestGetTextFromWhenIdNotPresent()
        {
            CodeTransformer ct = new CodeTransformer();
       
            string text = "all text should be returned unchanged";
            Assert.AreEqual(text, ct.GetTextFromStartTo("?", text));
        }

        [Test]
        public void GetFirstLineFromText()
        {
            CodeTransformer ct = new CodeTransformer();

            string text = "LineOne\r\nLineTwo";
            Assert.AreEqual("LineOne", ct.GetTextFromStartTo("\r\n", text));
        }

        [Test]
        public void FindFirstLineIndex()
        {
            CodeTransformer ct = new CodeTransformer();

            Assert.IsTrue(ct.WhatLineIsIndexOn(0, "_") == 0);
        }

        [Test]
        public void FindSecondLineIndex()
        {
            CodeTransformer ct = new CodeTransformer();

            Assert.IsTrue(ct.WhatLineIsIndexOn(5, "\r\n_testestestest") == 1);
        }
    }
}
