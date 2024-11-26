using System;
using System.Linq;
using System.Xml.Linq;

namespace ChineseDictionary
{
    public class GetPinyinCommand
    {
        // Exécuter la commande pour obtenir le pinyin d'un mot
        public void Execute(string word)
        {
            XDocument doc = XDocument.Load("cfdict.xml");

            var result = from w in doc.Descendants("word")
                         where w.Element("trad")?.Value == word || w.Element("simp")?.Value == word
                         select w.Element("py")?.Value;

            var pinyin = result.FirstOrDefault();

            if (pinyin != null)
            {
                Console.WriteLine($"Le pinyin de {word} est : {pinyin}");
            }
            else
            {
                Console.WriteLine($"Aucune information de pinyin trouvée pour le mot {word}.");
            }
        }
    }
}