using System;

namespace ChineseDictionary
{
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(string command)
            : base($"La commande '{command}' est inconnue. Tapez 'help' pour afficher les commandes disponibles.") { }
    }
}