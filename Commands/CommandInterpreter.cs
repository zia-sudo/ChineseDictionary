using System;

namespace ChineseDictionary
{
    public class CommandInterpreter
    {
        private HistoryStack historyStack = new HistoryStack(); // Créer une pile pour l'historique

        // Modifier la méthode pour intégrer l'historique
        public bool Interpret(string input)
        {
            string[] parts = input.Split(' ');
            string command = parts[0];

            // Vérifier la commande et appeler la méthode appropriée
            switch (command)
            {
                case "help":
                    new HelpCommand().Execute();
                    break;

                case "exit":
                    return new ExitCommand().Execute(); // Retourne true pour quitter

                case "getpinyin":
                    if (parts.Length > 1)
                    {
                        string word = parts[1];
                        new GetPinyinCommand().Execute(word);
                        historyStack.AddToHistory(word); // Ajouter à l'historique
                    }
                    else
                        Console.WriteLine("Veuillez entrer un mot après la commande 'getpinyin'.");
                    break;

                case "gettraditional":
                    if (parts.Length > 1)
                    {
                        string word = parts[1];
                        new GetTraditionalCommand().Execute(word);
                        historyStack.AddToHistory(word); // Ajouter à l'historique
                    }
                    else
                        Console.WriteLine("Veuillez entrer un mot après la commande 'gettraditional'.");
                    break;

                case "getsimplified":
                    if (parts.Length > 1)
                    {
                        string word = parts[1];
                        new GetSimplifiedCommand().Execute(word);
                        historyStack.AddToHistory(word); // Ajouter à l'historique
                    }
                    else
                        Console.WriteLine("Veuillez entrer un mot après la commande 'getsimplified'.");
                    break;
                
                case "gettranslation":
                    if (parts.Length > 1)
                    {
                        string word = parts[1];
                        new GetTranslationCommand().Execute(word);
                        historyStack.AddToHistory(word); // Ajouter à l'historique
                    }
                    else
                        Console.WriteLine("Veuillez entrer un mot après la commande 'gettranslation'.");
                    break;

                case "search":
                    if (parts.Length > 1)
                    {
                        string word = parts[1];
                        new SearchCommand().Execute(word);
                        historyStack.AddToHistory(word); // Ajouter à l'historique
                    }
                    else
                        Console.WriteLine("Veuillez entrer un mot après la commande 'search'.");
                    break;

                case "add":
                    new AddCommand().Execute();  // Appeler la commande Add
                    break;

                case "remove":
                    if (parts.Length > 1)
                    {
                        string word = parts[1];
                        new RemoveCommand().Execute(word); // Appeler la commande Remove
                        historyStack.AddToHistory(word); // Ajouter à l'historique
                    }
                    else
                        Console.WriteLine("Veuillez entrer un mot après la commande 'remove'.");
                    break;
                
                case "undo":
                    string lastSearch = historyStack.UndoLastSearch();
                    if (lastSearch != null)
                        Console.WriteLine($"Dernière recherche annulée : {lastSearch}");
                    else
                        Console.WriteLine("Aucune recherche précédente à annuler.");
                    break;

                case "history":
                    historyStack.ShowHistory(); // Afficher l'historique
                    break;

                default:
                    Console.WriteLine("Commande inconnue. Tapez 'help' pour afficher les commandes disponibles.");
                    break;
            }
            return false;  // Ne quitte pas le programme
        }
    }
}