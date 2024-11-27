using System;
using ChineseDictionary;

namespace ChineseDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandInterpreter interpreter = new CommandInterpreter();

            Console.WriteLine("Bienvenue dans le Dictionnaire Chinois !");
            Console.WriteLine("Tapez 'help' pour afficher les commandes disponibles.");

            bool exitProgram = false;

            while (!exitProgram)
            {
                Console.Write("\n> ");
                string? input = Console.ReadLine()?.Trim().ToLower(); // Autorise les valeurs nulles

                if (!string.IsNullOrEmpty(input))
                {
                    exitProgram = interpreter.Interpret(input); // Si true, quitter la boucle
                }
                else
                {
                    Console.WriteLine("Entrée invalide. Veuillez essayer à nouveau.");
                }
            }

            Console.WriteLine("Au revoir !");
        }
    }
}