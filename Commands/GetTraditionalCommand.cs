using System;
using System.Linq;

namespace ChineseDictionary
{
    public class GetTraditionalCommand
    {
        public void Execute(string word)
        {
            // Vérifier si le mot est null ou vide
            if (string.IsNullOrWhiteSpace(word))
            {
                Console.WriteLine("Veuillez entrer un mot valide.");
                return;
            }

            // Nettoyer et normaliser le mot
            word = word.Trim();

            // Obtenir le document XML via XmlCache
            var doc = XmlCache.GetDocument();

            // Requête LINQ pour récupérer la forme traditionnelle
            var result = doc.Descendants("word")
                .Where(w => string.Equals(w.Element("trad")?.Value, word, StringComparison.OrdinalIgnoreCase)
                         || string.Equals(w.Element("simp")?.Value, word, StringComparison.OrdinalIgnoreCase))
                .Select(w => w.Element("trad")?.Value);

            // Obtenir le premier résultat trouvé
            var traditional = result.FirstOrDefault();

            if (traditional != null)
            {
                // S'assurer que les caractères chinois sont bien affichés
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine($"La forme traditionnelle de {word} est : {traditional}");
            }
            else
            {
                Console.WriteLine($"Aucune information traditionnelle trouvée pour le mot {word}.");
            }
        }
    }
}