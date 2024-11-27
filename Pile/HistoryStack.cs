using System;
using System.Collections.Generic;

namespace ChineseDictionary
{
    public class HistoryStack
    {
        private Stack<string> history = new Stack<string>();

        // Ajouter un mot à l'historique
        public void AddToHistory(string word)
        {
            history.Push(word);
        }

        // Récupérer le dernier mot recherché
        public string GetLastSearch()
        {
            return history.Count > 0 ? history.Peek() : null;
        }

        // Supprimer le dernier mot de l'historique
        public string UndoLastSearch()
        {
            return history.Count > 0 ? history.Pop() : null;
        }

        // Afficher l'historique des recherches
        public void ShowHistory()
        {
            Console.WriteLine("Historique des recherches :");
            foreach (var word in history)
            {
                Console.WriteLine(word);
            }
        }
    }
}