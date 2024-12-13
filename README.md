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

## Commandes disponibles et leurs usages

### 1. **Ajouter un mot dans le dictionnaire** : `add`
Ajoutez un nouveau mot dans le dictionnaire en suivant ces étapes :
1. Entrez la commande `add`.
2. Suivez les instructions :
   - Saisissez la **forme traditionnelle** du caractère.
   - Saisissez la **forme simplifiée** du caractère.
   - Saisissez la **traduction** du caractère (si plusieurs traductions existent, séparez-les par une virgule `,`).
3. Une fois le caractère enregistré, un aperçu sera affiché, et un numéro sera automatiquement attribué par le dictionnaire.

### 2. **Quitter l'application** : `exit`
- Entrez simplement `exit` pour fermer l'application.

### 3. **Afficher les commandes disponibles** : `help`
- Tapez `help` pour afficher la liste des commandes et leur utilisation.

### 4. **Afficher le pinyin d’un caractère chinois** : `getpinyin`
1. Tapez `getpinyin` suivi d'un espace.
2. Ajoutez le caractère chinois dont vous souhaitez obtenir le pinyin.
   
   **Exemple :** `getpinyin 你`

### 5. **Afficher la forme simplifiée d’un caractère chinois** : `getsimplified`
1. Tapez `getsimplified` suivi d'un espace.
2. Ajoutez le caractère chinois dont vous souhaitez obtenir la forme simplifiée.
   
   **Exemple :** `getsimplified 你`

### 6. **Afficher la forme traditionnelle d’un caractère chinois** : `gettraditional`
1. Tapez `gettraditional` suivi d'un espace.
2. Ajoutez le caractère chinois dont vous souhaitez obtenir la forme traditionnelle.
   
   **Exemple :** `gettraditional 你`

### 7. **Sauvegarder les données du dictionnaire** : `save`
1. Tapez la commande `save`.
2. Appuyez sur la touche `Entrée`.
3. L’application vous demandera de :
   - Choisir le caractère à enregistrer.
   - Spécifier le format du fichier (par exemple : `.txt`, `.json`, `.xml`, ).

### 8. **Afficher la traduction d’un caractère chinois** : `gettranslation`
1. Tapez `gettranslation` suivi d'un espace.
2. Ajoutez le caractère chinois dont vous souhaitez obtenir la traduction.
   
   **Exemple :** `gettranslation 你`

### 9. **Rechercher un mot ou un caractère** : `search`
1. Tapez `search` suivi d'un espace.
2. Ajoutez le mot ou le caractère chinois à rechercher.
3. Le dictionnaire affichera toutes les informations relatives à ce mot ou caractère.
   
   **Exemple :** `search 你`

### 10. **Annuler la dernière recherche effectuée** : `undo`
- Tapez simplement `undo` pour retirer le dernier mot ajouté à l’historique des recherches.

### 11. **Afficher l’historique des recherches** : `history`
- Tapez `history` pour afficher la liste des mots recherchés précédemment avec leur index.

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







