using System;
using System.IO;

namespace ChineseDictionary
{
    public class LoadCommand
    {
        public void Execute(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                Console.WriteLine("Fichier introuvable ou chemin invalide.");
                return;
            }

            try
            {
                var lines = File.ReadAllLines(filePath);
                Console.WriteLine("Contenu du fichier chargé :");
                foreach (var line in lines)
                {
                    Console.WriteLine(line);
                }

                Console.WriteLine($"Le fichier '{filePath}' a été chargé avec succès.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors du chargement du fichier : {ex.Message}");
            }
        }
    }
}