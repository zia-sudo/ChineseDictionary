using System;

namespace ChineseDictionary
{
    public class CommandInterpreter
    {
        // Modifie la méthode pour retourner un booléen indiquant si le programme doit se fermer
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
                        new GetPinyinCommand().Execute(parts[1]);
                    else
                        Console.WriteLine("Veuillez entrer un mot après la commande 'getpinyin'.");
                    break;
                case "gettraditional":
                    if (parts.Length > 1)
                        new GetTraditionalCommand().Execute(parts[1]);
                    else
                        Console.WriteLine("Veuillez entrer un mot après la commande 'gettraditional'.");
                    break;
                case "getsimplified":
                    if (parts.Length > 1)
                        new GetSimplifiedCommand().Execute(parts[1]);
                    else
                        Console.WriteLine("Veuillez entrer un mot après la commande 'getsimplified'.");
                    break;
                case "gettranslation":
                    if (parts.Length > 1)
                        new GetTranslationCommand().Execute(parts[1]);
                    else
                        Console.WriteLine("Veuillez entrer un mot après la commande 'gettranslation'.");
                    break;
                case "search":
                    if (parts.Length > 1)
                        new SearchCommand().Execute(parts[1]);
                    else
                        Console.WriteLine("Veuillez entrer un mot après la commande 'search'.");
                    break;
                case "save":
                    new SaveCommand().Execute();
                    break;
                case "add":
                    new AddCommand().Execute();
                    break;
                default:
                    Console.WriteLine("Commande inconnue. Tapez 'help' pour afficher les commandes disponibles.");
                    break;
            }
            return false;  // Ne quitte pas le programme
        }
    }
}