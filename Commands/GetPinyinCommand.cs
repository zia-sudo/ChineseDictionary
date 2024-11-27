using System;
using System.Linq;
using System.Xml.Linq;

namespace ChineseDictionary
{
    public class GetPinyinCommand
    {
        // Exécuter la commande pour obtenir le pinyin d'un mot
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
                         select w.Element("py")?.Value;

            var pinyin = result.FirstOrDefault();

            if (pinyin != null)
            {
                Console.OutputEncoding = System.Text.Encoding.UTF8; // S'assurer que la sortie gère correctement les caractères chinois
                Console.WriteLine($"Le pinyin de {word} est : {pinyin}");
            }
            else
            {
                Console.WriteLine($"Aucune information de pinyin trouvée pour le mot {word}.");
            }
        }
    }
}