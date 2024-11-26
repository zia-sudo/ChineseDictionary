using System;

namespace ChineseDictionary
{
    public class ExitCommand
    {
        // Exécuter la commande pour quitter le programme
        public bool Execute()
        {
            Console.WriteLine("Vous venez de cliquer sur 'exit' ! Merci d'avoir utilisé ce dictionnaire !");
            return true;
        }
    }
}