using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace ChineseDictionary
{
    public class AddCommand
    {
        public void Execute()
        {
            Console.WriteLine("Ajout d'un nouveau mot au dictionnaire");

            Console.Write("Entrez la forme traditionnelle du mot : ");
            string trad = Console.ReadLine();

            if (trad?.ToLower() == "exit") return;

            // Vérifier si la saisie est en caractères chinois
            if (!IsChinese(trad))
            {
                Console.WriteLine("La forme traditionnelle doit être écrite en caractères chinois.");
                return;
            }

            // Charger le fichier XML
            XDocument doc = XDocument.Load("./Data/cfdict.xml");

            if (doc.Root?.Name != "dic")
            {
                Console.WriteLine("Le fichier XML n'a pas une racine valide (<dic> attendue).");
                return;
            }

            // Vérifier si le mot existe déjà
            var existingWord = doc.Descendants("word").FirstOrDefault(w =>
                w.Element("trad")?.Value == trad);

            if (existingWord != null)
            {
                Console.WriteLine("Ce mot existe déjà dans le dictionnaire.");
                Console.WriteLine($"Forme Traditionnelle : {existingWord.Element("trad")?.Value}");
                Console.WriteLine($"Forme Simplifiée : {existingWord.Element("simp")?.Value}");
                Console.WriteLine($"Pinyin : {existingWord.Element("py")?.Value}");
                Console.WriteLine("Traductions : ");
                foreach (var translation in existingWord.Element("trans").Elements("fr"))
                {
                    Console.WriteLine($"  - {translation.Value}");
                }
                return;
            }

            // Récupérer les autres informations
            Console.Write("Entrez la forme simplifiée du mot : ");
            string simp = Console.ReadLine();

            if (simp?.ToLower() == "exit") return;

            Console.Write("Entrez le pinyin du mot : ");
            string pinyin = Console.ReadLine();

            if (pinyin?.ToLower() == "exit") return;

            Console.Write("Entrez les traductions (séparées par des virgules) : ");
            string translationsInput = Console.ReadLine();

            if (translationsInput?.ToLower() == "exit") return;

            var translations = translationsInput.Split(',');

            // Générer un ID unique pour le mot
            int newId;
            var words = doc.Descendants("word");

            var maxId = words
                .Where(w => int.TryParse(w.Element("id")?.Value, out _))
                .Max(w => int.Parse(w.Element("id")?.Value));

            newId = maxId + 1;

            XElement newWord = new XElement("word",
                new XElement("id", newId),
                new XElement("upd", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new XElement("trad", trad),
                new XElement("simp", simp),
                new XElement("py", pinyin),
                new XElement("trans", translations.Select(t => new XElement("fr", t.Trim())))
            );

            doc.Root.Add(newWord);
            doc.Save("./Data/cfdict.xml");

            Console.WriteLine("Le mot a été ajouté avec succès !");
            Console.WriteLine($"ID : {newId}");
            Console.WriteLine($"Forme Traditionnelle : {trad}");
            Console.WriteLine($"Forme Simplifiée : {simp}");
            Console.WriteLine($"Pinyin : {pinyin}");
            Console.WriteLine("Traductions : ");
            foreach (var translation in translations)
            {
                Console.WriteLine($"  - {translation.Trim()}");
            }
        }

        private bool IsChinese(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;

            // Regex pour détecter des caractères chinois
            Regex chineseRegex = new Regex(@"\p{IsCJKUnifiedIdeographs}");
            return chineseRegex.IsMatch(input);
        }
    }
}