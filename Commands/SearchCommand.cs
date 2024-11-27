using System;
using System.Linq;
using System.Xml.Linq;

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

            // Normalisation de l'entrée utilisateur
            word = word.Trim();

            // Charger le fichier XML
            XDocument doc = XDocument.Load("./Data/cfdict.xml");

            // Recherche stricte (exacte) insensible à la casse
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
            else
            {
                Console.WriteLine("Aucune correspondance exacte trouvée pour le mot : " + word);
                Console.WriteLine("Recherche de mots similaires...");

                var partialMatches = from w in doc.Descendants("word")
                                     where (w.Element("trad")?.Value.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                                        || (w.Element("simp")?.Value.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                                        || (w.Element("py")?.Value.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
                                     select new
                                     {
                                         Traditional = w.Element("trad")?.Value,
                                         Simplified = w.Element("simp")?.Value,
                                         Pinyin = w.Element("py")?.Value,
                                         Translations = w.Element("trans")?.Elements("fr").Select(t => t.Value).ToList()
                                     };

                if (partialMatches.Any())
                {
                    Console.WriteLine("Résultats similaires :");
                    foreach (var match in partialMatches)
                    {
                        Console.WriteLine("----");
                        Console.WriteLine("Forme Traditionnelle : " + match.Traditional);
                        Console.WriteLine("Forme Simplifiée : " + match.Simplified);
                        Console.WriteLine("Pinyin : " + match.Pinyin);
                        Console.WriteLine("Traductions : ");
                        foreach (var translation in match.Translations)
                        {
                            Console.WriteLine("  - " + translation);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Aucun mot similaire trouvé dans le dictionnaire.");
                }
            }
        }
    }
}