using System;

namespace ChineseDictionary
{
    public class ChangeLanguageCommand
    {
        private readonly HelpCommand helpCommand;

        public ChangeLanguageCommand(HelpCommand helpCommand)
        {
            this.helpCommand = helpCommand;
        }

        public void Execute(string? lang)
        {
            if (string.IsNullOrWhiteSpace(lang))
            {
                Console.WriteLine("Veuillez fournir une langue valide : 'fr' pour français ou 'en' pour anglais.");
                return;
            }

            if (lang == "fr" || lang == "en")
            {
                helpCommand.SetLanguage(lang);
                Console.WriteLine(lang == "fr"
                    ? "Les messages d'aide seront désormais affichés en français."
                    : "Help messages will now be displayed in English.");
            }
            else
            {
                Console.WriteLine("Langue non supportée. Veuillez choisir 'fr' pour français ou 'en' pour anglais.");
            }
        }
    }
}