using NUnit.Framework;
using ConvertMarkdown;
using System;
using System.Collections.Generic;

namespace ConvertMarkdown.Tests
{
    class TokenizerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ConvertMarkdownBoldTest()
        {
            List<string> input = new List<string>()
            {
                "**Hello World** **Hello Germany**"
            };

            Assert.AreEqual(Markdown.Convert(input), "<p><strong>Hello World</strong> <strong>Hello Germany</strong></p>");
        }
    }
}
