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
            // Demander le mot à enregistrer
            Console.Write("Entrez le mot que vous souhaitez enregistrer : ");
            string? word = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(word))
            {
                Console.WriteLine("Le mot ne peut pas être vide.");
                return;
            }

            // Charger le document XML via XmlCache
            var doc = XmlCache.GetDocument();

            // Requête LINQ pour trouver le mot
            var exactMatch = doc.Descendants("word")
                .Where(w => string.Equals(w.Element("trad")?.Value, word, StringComparison.OrdinalIgnoreCase)
                         || string.Equals(w.Element("simp")?.Value, word, StringComparison.OrdinalIgnoreCase)
                         || string.Equals(w.Element("py")?.Value, word, StringComparison.OrdinalIgnoreCase))
                .Select(w => new
                {
                    Traditional = w.Element("trad")?.Value,
                    Simplified = w.Element("simp")?.Value,
                    Pinyin = w.Element("py")?.Value,
                    Translations = w.Element("trans")?.Elements("fr").Select(t => t.Value).ToList()
                });

            // Récupérer le premier résultat
            var data = exactMatch.FirstOrDefault();

            if (data != null)
            {
                Console.Write("Quel format voulez-vous utiliser pour enregistrer ? (xml, txt, json) : ");
                string? fileType = Console.ReadLine()?.Trim().ToLower();

                string filePath;

                if (fileType == "xml")
                {
                    // Sauvegarder au format XML
                    XElement element = new XElement("word",
                        new XElement("trad", data.Traditional),
                        new XElement("simp", data.Simplified),
                        new XElement("py", data.Pinyin),
                        new XElement("trans", data.Translations.Select(t => new XElement("fr", t)))
                    );

                    filePath = $"{word}_result.xml";
                    XElement root = new XElement("words", element);
                    XDocument newDoc = new XDocument(root);
                    newDoc.Save(filePath);
                    Console.WriteLine($"Résultats sauvegardés dans {filePath}");
                }
                else if (fileType == "txt")
                {
                    // Sauvegarder au format TXT
                    filePath = $"{word}_result.txt";
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        writer.WriteLine($"Informations pour le mot : {word}");
                        writer.WriteLine($"Forme Traditionnelle : {data.Traditional}");
                        writer.WriteLine($"Forme Simplifiée : {data.Simplified}");
                        writer.WriteLine($"Pinyin : {data.Pinyin}");
                        writer.WriteLine("Traductions : ");
                        foreach (var translation in data.Translations)
                        {
                            writer.WriteLine($"  - {translation}");
                        }
                    }
                    Console.WriteLine($"Résultats sauvegardés dans {filePath}");
                }
                else if (fileType == "json")
                {
                    // Sauvegarder au format JSON
                    filePath = $"{word}_result.json";
                    var jsonData = new
                    {
                        Traditional = data.Traditional,
                        Simplified = data.Simplified,
                        Pinyin = data.Pinyin,
                        Translations = data.Translations
                    };

                    File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonData, Formatting.Indented));
                    Console.WriteLine($"Résultats sauvegardés dans {filePath}");
                }
                else
                {
                    Console.WriteLine("Format non supporté. Veuillez choisir entre 'xml', 'txt' ou 'json'.");
                    return;
                }

                // Rafraîchir le cache XML après la sauvegarde
                XmlCache.RefreshDocument();
            }
            else
            {
                Console.WriteLine("Aucune information trouvée pour ce mot.");
            }
        }
    }
}