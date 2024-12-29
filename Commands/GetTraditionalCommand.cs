using System;
using System.Linq;

namespace ChineseDictionary
{
    public class GetTraditionalCommand
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
                         select w.Element("trad")?.Value;

            var traditional = result.FirstOrDefault();

            if (traditional != null)
            {
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