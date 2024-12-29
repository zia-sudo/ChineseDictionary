using System;

namespace ChineseDictionary
{
    public class CommandInterpreter
    {
        private readonly HistoryStack historyStack = new HistoryStack(); // Gestion de l'historique
        private readonly HelpCommand helpCommand = new HelpCommand();   // Commande d'aide

        public bool Interpret(string input)
        {
            // Séparer la commande et ses arguments
            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                Console.WriteLine("Entrée invalide. Tapez 'help' pour afficher les commandes disponibles.");
                return false;
            }

            string command = parts[0]; // Nom de la commande
            string argument = parts.Length > 1 ? parts[1] : string.Empty; // Argument fourni

            try
            {
                switch (command.ToLower())
                {
                    case "help":
                        helpCommand.Execute();
                        break;

                    case "exit":
                        return true;

                    case "changelanguage":
                        if (!string.IsNullOrWhiteSpace(argument))
                        {
                            new ChangeLanguageCommand(helpCommand).Execute(argument);
                        }
                        else
                        {
                            Console.WriteLine("Veuillez fournir une langue valide ('fr' ou 'en').");
                        }
                        break;

                    case "getpinyin":
                    case "getsimplified":
                    case "gettraditional":
                    case "gettranslation":
                    case "search":
                    case "remove":
                        if (!string.IsNullOrWhiteSpace(argument))
                        {
                            ExecuteCommandWithHistory(command, argument);
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
                        Console.WriteLine(historyStack.UndoLastSearch() ?? "Aucune recherche précédente à annuler.");
                        break;

                    case "history":
                        historyStack.ShowHistory();
                        break;

                    default:
                        throw new InvalidCommandException(command); // Lever une exception pour commande inconnue
                }
            }
            catch (InvalidCommandException ex)
            {
                // Gérer l'exception pour commande inconnue
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                // Gérer toutes les autres exceptions inattendues
                Console.WriteLine($"Erreur inattendue : {ex.Message}");
            }

            return false;
        }

        private void ExecuteCommandWithHistory(string command, string argument)
        {
            switch (command.ToLower())
            {
                case "getpinyin":
                    new GetPinyinCommand().Execute(argument);
                    break;

                case "getsimplified":
                    new GetSimplifiedCommand().Execute(argument);
                    break;

                case "gettraditional":
                    new GetTraditionalCommand().Execute(argument);
                    break;

                case "gettranslation":
                    new GetTranslationCommand().Execute(argument);
                    break;

                case "search":
                    new SearchCommand().Execute(argument);
                    break;

                case "remove":
                    new RemoveCommand().Execute(argument);
                    break;

                default:
                    Console.WriteLine("Commande non reconnue.");
                    return;
            }

            historyStack.AddToHistory(argument); // Ajouter à l'historique
        }
    }
}