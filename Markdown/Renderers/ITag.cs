using System;


namespace Markdown.Renderers
{
    public interface ITag : IDisposable
    {
        string OpeningTag { get; }
        string ClosingTag { get; }
    }
}
