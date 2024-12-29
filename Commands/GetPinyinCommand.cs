using System;
using System.Linq;

namespace ChineseDictionary
{
    public class GetPinyinCommand
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

            // Accéder au document XML via XmlCache
            var doc = XmlCache.GetDocument();

            // Requête LINQ pour trouver le pinyin
            var result = doc.Descendants("word")
                .Where(w => string.Equals(w.Element("trad")?.Value, word, StringComparison.OrdinalIgnoreCase)
                         || string.Equals(w.Element("simp")?.Value, word, StringComparison.OrdinalIgnoreCase))
                .Select(w => w.Element("py")?.Value);

            // Obtenir le premier résultat
            var pinyin = result.FirstOrDefault();

            if (pinyin != null)
            {
                // S'assurer que la sortie gère correctement les caractères chinois
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine($"Le pinyin de {word} est : {pinyin}");
            }
            else
            {
                Console.WriteLine($"Aucune information de pinyin trouvée pour le mot {word}.");
            }
        }
    }
}