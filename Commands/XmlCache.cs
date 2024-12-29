using System.Xml.Linq;

namespace ChineseDictionary
{
    public static class XmlCache
    {
        private static XDocument? _document;
        private static readonly string FilePath = "./Data/cfdict.xml";

        public static XDocument GetDocument()
        {
            if (_document == null)
            {
                _document = XDocument.Load(FilePath);
            }
            return _document;
        }

        public static void RefreshDocument()
        {
            _document = XDocument.Load(FilePath);
        }
    }
}