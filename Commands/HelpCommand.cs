#nullable enable
using System;

namespace ChineseDictionary
{
    public class HelpCommand
    {
        private string currentLanguage = "fr"; // Langue par défaut : français

        public void SetLanguage(string language)
        {
            if (language == "fr" || language == "en")
            {
                currentLanguage = language;
                Console.WriteLine(language == "fr"
                    ? "Langue changée en : français."
                    : "Language switched to: English.");
            }
            else
            {
                Console.WriteLine(language == "fr"
                    ? "Langue non supportée. Veuillez choisir 'fr' ou 'en'."
                    : "Unsupported language. Please choose 'fr' or 'en'.");
            }
        }

        public void Execute()
        {
            if (currentLanguage == "fr")
            {
                Console.WriteLine("\n--- Commandes disponibles ---");
                Console.WriteLine("1. help                         - Affiche cette liste d'aide.");
                Console.WriteLine("2. exit                         - Quitte le programme.");
                Console.WriteLine("3. getpinyin <caractere>        - Recherche le pinyin d'un mot chinois.");
                Console.WriteLine("4. getsimplified <caractere>    - Recherche la forme simplifiée d'un mot chinois.");
                Console.WriteLine("5. gettraditional <caractere>   - Recherche la forme traditionnelle d'un mot chinois.");
                Console.WriteLine("6. gettranslation <caractere>   - Recherche la traduction d'un mot chinois en français.");
                Console.WriteLine("7. search <caractere>           - Recherche un mot dans le dictionnaire (affiche toutes les informations).");
                Console.WriteLine("8. save                         - Sauvegarde les résultats de recherche dans un fichier XML, JSON, TXT.");
                Console.WriteLine("9. add                          - Ajoute un nouveau mot dans le dictionnaire.");
                Console.WriteLine("10. undo                        - Annule la dernière recherche effectuée.");
                Console.WriteLine("11. history [index]             - Affiche l'historique ou un mot spécifique par son index.");
                Console.WriteLine("12. changelanguage <langue>     - Change la langue ('fr' ou 'en').");

                Console.WriteLine("\n--- Instructions supplémentaires ---");
                Console.WriteLine("Lorsque vous entrez une commande nécessitant un mot, comme 'getpinyin', 'gettranslation', etc.,");
                Console.WriteLine("vous devrez entrer un mot chinois que vous souhaitez rechercher.");
                Console.WriteLine("Par exemple : 'getpinyin' suivi d'un mot chinois comme '你好'.");
                Console.WriteLine("\nPour afficher un mot spécifique de l'historique, utilisez : 'history <index>' où <index> est un numéro.");
                Console.WriteLine("Tapez 'exit' à tout moment pour quitter le programme.");
            }
            else if (currentLanguage == "en")
            {
                Console.WriteLine("\n--- Available Commands ---");
                Console.WriteLine("1. help                         - Displays this help list.");
                Console.WriteLine("2. exit                         - Exits the program.");
                Console.WriteLine("3. getpinyin <character>        - Finds the pinyin of a Chinese word.");
                Console.WriteLine("4. getsimplified <character>    - Finds the simplified form of a Chinese word.");
                Console.WriteLine("5. gettraditional <character>   - Finds the traditional form of a Chinese word.");
                Console.WriteLine("6. gettranslation <character>   - Finds the translation of a Chinese word into French.");
                Console.WriteLine("7. search <character>           - Searches a word in the dictionary (displays all information).");
                Console.WriteLine("8. save                         - Saves search results into an XML, JSON, or TXT file.");
                Console.WriteLine("9. add                          - Adds a new word to the dictionary.");
                Console.WriteLine("10. undo                        - Cancels the last performed search.");
                Console.WriteLine("11. history [index]             - Displays the search history or a specific word by index.");
                Console.WriteLine("12. changelanguage <language>   - Switches the language ('fr' or 'en').");

                Console.WriteLine("\n--- Additional Instructions ---");
                Console.WriteLine("When using a command that requires a word, like 'getpinyin' or 'gettranslation',");
                Console.WriteLine("you must provide a Chinese word you wish to search for.");
                Console.WriteLine("For example: 'getpinyin' followed by a Chinese word like '你好'.");
                Console.WriteLine("\nTo display a specific word from the history, use: 'history <index>' where <index> is a number.");
                Console.WriteLine("Type 'exit' at any time to leave the program.");
            }
            else
            {
                Console.WriteLine("Language not set. Please set it to 'fr' or 'en'.");
            }
        }
    }
}