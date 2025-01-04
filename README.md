# **ChineseDictionary**

**ChineseDictionary** est une application console écrite en C# qui permet d'interagir avec un dictionnaire chinois-français. Ce programme offre des fonctionnalités variées, telles que :
- La recherche de mots chinois.
- L'affichage des pinyin, formes simplifiées ou traditionnelles.
- L'ajout de nouvelles entrées au dictionnaire.
- La sauvegarde des données dans divers formats.
- La gestion multilingue (français et anglais) pour les commandes.
- L'importations des mots à partir de fichiers CSV ou texte puis l'enregistrer dans le fichier xml principal.


Ce projet a été réalisé dans le cadre du cours de Programmation Orientée Objet en C# au premier semestre de Master 2.

---

## **Structure du projet**

### **1. Dossier `Commands`**
Ce dossier contient les classes responsables de l'exécution des différentes commandes disponibles dans l'application.

- **`AddCommand.cs`** : Ajoute une nouvelle entrée au dictionnaire.
- **`ExitCommand.cs`** : Quitte proprement l'application.
- **`HelpCommand.cs`** : Liste toutes les commandes disponibles avec leur description.
- **`GetPinyinCommand.cs`** : Affiche le pinyin d'un caractère ou mot chinois.
- **`GetSimplifiedCommand.cs`** : Affiche la forme simplifiée d'un caractère ou mot chinois.
- **`GetTraditionalCommand.cs`** : Affiche la forme traditionnelle d'un caractère ou mot chinois.
- **`GetTranslationCommand.cs`** : Affiche la traduction française d'un caractère ou mot chinois.
- **`SearchCommand.cs`** : Recherche un caractère ou mot chinois dans le dictionnaire.
- **`SaveCommand.cs`** : Sauvegarde les résultats dans un fichier (formats disponibles : JSON, TXT, XML).
- **`LoadCommand.cs`** : Permet de charger des mots à partir d'un fichier CSV ou texte enregistré dans le dossier Data.
- **`ChangeLanguageCommand.cs`** : Permet de basculer entre les langues disponibles (français/anglais).
- **`InvalidCommandException.cs`** : Gère les commandes inconnues avec des messages explicatifs.
- **`XmlCache.cs`** : Gère le chargement et la mise en cache du fichier XML principal contenant les données.

### **2. Dossier `Pile`**
Ce dossier contient la gestion de l'historique des recherches via la classe `HistoryStack`. Les fonctionnalités incluent :
- **Ajouter un mot à l'historique** : `AddToHistory(string word)`.
- **Récupérer le dernier mot recherché** : `GetLastSearch()`.
- **Annuler la dernière recherche** : `UndoLastSearch()`.
- **Afficher l'historique complet** : `ShowHistory()`.
- **Afficher un mot spécifique par index** : `ShowSpecificHistory(int index)`.

### **3. Dossier `Data`**
- **`cfdict.xml`** : Le fichier principal contenant les données du dictionnaire (mots, pinyin, formes simplifiées/traditionnelles, traductions).
- - **`words.csv`** : Fichier d'exemple pour tester l'importation de données via la commande load.

### **4. Dossier `Main`**
- **`Program.cs`** : Point d'entrée principal de l'application. C'est ici que l'exécution commence.

---

## **XmlCache.cs : Gestion optimisée des données**

La classe **`XmlCache`** est responsable de la gestion du fichier XML principal contenant les données du dictionnaire. Elle optimise les performances en mettant en cache le contenu du fichier pour éviter de le charger plusieurs fois inutilement.

### **Fonctionnalités principales :**

1. **Chargement initial des données :**
   - La méthode `GetDocument()` charge le fichier XML situé dans `./Data/cfdict.xml` lors du premier appel.
   - Si le document est déjà chargé, il renvoie la version mise en cache pour éviter des lectures répétées sur disque.

2. **Actualisation des données :**
   - La méthode `RefreshDocument()` recharge le fichier XML depuis le disque, utile si le fichier a été modifié ou mis à jour pendant l'exécution du programme.

## **Commandes disponibles et leurs usages**

Voici une liste détaillée de toutes les commandes disponibles dans l'application.

### **1. Ajouter un mot dans le dictionnaire** : `add`
Ajoutez un nouveau mot dans le dictionnaire.
Créer automatiquement une sauvegarde avant modification `cfdict_backup.xml`

**Exemple :**
```plaintext
> add
Ajout d'un nouveau mot au dictionnaire
Entrez la forme traditionnelle du mot : 肉夹馍
Entrez la forme simplifiée du mot : 肉夹馍
Entrez le pinyin du mot : rou4jia1mo2
Entrez les traductions (séparées par des virgules) : viande insérée dans du pain,  une sorte de petit sandwich constitué d'un petit pain rond, au milieu duquel on place de la viande
Le mot a été ajouté avec succès !
ID : 362090
Forme Traditionnelle : 肉夹馍
Forme Simplifiée : 肉夹馍
Pinyin : rou4jia1mo2
Traductions : 
  - viande insérée dans du pain
  - une sorte de petit sandwich constitué d'un petit pain rond
  - au milieu duquel on place de la viande
```

