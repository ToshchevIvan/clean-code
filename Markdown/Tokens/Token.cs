using Markdown.Renderers;


namespace Markdown.Tokens
{
    public abstract class Token : IToken
    {
        public string Content { get; }

        public Token(string content)
        {
            Content = content;
        }

        public abstract string Render(ITokenRenderer renderer);
    }
}
