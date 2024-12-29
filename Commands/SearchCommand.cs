using System;
using System.Linq;

namespace ChineseDictionary
{
    public class SearchCommand
    {
        public void Execute(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
            {
                Console.WriteLine("Veuillez entrer un mot valide.");
                return;
            }

            word = word.Trim();
            var doc = XmlCache.GetDocument();

            // Requête LINQ avec correspondance exacte
            var exactMatch = doc.Descendants("word")
                .Where(w => string.Equals(w.Element("trad")?.Value, word, StringComparison.OrdinalIgnoreCase)
                         || string.Equals(w.Element("simp")?.Value, word, StringComparison.OrdinalIgnoreCase)
                         || string.Equals(w.Element("py")?.Value, word, StringComparison.OrdinalIgnoreCase))
                .Select(w => new
                {
                    Traditional = w.Element("trad")?.Value ?? "N/A",
                    Simplified = w.Element("simp")?.Value ?? "N/A",
                    Pinyin = w.Element("py")?.Value ?? "N/A",
                    Translations = w.Element("trans")?.Elements("fr").Select(t => t.Value).ToList() ?? new List<string>()
                }); // Point-virgule ajouté ici.

            var result = exactMatch.FirstOrDefault();

            if (result != null)
            {
                Console.WriteLine("Informations pour le mot : " + word);
                Console.WriteLine("Forme Traditionnelle : " + result.Traditional);
                Console.WriteLine("Forme Simplifiée : " + result.Simplified);
                Console.WriteLine("Pinyin : " + result.Pinyin);
                Console.WriteLine("Traductions : ");
                foreach (var translation in result.Translations)
                {
                    Console.WriteLine("  - " + translation);
                }
            }
            else
            {
                Console.WriteLine($"Aucune information trouvée pour le mot : {word}.");
            }
        }
    }
}