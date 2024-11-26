using System;
using System.Linq;
using System.Xml.Linq;

namespace ChineseDictionary
{
    public class GetTraditionalCommand
    {
        // Exécuter la commande pour obtenir la forme traditionnelle d'un mot
        public void Execute(string word)
        {
            XDocument doc = XDocument.Load("cfdict.xml");

            var result = from w in doc.Descendants("word")
                         where w.Element("trad")?.Value == word || w.Element("simp")?.Value == word
                         select w.Element("trad")?.Value;

            var traditional = result.FirstOrDefault();

            if (traditional != null)
            {
                Console.WriteLine($"La forme traditionnelle de {word} est : {traditional}");
            }
            else
            {
                Console.WriteLine($"Aucune information traditionnelle trouvée pour le mot {word}.");
            }
        }
    }
}