### **2. Quitter l'application** : `exit`
Fermez l'application proprement.

**Exemple :**
```plaintext
> exit
Au revoir !
```

### **3. Afficher les commandes disponibles** : `help`
Affiche la liste complète des commandes disponibles.

**Exemple :**
```plaintext
> help
--- Commandes disponibles ---
1. help                         - Affiche cette liste d'aide.
2. exit                         - Quitte le programme.
...
```

### **4. Afficher le pinyin d’un caractère chinois** : `getpinyin`
Affiche le pinyin d’un caractère.

**Exemple :**
```plaintext
> getpinyin 妳
Le pinyin de 妳 est : nǐ
```

### **5. Sauvegarder les données du dictionnaire** : `save`
Sauvegarde les données dans un fichier.

**Exemple :**
```plaintext
> save
Entrez le mot que vous souhaitez enregistrer : 妳
Quel format voulez-vous utiliser pour enregistrer ? (xml, txt, json) : json
Résultats sauvegardés dans 妳_result.json
```

### **6. Changer la langue** : `changelanguage`
Permet de basculer entre le français et l'anglais pour 'help'.

**Exemple :**
```plaintext
> changelanguage en
Language switched to: English.
Help messages will now be displayed in English.
```

### **7. Supprimer un mot** : `remove`
Permet de supprimer un mot choisi par l'utilisateur dans le fichier xml.

**Exemple :**
```plaintext
> remove 肉夹馍
Le mot '肉夹馍' a été trouvé et sera supprimé.
Le mot a été supprimé avec succès.
```

### **8. Afficher le caractère simplifié d’un caractère chinois tradictionnel** : `getsimplified`
Affiche le caractère simplifié d’un caractère chinois tradictionnel.

**Exemple :**
```plaintext
> getsimplified 張家界
La forme simplifiée de 張家界 est : 张家界
```

### **9. Afficher la forme traditionnelle d’un caractère chinois simplifié** : `gettraditional`
Affiche le caractère traditionnel d’un caractère chinois simplifié.

**Exemple :**
```plaintext
> gettraditional 张家界
La forme traditionnelle de 张家界 est : 張家界
```

### **10. Afficher la traduction d’un caractère chinois** : `gettranslation`
Affiche la traduction en français d’un caractère chinois simplifié ou traditionnel.

**Exemple :**
```plaintext
> gettranslation 张家界
Traductions pour 张家界 :
- ville de Zhangjiajie
```

### **11. Annuler la dernière recherche effectuée** : `undo`
Supprime la dernière recherche effectuée de l'historique.

**Exemple :**
```plaintext
> undo
Recherche annulée avec succès : 妳
```

### **12. Afficher l’historique des recherches** : `history`
Affiche la liste des mots précédemment recherchés dans l'ordre chronologique.
Affiche un mot spécifique en fonction de l'index fourni. `history 1`

**Exemple :**
```plaintext
> history
Historique des recherches :
1. 你好
2. 妳
3. 张家界


> history 1
Mot #1 dans l'historique : 张家界
```
### **13. Annuler la dernière recherche effectuée** : `undo`
Supprime la dernière recherche effectuée de l'historique.

**Exemple :**
```plaintext
> undo
Recherche annulée avec succès : 妳
```

### **14. Importer des mots depuis un fichier CSV/TXT** : `load`
Ajoute les mots depuis un fichier (CSV ou TXT) dans le dictionnaire principal.
Il faut vous assurer que le fichier txt ou csv soit bien enregistrer dans le dossier Data.

**Exemple :**
```plaintext
> load words.csv
Chemin absolu interprété : /home/zia/Téléchargements/M2_S1/ChineseDictionary/Data/words.csv
Chargement du fichier : /home/zia/Téléchargements/M2_S1/ChineseDictionary/Data/words.csv
Veuillez entrer le délimiteur utilisé dans le fichier (par défaut ',') : ,
Voulez-vous sauvegarder ces données dans le dictionnaire principal ? (y/n) : y
Mots sauvegardés dans cfdict.xml avec succès !
```

### **15. Rechercher un mot ou un caractère :** : `search`
La commande search permet de rechercher un mot ou un caractère chinois dans le dictionnaire. Elle retourne toutes les informations associées au mot recherché, notamment :
- La forme traditionnelle.
- La forme simplifiée.
- Le pinyin.
- Les traductions en français.

**Exemple :**
```plaintext
> search 妳
Informations pour le mot : 妳
Forme Traditionnelle : 妳
Forme Simplifiée : 妳
Pinyin : ni3
Traductions : 
  - (arch.) toi (féminin)
  - tu (pour les femmes)
Ajouté à l'historique : 妳
```
---

## **Prérequis techniques**

1. **Environnement :**
   - .NET 6.0 ou supérieur.
   - Visual Studio ou tout IDE compatible avec C#.

2. **Installation :**
   - Clonez le projet :  
     ```bash
     git clone https://github.com/zia-sudo/ChineseDictionary
     ```
   - Naviguez dans le répertoire cloné et lancez l'application :  
     ```bash
     dotnet build
     dotnet run
     ```

---

## **Auteur**
Léa MANET