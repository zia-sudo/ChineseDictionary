using System;
using System.Linq;
using System.Xml.Linq;

namespace ChineseDictionary
{
    public class GetSimplifiedCommand
    {
        // Exécuter la commande pour obtenir la forme simplifiée d'un mot
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

            // Recherche insensible à la casse
            var result = from w in doc.Descendants("word")
                         where string.Equals(w.Element("trad")?.Value, word, StringComparison.OrdinalIgnoreCase)
                            || string.Equals(w.Element("simp")?.Value, word, StringComparison.OrdinalIgnoreCase)
                         select w.Element("simp")?.Value;

            var simplified = result.FirstOrDefault();

            if (simplified != null)
            {
                Console.WriteLine($"La forme simplifiée de {word} est : {simplified}");
            }
            else
            {
                Console.WriteLine($"Aucune information simplifiée trouvée pour le mot {word}.");
            }
        }
    }
}