using System;
using System.Linq;
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

            // Charger le fichier XML
            XDocument doc = XDocument.Load("./Data/cfdict.xml");

            // Vérifier si l'élément racine est bien <dic>
            if (doc.Root?.Name != "dic")
            {
                Console.WriteLine("Le fichier XML n'a pas une racine valide (<dic> attendue).");
                return;
            }

            // Vérifier si le mot existe déjà dans le dictionnaire
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
                return; // Si le mot existe déjà, on arrête l'ajout.
            }

            // Récupérer les autres informations
            Console.Write("Entrez la forme simplifiée du mot : ");
            string simp = Console.ReadLine();

            Console.Write("Entrez le pinyin du mot : ");
            string pinyin = Console.ReadLine();

            Console.Write("Entrez les traductions (séparées par des virgules) : ");
            string translationsInput = Console.ReadLine();
            var translations = translationsInput.Split(',');

            // Générer un ID unique pour le mot (en s'assurant qu'il est numérique)
            int newId;

            // Charger les éléments <word> dans le dictionnaire
            var words = doc.Descendants("word");

            // Vérifier si un ID numérique existe
            var maxId = words
                .Where(w => int.TryParse(w.Element("id")?.Value, out _))
                .Max(w => int.Parse(w.Element("id")?.Value));

            // Générer un nouvel ID en l'incrémentant de 1
            newId = maxId + 1;

            // Créer un nouvel élément pour le mot
            XElement newWord = new XElement("word",
                new XElement("id", newId), // Utilisation de l'ID numérique généré
                new XElement("upd", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()), // Timestamp actuel
                new XElement("trad", trad),
                new XElement("simp", simp),
                new XElement("py", pinyin),
                new XElement("trans", translations.Select(t => new XElement("fr", t.Trim())))
            );

            // Ajouter le nouvel élément sous la racine <dic>
            doc.Root.Add(newWord);

            // Sauvegarder le fichier
            doc.Save("./Data/cfdict.xml");

            Console.WriteLine("Le mot a été ajouté avec succès !");
            Console.WriteLine("Informations du mot ajouté :");
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
    }
}