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

            // Recherche insensible à la casse
            var result = from w in doc.Descendants("word")
                         where w.Element("trad")?.Value.Equals(word, StringComparison.OrdinalIgnoreCase) == true ||
                               w.Element("simp")?.Value.Equals(word, StringComparison.OrdinalIgnoreCase) == true ||
                               w.Element("py")?.Value.Contains(word, StringComparison.OrdinalIgnoreCase) == true
                         select w;

            var wordEntry = result.FirstOrDefault();

            if (wordEntry != null)
            {
                // Récupérer toutes les traductions
                var translations = wordEntry.Element("trans")?.Elements("fr");

                if (translations != null && translations.Any())
                {
                    Console.WriteLine($"Traductions pour {word} :");
                    foreach (var translation in translations)
                    {
                        Console.WriteLine($"- {translation.Value}");
                    }
                }
                else
                {
                    Console.WriteLine($"Aucune traduction trouvée pour le mot {word}.");
                }
            }
            else
            {
                Console.WriteLine($"Aucune information trouvée pour le mot {word}.");
            }
        }
    }
}