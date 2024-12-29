using System;
using System.Linq;

namespace ChineseDictionary
{
    public class GetTranslationCommand
    {
        public void Execute(string word)
        {
            // Vérification de l'entrée utilisateur
            if (string.IsNullOrWhiteSpace(word))
            {
                Console.WriteLine("Veuillez entrer un mot valide.");
                return;
            }

            // Normalisation du mot
            word = word.Trim();

            // Charger le document XML via XmlCache
            var doc = XmlCache.GetDocument();

            // Requête LINQ pour trouver le mot correspondant
            var result = doc.Descendants("word")
                .Where(w => string.Equals(w.Element("trad")?.Value, word, StringComparison.OrdinalIgnoreCase)
                         || string.Equals(w.Element("simp")?.Value, word, StringComparison.OrdinalIgnoreCase)
                         || w.Element("py")?.Value.Contains(word, StringComparison.OrdinalIgnoreCase) == true)
                .Select(w => w);

            // Récupérer le premier résultat trouvé
            var wordEntry = result.FirstOrDefault();

            if (wordEntry != null)
            {
                // Récupérer les traductions
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