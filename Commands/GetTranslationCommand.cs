using System;
using System.Linq;
using System.Xml.Linq;

namespace ChineseDictionary
{
    public class GetTranslationCommand
    {
        // Exécuter la commande pour obtenir la traduction d'un mot
        public void Execute(string word)
        {
            XDocument doc = XDocument.Load("./Data/cfdict.xml");

            var result = from w in doc.Descendants("word")
                         where w.Element("trad")?.Value == word || w.Element("simp")?.Value == word
                         select w.Element("trans")?.Elements("fr").Select(t => t.Value).ToList();

            var translations = result.FirstOrDefault();

            if (translations != null && translations.Any())
            {
                Console.WriteLine($"Les traductions en français pour {word} sont :");
                foreach (var translation in translations)
                {
                    Console.WriteLine($"  - {translation}");
                }
            }
            else
            {
                Console.WriteLine($"Aucune traduction trouvée pour le mot {word}.");
            }
        }
    }
}