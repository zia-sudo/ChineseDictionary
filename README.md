# ChineseDictionary

**ChineseDictionary** est une application console en C# permettant d'interagir avec un dictionnaire chinois-français. Elle offre des fonctionnalités telles que la recherche de mots, l'ajout d'entrées, la récupération de traductions et de pinyin, ainsi que la gestion des données du dictionnaire.

Ce projet a été réalisé dans le cadre du cours POO C# au premier semestre en M2.

## Structure du projet

### Dossier `Commands`
Ce dossier contient les fichiers responsables de l'exécution des différentes commandes dans l'application.

- **`AddCommand.cs`** : Permet d'ajouter une nouvelle entrée dans le dictionnaire.
- **`ExitCommand.cs`** : Permet de quitter l'application.
- **`HelpCommand.cs`** : Affiche les commandes disponibles et leur usage.
- **`GetPinyinCommand.cs`** : Affiche le pinyin d'un caractère chinois.
- **`GetSimplifiedCommand.cs`** : Affiche la forme simplifiée d'un caractère chinois.
- **`GetTraditionalCommand.cs`** : Affiche la forme traditionnelle d'un caractère chinois.
- **`SaveCommand.cs`** : Sauvegarde les données du dictionnaire dans json, txt ou xml.
- **`GetTranslationCommand.cs`** : Affiche la traduction d'un caractère chinois.
- **`SearchCommand.cs`** : Recherche un caractère ou mot dans le dictionnaire.
- **`CommandInterpreter.cs`** : Interprète les commandes et exécute la commande correspondante.

### Dossier `Pile`

Le dossier Pile contient la logique de gestion de l'historique des recherches. La classe HistoryStack gère une pile de mots recherchés, permettant d'ajouter, de récupérer et de supprimer des mots dans l'historique. Voici un aperçu des fonctionnalités principales de cette classe :

Ajouter un mot à l'historique : La méthode AddToHistory(string word) permet d'ajouter un mot à la pile.
Récupérer le dernier mot recherché : La méthode GetLastSearch() retourne le mot le plus récemment ajouté.
Annuler la dernière recherche : La méthode UndoLastSearch() supprime et retourne le dernier mot de la pile.
Afficher l'historique des recherches : La méthode ShowHistory() affiche tous les mots enregistrés dans l'historique.

### Dossier `Data`

- **`cfdict.xml`** : Le fichier XML contenant toutes les données du dictionnaire, telles que les mots, les formes traditionnelles et simplifiées, les pinyin et les traductions.

### Dossier `Main`

- **`Program.cs`** : Le point d'entrée principal de l'application, où l'exécution du programme commence.

## Fonctionnalités

1. **Ajout de mots** : 
   - Permet d'ajouter de nouveaux mots dans le dictionnaire, y compris leur forme traditionnelle, simplifiée, pinyin et traductions.
   
2. **Recherche de mots** : 
   - Permet de rechercher un mot ou caractère dans le dictionnaire en utilisant sa forme traditionnelle, et d'afficher ses informations détaillées telles que le pinyin, les traductions et les formes simplifiées.
   
3. **Affichage du pinyin, de la forme simplifiée ou traditionnelle** :
   - Affiche le pinyin, la forme simplifiée ou traditionnelle pour un caractère chinois donné.
   
4. **Sauvegarde des données** :
   - Sauvegarde automatiquement les données dans le fichier `cfdict.xml` pour une persistance des informations ajoutées.
   
5. **Aide et Commandes** :
   - Offre une fonctionnalité d'aide pour afficher les commandes disponibles et leur usage.

## Commandes disponibles

Voici un aperçu des commandes disponibles dans l'application :

- **`add`** : Ajouter un nouveau mot dans le dictionnaire. Vous entrez la commande sans rien mettre puis le dictionnaire vous guidera à mettre la forme tradictionnelle du caractère, puis la forme simplifiée, ensuite la traduction, s'il existe plusieurs traductions pour ce caractère, vous pouvez les séparer par une virgule. Si le caractère a été bien enregistré, vous verrez un aperçu du caractère que vous avez enregistré. Le dictionnaire lui attribuera également un numéro automatiquement.
- **`exit`** : Quitter l'application.
- **`help`** : Afficher les commandes disponibles et leur usage.
- **`getpinyin`** : Afficher le pinyin d'un caractère chinois. Pour l'utiliser, vous devez mettre un caractère derrière la commande en mettant un espace entre la commande et le caractère.
- **`getsimplified`** : Afficher la forme simplifiée d'un caractère chinois. Pour l'utiliser, vous devez mettre un caractère derrière la commande en mettant un espace entre la commande et le caractère.
- **`gettraditional`** : Afficher la forme traditionnelle d'un caractère chinois. Pour l'utiliser, vous devez mettre un caractère derrière la commande en mettant un espace entre la commande et le caractère.
- **`save`** : Sauvegarder les données du dictionnaire. Une fois que vous avez tapé la commande **`save`** , vous cliquez sur **`Entry`**  le dictionnaire vous demandera ensuite de choisir le caractère que vous voulez enregistrer puis il demandera le format du fichier que vous souhaitez choisir.
- **`gettranslation`** : Afficher la traduction d'un caractère chinois. Pour l'utiliser, vous devez mettre un caractère derrière la commande en mettant un espace entre la commande et le caractère.
- **`search`** : Rechercher un mot ou un caractère dans le dictionnaire afin d'obtenir toutes les informations du mot. Pour l'utiliser, vous devez mettre un caractère derrière la commande en mettant un espace entre la commande et le caractère.
- **`undo`** : Annuler la dernière recherche effectuée. Cette commande permet de retirer le dernier mot ajouté à l'historique des recherches.
- **`history`** : Afficher l'historique des mots recherchés précédemment. Cette commande permet de consulter la liste des mots recherchés, avec leur index.

### Exemple de commande

- Pour ajouter un mot : add
Ensuite, l'application demandera à l'utilisateur d'entrer les informations nécessaires pour le mot à ajouter.

- Pour rechercher toutes les informations d'un mot : search
Ensuite, l'application demandera à l'utlisateur d'entrer le thème qu'il veut rechercher, l'utilisateur peut saisir soit la forme traditionnelle, soit le pinyin, soit la forme simplifiée, soit la traduction.

### Prérequis
.NET 5.0 ou supérieur : Le projet utilise le framework .NET.

Visual Studio ou autre IDE C# pour le développement et l'exécution de l'application.

### Instructions d'utilisation

1. **Cloner le repository** :

 git clone https://github.com/votre-utilisateur/ChineseDictionary.git

2. **Compilation et exécution** :
Ouvrir le projet dans Visual Studio ou tout autre IDE C#.

Compiler et exécuter l'application en mode console. A l'aide de la commande : dotnet run

2. **Interaction avec l'application** :

Une fois l'application lancée, vous pouvez entrer une commande, comme add, search, ou help, pour interagir avec le dictionnaire.

Suivez les instructions à l'écran pour ajouter des mots, rechercher des mots, ou obtenir des informations spécifiques sur un caractère.

### Auteur

Léa MANET







