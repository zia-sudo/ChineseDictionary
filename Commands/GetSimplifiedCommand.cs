using System;
using System.Linq;

namespace ChineseDictionary
{
    public class GetSimplifiedCommand
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

            // Requête LINQ pour trouver la forme simplifiée
            var result = doc.Descendants("word")
                .Where(w => string.Equals(w.Element("trad")?.Value, word, StringComparison.OrdinalIgnoreCase)
                         || string.Equals(w.Element("simp")?.Value, word, StringComparison.OrdinalIgnoreCase))
                .Select(w => w.Element("simp")?.Value);

            // Récupérer le premier résultat
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