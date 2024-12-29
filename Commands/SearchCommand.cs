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

            var exactMatch = from w in doc.Descendants("word")
                             where string.Equals(w.Element("trad")?.Value, word, StringComparison.OrdinalIgnoreCase)
                                || string.Equals(w.Element("simp")?.Value, word, StringComparison.OrdinalIgnoreCase)
                                || string.Equals(w.Element("py")?.Value, word, StringComparison.OrdinalIgnoreCase)
                             select new
                             {
                                 Traditional = w.Element("trad")?.Value,
                                 Simplified = w.Element("simp")?.Value,
                                 Pinyin = w.Element("py")?.Value,
                                 Translations = w.Element("trans")?.Elements("fr").Select(t => t.Value).ToList()
                             };

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
        }
    }
}