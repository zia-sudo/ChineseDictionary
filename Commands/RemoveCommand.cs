using System;
using System.Linq;
using System.Xml.Linq;

namespace ChineseDictionary
{
    public class RemoveCommand
    {
        // Exécuter la commande pour supprimer un mot du dictionnaire en fonction de sa forme traditionnelle ou simplifiée
        public void Execute(string word)
        {
            // Charger le fichier XML
            XDocument doc = XDocument.Load("./Data/cfdict.xml");

            // Chercher le mot en fonction de la forme traditionnelle ou simplifiée
            var wordElement = doc.Descendants("word")
                .FirstOrDefault(w =>
                    w.Element("trad")?.Value.Equals(word, StringComparison.OrdinalIgnoreCase) == true ||
                    w.Element("simp")?.Value.Equals(word, StringComparison.OrdinalIgnoreCase) == true);

            // Si le mot est trouvé, on le supprime
            if (wordElement != null)
            {
                Console.WriteLine($"Le mot '{word}' a été trouvé et sera supprimé.");
                
                // Supprimer l'élément du dictionnaire
                wordElement.Remove();
                
                // Sauvegarder le fichier XML après suppression
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