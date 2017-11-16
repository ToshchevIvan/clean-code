﻿using System;
using System.Linq;
using Markdown.Languages;
using Markdown.Renderers;
using Markdown.Tokens;


namespace Markdown
{
    public class Md
    {
        public string RenderToHtml(string markdown)
        {
            if (markdown == null)
                throw new ArgumentNullException(nameof(markdown));
            var renderer = new HTMLTokenRenderer();
            var reader = new TokenReader(markdown);
            var token = new CompoundToken(reader.ReadTokens(MdLanguage.Readers));
            
            return renderer.Render(token);
        }
    }
}
