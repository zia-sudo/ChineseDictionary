using System;
using System.Linq;

namespace ChineseDictionary
{
    public class SearchCommand
    {
        public void Execute(string word)
        {
            // Vérification de l'entrée utilisateur
            if (string.IsNullOrWhiteSpace(word))
            {
                Console.WriteLine("Veuillez entrer un mot valide.");
                return;
            }

            // Normalisation de l'entrée utilisateur
            word = word.Trim();

            // Charger le document XML via XmlCache
            var doc = XmlCache.GetDocument();

            // Requête LINQ pour une correspondance exacte
            var exactMatch = doc.Descendants("word")
                .Where(w => string.Equals(w.Element("trad")?.Value, word, StringComparison.OrdinalIgnoreCase)
                         || string.Equals(w.Element("simp")?.Value, word, StringComparison.OrdinalIgnoreCase)
                         || string.Equals(w.Element("py")?.Value, word, StringComparison.OrdinalIgnoreCase))
                .Select(w => new
                {
                    Traditional = w.Element("trad")?.Value,
                    Simplified = w.Element("simp")?.Value,
                    Pinyin = w.Element("py")?.Value,
                    Translations = w.Element("trans")?.Elements("fr").Select(t => t.Value).ToList()
                });

            // Récupérer le premier résultat
            var result = exactMatch.FirstOrDefault();

            if (result != null)
            {
                // Affichage des informations sur le mot trouvé
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
                // Message pour un mot non trouvé
                Console.WriteLine($"Aucune information trouvée pour le mot : {word}.");
            }
        }
    }
}