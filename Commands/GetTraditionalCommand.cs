using System;
using System.Linq;
using System.Xml.Linq;

namespace ChineseDictionary
{
    public class GetTraditionalCommand
    {
        // Exécuter la commande pour obtenir la forme traditionnelle d'un mot
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
                         select w.Element("trad")?.Value;

            var traditional = result.FirstOrDefault();

            if (traditional != null)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8; // S'assurer que les caractères chinois s'affichent correctement
                Console.WriteLine($"La forme traditionnelle de {word} est : {traditional}");
            }
            else
            {
                Console.WriteLine($"Aucune information traditionnelle trouvée pour le mot {word}.");
            }
        }
    }
}