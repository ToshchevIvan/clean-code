using NUnit.Framework;
using FluentAssertions;
using Markdown;


namespace MdTests
{
    [TestFixture]
    public class Md_Should
    {
        private static Md md;
        
        [SetUp]
        public static void SetUp()
        {
            md = new Md();
        }

        [TestCase("", TestName = "When string is empty")]
        [TestCase("abc", TestName = "When string has no spaces")]
        [TestCase("some text", TestName = "When string has spaces")]
        public static void RenderPlainText(string text)
        {
            md.RenderToHtml(text)
                .Should()
                .Be(text);
        }

        [TestCase("_text_", ExpectedResult = "<em>text</em>")]
        [TestCase("_text _", ExpectedResult = "_text _")]
        [TestCase("_ text_", ExpectedResult = "_ text")]
        [TestCase("_simple_ text", ExpectedResult = "<em>simple</em> text")]
        [TestCase("some _simple_ text", ExpectedResult = "some <em>simple</em> text")]
        public static string RenderEmphasizedText(string text)
        {
            return md.RenderToHtml(text);
        }
    }
}
