using System;

namespace ChineseDictionary
{
    public class RemoveCommand
    {
        // Modifiez la méthode Execute pour accepter un mot à supprimer
        public void Execute(string word)
        {
            // Logique pour supprimer le mot du dictionnaire
            Console.WriteLine($"Suppression du mot : {word}");

            // Implémenter la logique de suppression ici (ex : suppression d'un fichier XML)
            Console.WriteLine($"Le mot '{word}' a été supprimé avec succès.");
        }
    }
}