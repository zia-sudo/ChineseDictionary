#nullable enable
using System;
using System.Collections.Generic;

namespace ChineseDictionary
{
    public class HistoryStack
    {
        private List<string> history = new List<string>();
        public bool HasHistory => history.Count > 0;
        public int HistoryCount => history.Count;

        public string LastSearch
        {
            get => history.Count > 0 ? history[^1] : "Aucune recherche précédente.";
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    history.Add(value);
                }
            }
        }

        public void AddToHistory(string word)
        {
            if (!string.IsNullOrEmpty(word))
            {
                history.Add(word);
                Console.WriteLine($"Ajouté à l'historique : {word}");
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
            if (!HasHistory)
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

        public void ShowSpecificHistory(string indexStr)
        {
            if (int.TryParse(indexStr, out int index) && index > 0 && index <= HistoryCount)
            {
                Console.WriteLine($"Mot #{index} dans l'historique : {history[index - 1]}");
            }
            else
            {
                Console.WriteLine($"Index invalide : {indexStr}. Entrez un nombre entre 1 et {HistoryCount}.");
            }
        }
    }
}