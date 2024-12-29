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

            word = word.Trim();
            var doc = XmlCache.GetDocument();

            var result = from w in doc.Descendants("word")
                         where string.Equals(w.Element("trad")?.Value, word, StringComparison.OrdinalIgnoreCase)
                            || string.Equals(w.Element("simp")?.Value, word, StringComparison.OrdinalIgnoreCase)
                         select w.Element("py")?.Value;

            var pinyin = result.FirstOrDefault();

            if (pinyin != null)
            {
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