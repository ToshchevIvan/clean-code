namespace Markdown.Tokens
{
    public delegate bool TryRead(TokenReader reader, out IToken token);
}
