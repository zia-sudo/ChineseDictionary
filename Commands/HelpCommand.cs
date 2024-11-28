using System;

namespace ChineseDictionary
{
    public class HelpCommand
    {
        public void Execute()
        {
            Console.WriteLine("\n--- Commandes disponibles ---");
            Console.WriteLine("1. help                         - Affiche cette liste d'aide.");
            Console.WriteLine("2. exit                         - Quitte le programme.");
            Console.WriteLine("3. getpinyin <caractere>        - Recherche le pinyin d'un mot chinois.");
            Console.WriteLine("4. getsimplified <caractere>    - Recherche la forme simplifiée d'un mot chinois.");
            Console.WriteLine("5. gettraditional <caractere>   - Recherche la forme traditionnelle d'un mot chinois.");
            Console.WriteLine("6. gettranslation <caractere>   - Recherche la traduction d'un mot chinois en français.");
            Console.WriteLine("7. search <caractere>           - Recherche un mot dans le dictionnaire (affiche toutes les informations).");
            Console.WriteLine("8. save <caractere>             - Sauvegarde les résultats de recherche dans un fichier XML.");
            Console.WriteLine("9. add                          - Ajoute un nouveau mot dans le dictionnaire.");
            Console.WriteLine("10. undo                        - Annule la dernière recherche effectuée.");
            Console.WriteLine("11. history                     - Affiche l'historique des recherches effectuées.");
            Console.WriteLine("\n--- Instructions supplémentaires ---");
            Console.WriteLine("Lorsque vous entrez une commande nécessitant un mot, comme 'getpinyin', 'gettranslation', etc.,");
            Console.WriteLine("vous devrez entrer un mot chinois que vous souhaitez rechercher.");
            Console.WriteLine("Par exemple : 'getpinyin' suivi d'un mot chinois comme '𡳞'.");
            Console.WriteLine("\nTapez 'exit' à tout moment pour quitter le programme.");
        }
    }
}