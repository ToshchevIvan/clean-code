using System.Collections.Generic;


namespace Markdown.Renderers
{
    public interface ITokenRenderer
    {
        string Emphasized(params string[] text);
        string Strong(params string[] text);
    }
}
