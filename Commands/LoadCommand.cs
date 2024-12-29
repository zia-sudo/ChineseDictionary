#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ChineseDictionary
{
    public class LoadCommand
    {
        private const string DefaultFolder = "Data"; // Dossier par défaut pour les fichiers

        public void Execute(string filePath)
        {
            // Si le chemin est relatif, ajoutez le dossier par défaut
            if (!Path.IsPathRooted(filePath))
            {
                filePath = Path.Combine(DefaultFolder, filePath);
            }

            Console.WriteLine($"Chemin absolu interprété : {Path.GetFullPath(filePath)}");

            // Vérifiez si le fichier existe
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Erreur : Le fichier '{filePath}' est introuvable. Chemin absolu : {Path.GetFullPath(filePath)}");
                return;
            }

            Console.WriteLine($"Chargement du fichier : {Path.GetFullPath(filePath)}");
            string extension = Path.GetExtension(filePath).ToLower();

            try
            {
                if (extension == ".csv")
                {
                    LoadCsv(filePath);
                }
                else if (extension == ".txt")
                {
                    LoadText(filePath);
                }
                else
                {
                    Console.WriteLine("Erreur : Seuls les fichiers .csv et .txt sont supportés.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement du fichier : {ex.Message}");
            }
        }

        private void LoadCsv(string filePath)
        {
            Console.Write("Veuillez entrer le délimiteur utilisé dans le fichier (par défaut ',') : ");
            string? delimiter = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(delimiter)) delimiter = ",";

            List<XElement> words = new List<XElement>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string? headerLine = reader.ReadLine();
                if (headerLine == null)
                {
                    Console.WriteLine("Erreur : Le fichier CSV est vide.");
                    return;
                }

                string[] headers = headerLine.Split(delimiter);
                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    if (line == null) continue;

                    string[] parts = line.Split(delimiter);
                    if (parts.Length < headers.Length)
                    {
                        Console.WriteLine($"Erreur : Ligne ignorée (colonnes insuffisantes) : {line}");
                        continue;
                    }

                    XElement wordElement = new XElement("word",
                        new XElement("trad", parts.ElementAtOrDefault(0) ?? ""),
                        new XElement("simp", parts.ElementAtOrDefault(1) ?? ""),
                        new XElement("py", parts.ElementAtOrDefault(2) ?? ""),
                        new XElement("trans",
                            (parts.ElementAtOrDefault(3) ?? "")
                            .Split(';', StringSplitOptions.RemoveEmptyEntries)
                            .Select(translation => new XElement("fr", translation.Trim())))
                    );

                    words.Add(wordElement);
                }
            }

            SaveWordsToXml(words);
        }

        private void LoadText(string filePath)
        {
            Console.WriteLine("Lecture du fichier texte...");
            List<XElement> words = new List<XElement>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // Exemple de format attendu : "trad|simp|py|trans1;trans2"
                    string[] parts = line.Split('|');
                    if (parts.Length < 4)
                    {
                        Console.WriteLine($"Erreur : Ligne ignorée (format incorrect) : {line}");
                        continue;
                    }

                    XElement wordElement = new XElement("word",
                        new XElement("trad", parts[0]),
                        new XElement("simp", parts[1]),
                        new XElement("py", parts[2]),
                        new XElement("trans",
                            parts[3].Split(';', StringSplitOptions.RemoveEmptyEntries)
                            .Select(translation => new XElement("fr", translation.Trim())))
                    );

                    words.Add(wordElement);
                }
            }

            SaveWordsToXml(words);
        }

        private void SaveWordsToXml(List<XElement> words)
        {
            if (words.Count == 0)
            {
                Console.WriteLine("Aucune donnée à sauvegarder.");
                return;
            }

            Console.Write("Voulez-vous sauvegarder ces données dans le dictionnaire principal ? (y/n) : ");
            string? response = Console.ReadLine()?.Trim().ToLower();
            if (response != "y")
            {
                Console.WriteLine("Sauvegarde annulée.");
                return;
            }

            try
            {
                const string DefaultXmlFile = "cfdict.xml";
                XDocument doc;
                if (File.Exists(DefaultXmlFile))
                {
                    doc = XDocument.Load(DefaultXmlFile);
                }
                else
                {
                    Console.WriteLine($"Création d'un nouveau fichier XML : {DefaultXmlFile}");
                    doc = new XDocument(new XElement("dictionary"));
                }

                XElement root = doc.Element("dictionary")!;
                root.Add(words);

                doc.Save(DefaultXmlFile);
                Console.WriteLine($"Mots sauvegardés dans {DefaultXmlFile} avec succès !");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la sauvegarde des données : {ex.Message}");
            }
        }
    }
}