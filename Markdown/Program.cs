using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Markdown.Renderers;
using Markdown.Tokens;


namespace Markdown
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new StrongTextToken(new List<IToken>
            {
                new PlainTextToken("abcde "),
                new EmphasizedTextToken("fg"),
                new PlainTextToken(" hklm"),
                new StrongTextToken(new List<IToken>(
                        new List<IToken>
                        {
                            new PlainTextToken(" oprst "),
                            new EmphasizedTextToken("xyz")
                        }
                    ))
            });

            Console.WriteLine(new HTMLTokenRenderer().Render(t));
        }
    }
}
