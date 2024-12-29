using System;
using System.Linq;
using System.Xml.Linq;

namespace ChineseDictionary
{
    public class RemoveCommand
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

            if (doc.Root == null || doc.Root.Name != "dic")
            {
                Console.WriteLine("Le fichier XML n'a pas une racine valide (<dic> attendue).");
                return;
            }

            var wordElement = doc.Descendants("word")
                .FirstOrDefault(w =>
                    w.Element("trad")?.Value.Equals(word, StringComparison.OrdinalIgnoreCase) == true ||
                    w.Element("simp")?.Value.Equals(word, StringComparison.OrdinalIgnoreCase) == true);

            if (wordElement != null)
            {
                Console.WriteLine($"Le mot '{word}' a été trouvé et sera supprimé.");

                // Créer une sauvegarde avant suppression
                File.Copy("./Data/cfdict.xml", "./Data/cfdict_backup.xml", overwrite: true);

                wordElement.Remove();
                doc.Save("./Data/cfdict.xml");

                Console.WriteLine("Le mot a été supprimé avec succès.");
            }
            else
            {
                Console.WriteLine($"Aucun mot trouvé avec la forme '{word}'.");
            }
        }
    }
}