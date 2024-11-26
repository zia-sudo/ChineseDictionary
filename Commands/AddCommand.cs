using System;
using System.Linq;
using System.Xml.Linq;

namespace ChineseDictionary
{
    public class AddCommand
    {
        // Exécuter la commande pour ajouter un nouveau mot dans le fichier XML
        public void Execute()
        {
            // Demander la forme traditionnelle du mot
            Console.WriteLine("Ajout d'un nouveau mot au dictionnaire");

            Console.Write("Entrez la forme traditionnelle du mot : ");
            string trad = Console.ReadLine();

            // Charger le fichier XML existant
            XDocument doc = XDocument.Load("cfdict.xml");

            // Vérifier si le mot existe déjà dans le dictionnaire (par la forme traditionnelle)
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

            // Si le mot n'existe pas, demander les autres informations
            Console.Write("Entrez la forme simplifiée du mot : ");
            string simp = Console.ReadLine();

            Console.Write("Entrez le pinyin du mot : ");
            string pinyin = Console.ReadLine();

            Console.Write("Entrez les traductions (séparées par des virgules) : ");
            string translationsInput = Console.ReadLine();
            var translations = translationsInput.Split(',');

            // Générer un ID unique pour ce mot
            string id = Guid.NewGuid().ToString();

            // Créer un nouvel élément pour le mot à ajouter
            XElement newWord = new XElement("word",
                new XElement("id", id),
                new XElement("trad", trad),
                new XElement("simp", simp),
                new XElement("py", pinyin),
                new XElement("trans", new XElement("fr", translations))
            );

            // Ajouter le mot au document XML
            doc.Element("words").Add(newWord);

            // Sauvegarder le fichier XML mis à jour
            doc.Save("cfdict.xml");

            Console.WriteLine("Le mot a été ajouté avec succès !");
            Console.WriteLine("Informations du mot ajouté :");

            // Afficher les informations du mot ajouté
            Console.WriteLine($"ID : {id}");
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