using System;

namespace ChineseDictionary
{
    public class CommandInterpreter
    {
        private HistoryStack historyStack = new HistoryStack(); // Créer une pile pour l'historique

        public bool Interpret(string input)
        {
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string command = parts.Length > 0 ? parts[0] : string.Empty;

            switch (command)
            {
                case "help":
                    new HelpCommand().Execute();
                    break;

                case "exit":
                    return new ExitCommand().Execute();

                case "getpinyin":
                case "gettraditional":
                case "getsimplified":
                case "gettranslation":
                case "search":
                case "remove":
                    if (parts.Length > 1)
                    {
                        string word = parts[1];
                        ExecuteCommand(command, word);
                    }
                    else
                        Console.WriteLine($"Veuillez entrer un mot après la commande '{command}'.");
                    break;

                case "add":
                    new AddCommand().Execute();
                    break;

                case "save":
                    new SaveCommand().Execute();
                    break;

                case "undo":
                    string? lastSearch = historyStack.UndoLastSearch();
                    Console.WriteLine(lastSearch != null
                        ? $"Dernière recherche annulée : {lastSearch}"
                        : "Aucune recherche précédente à annuler.");
                    break;

                case "history":
                    historyStack.ShowHistory();
                    break;

                default:
                    Console.WriteLine("Commande inconnue. Tapez 'help' pour afficher les commandes disponibles.");
                    break;
            }

            return false;
        }

        private void ExecuteCommand(string command, string word)
        {
            switch (command)
            {
                case "getpinyin":
                    new GetPinyinCommand().Execute(word);
                    historyStack.AddToHistory(word);
                    break;
                case "gettraditional":
                    new GetTraditionalCommand().Execute(word);
                    historyStack.AddToHistory(word);
                    break;
                case "getsimplified":
                    new GetSimplifiedCommand().Execute(word);
                    historyStack.AddToHistory(word);
                    break;
                case "gettranslation":
                    new GetTranslationCommand().Execute(word);
                    historyStack.AddToHistory(word);
                    break;
                case "search":
                    new SearchCommand().Execute(word);
                    historyStack.AddToHistory(word);
                    break;
                case "remove":
                    new RemoveCommand().Execute(word);
                    historyStack.AddToHistory(word);
                    break;
            }
        }
    }
}