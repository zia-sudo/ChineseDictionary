using System;
using System.Linq;

namespace ChineseDictionary
{
    public class GetSimplifiedCommand
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