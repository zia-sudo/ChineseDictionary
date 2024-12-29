#nullable enable
using System;
using System.Collections.Generic;

namespace ChineseDictionary
{
    public class HistoryStack
    {
        private List<string> history = new List<string>();

        public void AddToHistory(string word)
        {
            if (!string.IsNullOrEmpty(word))
            {
                history.Add(word);
            }
        }

        public string? UndoLastSearch()
        {
            if (history.Count == 0) return null;

            string lastSearch = history[^1];
            history.RemoveAt(history.Count - 1);
            return lastSearch;
        }

        public void ShowHistory()
        {
            if (history.Count == 0)
            {
                Console.WriteLine("Aucune recherche enregistrée.");
                return;
            }

            Console.WriteLine("Historique des recherches :");
            for (int i = 0; i < history.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {history[i]}");
            }
        }
    }
}