using System;
using System.Linq;
using System.Xml.Linq;

namespace ChineseDictionary
{
    public class GetSimplifiedCommand
    {
        // Exécuter la commande pour obtenir la forme simplifiée d'un mot
        public void Execute(string word)
        {
            XDocument doc = XDocument.Load("cfdict.xml");

            var result = from w in doc.Descendants("word")
                         where w.Element("trad")?.Value == word || w.Element("simp")?.Value == word
                         select w.Element("simp")?.Value;

            var simplified = result.FirstOrDefault();

            if (simplified != null)
            {
                Console.WriteLine($"La forme simplifiée de {word} est : {simplified}");
            }
            else
            {
                Console.WriteLine($"Aucune information simplifiée trouvée pour le mot {word}.");
            }
        }
    }
}