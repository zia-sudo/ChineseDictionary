using System;
using System.Linq;

namespace ChineseDictionary
{
    public class GetTranslationCommand
    {
        public void Execute(string word)
        {
            var doc = XmlCache.GetDocument();

            var result = from w in doc.Descendants("word")
                         where w.Element("trad")?.Value.Equals(word, StringComparison.OrdinalIgnoreCase) == true ||
                               w.Element("simp")?.Value.Equals(word, StringComparison.OrdinalIgnoreCase) == true ||
                               w.Element("py")?.Value.Contains(word, StringComparison.OrdinalIgnoreCase) == true
                         select w;

            var wordEntry = result.FirstOrDefault();

            if (wordEntry != null)
            {
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