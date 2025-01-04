using System;

namespace ChineseDictionary
{
    public class ExitCommand
    {
        // Exécuter la commande pour quitter le programme
        public bool Execute()
        {
            Console.WriteLine("Au revoir ! Merci d'avoir utilisé ChineseDictionary !");
            return true; // Indique à CommandInterpreter de terminer le programme
        }
    }
}