#nullable enable
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

            if (TryParseLanguage(lang, out Language selectedLanguage))
            {
                string languageCode = selectedLanguage == Language.French ? "fr" : "en";
                helpCommand.SetLanguage(languageCode);

                Console.WriteLine(selectedLanguage == Language.French
                    ? "Les messages d'aide seront désormais affichés en français."
                    : "Help messages will now be displayed in English.");
            }
            else
            {
                Console.WriteLine("Langue non supportée. Veuillez choisir 'fr' pour français ou 'en' pour anglais.");
            }
        }

        private bool TryParseLanguage(string input, out Language language)
        {
            switch (input.ToLower())
            {
                case "fr":
                    language = Language.French;
                    return true;
                case "en":
                    language = Language.English;
                    return true;
                default:
                    language = default;
                    return false;
            }
        }

        // Énumération pour représenter les langues
        private enum Language
        {
            French,
            English
        }
    }
}