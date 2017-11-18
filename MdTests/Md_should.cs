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
        [TestCase("_simple_ text", ExpectedResult = "<em>simple</em> text")]
        [TestCase("some _simple_ text", ExpectedResult = "some <em>simple</em> text")]
        public static string RenderEmphasizedText(string text)
        {
            return md.RenderToHtml(text);
        }
        
        [TestCase("__text__", ExpectedResult = "<strong>text</strong>")]
        [TestCase("__simple__ text", ExpectedResult = "<strong>simple</strong> text")]
        [TestCase("some __simple__ text", ExpectedResult = "some <strong>simple</strong> text")]
        public static string RenderStrongText(string text)
        {
            return md.RenderToHtml(text);
        }
        
        [TestCase("_emphasized_ and __strong__", ExpectedResult = "<em>emphasized</em> and <strong>strong</strong>")]
        [TestCase("__emphasized _inside_ strong__", ExpectedResult = "<strong>emphasized <em>inside</em> strong</strong>")]
        [TestCase("_strong __inside__ emphasized_", ExpectedResult = "<em>strong _</em>inside__ emphasized_")]
        public static string RenderCombinedText(string text)
        {
            return md.RenderToHtml(text);
        }
        
        [TestCase("\\_text_", ExpectedResult = "_text_")]
        [TestCase("_text\\_", ExpectedResult = "_text_")]
        [TestCase("_text1_2", ExpectedResult = "_text1_2")]
        [TestCase("_1text_", ExpectedResult = "_1text_")]
        [TestCase("_text _", ExpectedResult = "_text _")]
        [TestCase("_ text_", ExpectedResult = "_ text_")]
        [TestCase("__text __", ExpectedResult = "__text __")]
        [TestCase("__ text__", ExpectedResult = "__ text__")]
        public static string NotRenderEscapedOrInvalidSequences(string text)
        {
            return md.RenderToHtml(text);
        }
        
        [TestCase("_\\_text__", ExpectedResult = "<em>_text</em>_")]
        [TestCase("\\__text__", ExpectedResult = "_<em>text</em>_")]
        [TestCase("_text\\\\_", ExpectedResult = "<em>text\\</em>")]
        public static string RenderToughCases(string text)
        {
            return md.RenderToHtml(text);
        }
    }
}
