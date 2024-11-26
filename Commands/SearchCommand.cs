using System;
using System.Linq;
using System.Xml.Linq;

namespace ChineseDictionary
{
    public class SearchCommand
    {
        // Exécuter la commande de recherche pour afficher toutes les informations du mot
        public void Execute(string word)
        {
            XDocument doc = XDocument.Load("./Data/cfdict.xml");

            var result = from w in doc.Descendants("word")
                         where w.Element("trad")?.Value == word || w.Element("simp")?.Value == word
                         select new
                         {
                             Traditional = w.Element("trad")?.Value,
                             Simplified = w.Element("simp")?.Value,
                             Pinyin = w.Element("py")?.Value,
                             Translations = w.Element("trans")?.Elements("fr").Select(t => t.Value).ToList()
                         };

            var data = result.FirstOrDefault();

            if (data != null)
            {
                Console.WriteLine("Informations pour le mot : " + word);
                Console.WriteLine("Forme Traditionnelle : " + data.Traditional);
                Console.WriteLine("Forme Simplifiée : " + data.Simplified);
                Console.WriteLine("Pinyin : " + data.Pinyin);
                Console.WriteLine("Traductions : ");
                foreach (var translation in data.Translations)
                {
                    Console.WriteLine("  - " + translation);
                }
            }
            else
            {
                Console.WriteLine("Aucune information trouvée pour ce mot.");
            }
        }
    }
}