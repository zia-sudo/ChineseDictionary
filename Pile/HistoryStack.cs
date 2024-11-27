using System;
using System.Collections.Generic;

namespace ChineseDictionary
{
    public class HistoryStack
    {
        private List<string> history = new List<string>();  // Utilisation d'une liste pour préserver l'ordre

        // Ajouter un mot à l'historique
        public void AddToHistory(string word)
        {
            history.Add(word);  // Ajout à la fin de la liste
        }

        // Récupérer le dernier mot recherché
        public string GetLastSearch()
        {
            return history.Count > 0 ? history[history.Count - 1] : null;  // Dernier élément de la liste
        }

        // Supprimer le dernier mot de l'historique
        public string UndoLastSearch()
        {
            if (history.Count > 0)
            {
                string lastSearch = history[history.Count - 1];
                history.RemoveAt(history.Count - 1);  // Suppression du dernier élément
                return lastSearch;
            }
            return null;
        }

        // Supprimer un mot spécifique de l'historique
        public bool RemoveFromHistory(string word)
        {
            if (history.Contains(word))
            {
                history.Remove(word);
                Console.WriteLine($"Le mot '{word}' a été supprimé de l'historique.");
                return true;
            }
            Console.WriteLine($"Le mot '{word}' n'est pas présent dans l'historique.");
            return false;
        }

        // Afficher l'historique des recherches dans l'ordre chronologique
        public void ShowHistory()
        {
            if (history.Count == 0)
            {
                Console.WriteLine("Aucun mot dans l'historique.");
                return;
            }

            Console.WriteLine("Historique des recherches :");
            int i = 1;
            foreach (var word in history)
            {
                Console.WriteLine($"{i}. {word}");
                i++;
            }
        }
    }
}