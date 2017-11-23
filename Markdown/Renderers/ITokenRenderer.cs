using System;
using Markdown.Tokens;


namespace Markdown.Renderers
{
    public interface ITokenRenderer
    {
        IDisposable Emphasized();
        IDisposable Strong();
        void PushText(string text);
        string Render(IToken token);
    }
}
