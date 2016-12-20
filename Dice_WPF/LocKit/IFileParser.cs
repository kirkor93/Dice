using System.Text;

namespace LocKit
{
    public interface IFileParser
    {
        Encoding FileEncoding { get; set; }
        ParsedDictionary ResultDictionary { get; }

        void ParseFile(string path);
    }
}
