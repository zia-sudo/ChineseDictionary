#nullable enable
using System;

namespace ChineseDictionary
{
    public class CommandInterpreter
    {
        private readonly HistoryStack historyStack = new HistoryStack();
        private readonly HelpCommand helpCommand = new HelpCommand(); // Instance de HelpCommand

        public bool Interpret(string input)
        {
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                Console.WriteLine("Entrée invalide. Tapez 'help' pour afficher les commandes disponibles.");
                return false;
            }

            string command = parts[0].ToLower();
            string argument = parts.Length > 1 ? parts[1] : string.Empty;

            try
            {
                switch (command)
                {
                    case "help":
                        helpCommand.Execute();
                        break;

                    case "exit":
                        return true;

                    case "getpinyin":
                        if (!string.IsNullOrWhiteSpace(argument))
                        {
                            ExecuteCommandWithHistory(new GetPinyinCommand(), argument);
                        }
                        else
                        {
                            Console.WriteLine("Veuillez fournir un mot valide.");
                        }
                        break;

                    case "getsimplified":
                        if (!string.IsNullOrWhiteSpace(argument))
                        {
                            ExecuteCommandWithHistory(new GetSimplifiedCommand(), argument);
                        }
                        else
                        {
                            Console.WriteLine("Veuillez fournir un mot valide.");
                        }
                        break;

                    case "gettraditional":
                        if (!string.IsNullOrWhiteSpace(argument))
                        {
                            ExecuteCommandWithHistory(new GetTraditionalCommand(), argument);
                        }
                        else
                        {
                            Console.WriteLine("Veuillez fournir un mot valide.");
                        }
                        break;

                    case "gettranslation":
                        if (!string.IsNullOrWhiteSpace(argument))
                        {
                            ExecuteCommandWithHistory(new GetTranslationCommand(), argument);
                        }
                        else
                        {
                            Console.WriteLine("Veuillez fournir un mot valide.");
                        }
                        break;

                    case "search":
                        if (!string.IsNullOrWhiteSpace(argument))
                        {
                            ExecuteCommandWithHistory(new SearchCommand(), argument);
                        }
                        else
                        {
                            Console.WriteLine("Veuillez fournir un mot valide.");
                        }
                        break;

                    case "remove":
                        if (!string.IsNullOrWhiteSpace(argument))
                        {
                            ExecuteCommandWithHistory(new RemoveCommand(), argument);
                        }
                        else
                        {
                            Console.WriteLine("Veuillez fournir un mot valide.");
                        }
                        break;

                    case "add":
                        new AddCommand().Execute();
                        break;

                    case "save":
                        new SaveCommand().Execute();
                        break;

                    case "undo":
                        string? undoneSearch = historyStack.UndoLastSearch();
                        if (undoneSearch != null)
                        {
                            Console.WriteLine($"Recherche annulée avec succès : {undoneSearch}");
                        }
                        else
                        {
                            Console.WriteLine("Aucune recherche précédente à annuler.");
                        }
                        break;

                    case "history":
                        if (!string.IsNullOrWhiteSpace(argument))
                        {
                            historyStack.ShowSpecificHistory(argument);
                        }
                        else
                        {
                            historyStack.ShowHistory();
                        }
                        break;

                    case "changelanguage":
                        if (!string.IsNullOrWhiteSpace(argument))
                        {
                            new ChangeLanguageCommand(helpCommand).Execute(argument); // Passe l'instance de HelpCommand
                        }
                        else
                        {
                            Console.WriteLine("Veuillez fournir une langue valide ('fr' ou 'en').");
                        }
                        break;

                    default:
                        Console.WriteLine($"Commande inconnue : {command}. Tapez 'help' pour voir la liste des commandes.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur inattendue : {ex.Message}");
            }

            return false;
        }

        private void ExecuteCommandWithHistory(object command, string argument)
        {
            switch (command)
            {
                case GetPinyinCommand pinyinCommand:
                    pinyinCommand.Execute(argument);
                    break;

                case GetSimplifiedCommand simplifiedCommand:
                    simplifiedCommand.Execute(argument);
                    break;

                case GetTraditionalCommand traditionalCommand:
                    traditionalCommand.Execute(argument);
                    break;

                case GetTranslationCommand translationCommand:
                    translationCommand.Execute(argument);
                    break;

                case SearchCommand searchCommand:
                    searchCommand.Execute(argument);
                    break;

                case RemoveCommand removeCommand:
                    removeCommand.Execute(argument);
                    break;

                default:
                    Console.WriteLine("Commande non reconnue.");
                    return;
            }

            historyStack.AddToHistory(argument);
        }
    }
}