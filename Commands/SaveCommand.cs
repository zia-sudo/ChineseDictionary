#nullable enable
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace ChineseDictionary
{
    public class SaveCommand
    {
        public void Execute()
        {
            Console.Write("Entrez le mot que vous souhaitez enregistrer : ");
            string? word = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(word))
            {
                Console.WriteLine("Le mot ne peut pas être vide.");
                return;
            }

            var doc = XmlCache.GetDocument();

            var exactMatch = doc.Descendants("word")
                .Where(w => string.Equals(w.Element("trad")?.Value, word, StringComparison.OrdinalIgnoreCase)
                    || string.Equals(w.Element("simp")?.Value, word, StringComparison.OrdinalIgnoreCase)
                    || string.Equals(w.Element("py")?.Value, word, StringComparison.OrdinalIgnoreCase))
                .Select(w => new
                {
                    Traditional = w.Element("trad")?.Value ?? "Inconnu",
                    Simplified = w.Element("simp")?.Value ?? "Inconnu",
                    Pinyin = w.Element("py")?.Value ?? "Inconnu",
                    Translations = w.Element("trans")?.Elements("fr").Select(t => t.Value).ToList() ?? new List<string>()
                });

            var data = exactMatch.FirstOrDefault();

            if (data == null)
            {
                Console.WriteLine("Aucune information trouvée pour ce mot.");
                return;
            }

            Console.Write("Quel format voulez-vous utiliser pour enregistrer ? (xml, txt, json) : ");
            string? fileType = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrEmpty(fileType))
            {
                Console.WriteLine("Format non spécifié.");
                return;
            }

            string filePath;

            switch (fileType)
            {
                case "xml":
                    filePath = $"{word}_result.xml";
                    var element = new XElement("word",
                        new XElement("trad", data.Traditional),
                        new XElement("simp", data.Simplified),
                        new XElement("py", data.Pinyin),
                        new XElement("trans", data.Translations.Select(t => new XElement("fr", t)))
                    );
                    var docXml = new XDocument(new XElement("words", element));
                    docXml.Save(filePath);
                    break;

                case "txt":
                    filePath = $"{word}_result.txt";
                    File.WriteAllLines(filePath, new[]
                    {
                        $"Mot : {word}",
                        $"Forme traditionnelle : {data.Traditional}",
                        $"Forme simplifiée : {data.Simplified}",
                        $"Pinyin : {data.Pinyin}",
                        "Traductions :",
                    }.Concat(data.Translations.Select(t => $"- {t}")));
                    break;

                case "json":
                    filePath = $"{word}_result.json";
                    var jsonData = new
                    {
                        data.Traditional,
                        data.Simplified,
                        data.Pinyin,
                        data.Translations
                    };
                    File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonData, Formatting.Indented));
                    break;

                default:
                    Console.WriteLine("Format non supporté. Veuillez choisir entre 'xml', 'txt' ou 'json'.");
                    return;
            }

            Console.WriteLine($"Résultats sauvegardés dans {filePath}");
            XmlCache.RefreshDocument();
        }
    }
}