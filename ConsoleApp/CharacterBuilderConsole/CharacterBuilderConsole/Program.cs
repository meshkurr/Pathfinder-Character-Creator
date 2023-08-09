using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

//TODO
    //Fix data storage.
        // Now, we should hold our data in seperate JSON files and use a method to extract the names of data in a particliar folder
    //Finish PathfinderClass class structure.  Slowly adding functionality does not work.  We need to add everything at once, so do some research

namespace CharacterBuilderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            #region filepaths and main menu choices
            const string CHARACTER_FILE_PATH = "F:\\Repos\\Pathfinder-Character-Creator\\ConsoleApp\\Characters\\Characters.JSON";
            const string PATHFINDER_CLASS_FILE_PATH = @"F:\Repos\Pathfinder-Character-Creator\ConsoleApp\Pathfinder Data\ClassData.JSON";
            const int CHARACTERS_CHOICE = 1;
            const int PATHFINDER_DATA_CHOICE = 2;
            const int HOMEBREW_DATA_CHOICE = 3;
            #endregion
            #region pathfinder menu choices
            const int PATHFINDER_CLASS_CHOICE = 1;
            const int PATHFINDER_ANCESTRY_CHOICE = 2;
            const int PATHFINDER_HERITAGE_CHOICE = 3;
            const int PATHFINDER_BACKGROUND_CHOICE = 4;
            const int PATHFINDER_SKILL_FEAT_CHOICE = 5;
            const int PATHFINDER_GENERAL_FEAT_CHOICE = 6;
            const int PATHFINDER_ARCHTYPE_CHOICE = 7;
            const int PATHFINDER_GEAR_CHOICE = 8;
            const int PATHFINDER_WEAPON_CHOICE = 9;
            const int PATHFINDER_ARMOR_CHOICE = 10;
            #endregion
            int mainMenuChoice = 0;
            int pathfinderMenuChoice = 0;
            int characterChoice = 0;
            int detailedCharacterChoice = 0;
            List<PathfinderCharacter> characters = JsonConvert.DeserializeObject<List<PathfinderCharacter>>(File.ReadAllText(CHARACTER_FILE_PATH));
            List<PathfinderClass> pathfinderClasses = JsonConvert.DeserializeObject<List<PathfinderClass>>(File.ReadAllText(PATHFINDER_CLASS_FILE_PATH));
            PathfinderCharacter selectedCharacter;
            //Display start menu with the option for characters, homebrew, and Pathfinder Data
            DisplayIntro();
            mainMenuChoice = DisplayMainMenu();
            //Upon selecting characters, show the list of characters and an add character button
            if(mainMenuChoice == CHARACTERS_CHOICE)
            {
                characterChoice = DisplayCharacters(characters);
                if(characterChoice == characters.Count + 1) //If we chose to make a new character
                {
                    //Create a character
                    selectedCharacter = AddCharacter(pathfinderClasses);
                    characters.Add(selectedCharacter);
                }
                else //If we don't add a character, then just choose whatever the use chose.
                {
                    selectedCharacter = characters[characterChoice - 1];
                }
                //Upon selecting a character, display detailed information about that character and give the option to edit or detete
                detailedCharacterChoice = DisplayDetailedCharacter(selectedCharacter);
            }
            //Reupload character array to Characters.JSON

            //Upon selecting homebrew, show homebrew packages
            //Upon selecting a package, show all the elements of the package and an add function
            //Upon selecting an element of a package, show edit or remove
            //Upon selecting edit, ask what to edit

            //Upon selecting pathfinder, show pathfinder choices (class, ancestry, ect.)
            if (mainMenuChoice == PATHFINDER_DATA_CHOICE)
            {
                pathfinderMenuChoice = DisplayPathfinderMenu();
                if(pathfinderMenuChoice == PATHFINDER_CLASS_CHOICE)
                {
                    pathfinderMenuChoice = DisplayPathfinderClassMenu(pathfinderClasses);
                }
            }
            else
            {
                Console.Write("Pathfinder Menu Skipped\n");
            }
            string charactersDataString = JsonConvert.SerializeObject(characters.ToArray(), Formatting.Indented);
            File.WriteAllText(CHARACTER_FILE_PATH, charactersDataString);
            Console.ReadLine();
        }
        public static void DisplayIntro()
        {
            Console.Write("Welcome to Meshach's Pathfinder Character Creator!\n");
        }
        static int DisplayMainMenu()
        {
            int choice;
            Console.Write("Please select an option.\n");
            Console.Write("1 - Characters\n");
            Console.Write("2 - Pathfinder Data\n");
            Console.Write("3 - Homebrew Data\n> ");
            choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }
        static int DisplayHomebrewMenu()
        {
            return 0;
        }
        static int DisplayPathfinderMenu()
        {
            int choice = 0;
            Console.Write("Please select data to inspect\n");
            Console.Write("1. Classes\n");
            Console.Write("2. Ancestry\n");
            Console.Write("3. Heritage\n");
            Console.Write("4. Background\n");
            Console.Write("5. Skill Feats\n");
            Console.Write("6. General Feats\n");
            Console.Write("7. Archtypes\n");
            Console.Write("8. Gear\n");
            Console.Write("9. Weapons\n");
            Console.Write("10. Armor\n");
            choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }
        static int DisplayPathfinderClassMenu(List<PathfinderClass> classList)
        {
            int choice = 0;
            int counter = 1;
            for (int i = 0; i < classList.Count; i++)
            {
                Console.Write(i + 1 + " - " + classList[i].name + "\n");
                counter++;
            }
            Console.Write(counter + " - Add a class\n> ");
            choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }
        static int DisplayCharacters(List<PathfinderCharacter> characterList)
        {
            int choice = 0;
            int counter = 1;
            for (int i = 0; i < characterList.Count; i++)
            {
                Console.Write(i + 1 + " - " + characterList[i].name + "\n");
                counter++;
            }
            Console.Write(counter + " - Add a character\n> ");
            choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }
        static int DisplayDetailedCharacter(PathfinderCharacter character)
        {
            int choice = 0;
            Console.Write("Name - " + character.name + "\n");
            Console.Write("Level - " + character.level + "\n");
            Console.Write("Class - " + character.pathfinderClass.name + "\n");
            Console.Write("Initiative - " + character.initiative + "\n");
            Console.Write("Strength - " + character.strength + "\n");
            Console.Write("Dexterity - " + character.dexterity + "\n");
            Console.Write("Constitution - " + character.constitution + "\n");
            Console.Write("Intelligence - " + character.intelligence + "\n");
            Console.Write("Wisdom - " + character.wisdom + "\n");
            Console.Write("Charisma - " + character.charisma + "\n");
            for(int i = 0; i < character.skills.Count; i++)
            {
                Console.Write(character.skills[i].name + " - " + character.skills[i].modifier + "\n");
            }
            Console.Write("Select 1 to edit the character, 2 to delete the character, and 3 to return\n> ");
            choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }
        static PathfinderCharacter AddCharacter(List<PathfinderClass> classList)
        {
            PathfinderClass testClass = PathfinderClass.placeholder;
            PathfinderCharacter newCharacter = new PathfinderCharacter();
            int choice = 1;
            string name;
            int level;
            int pathfinderClassChoice;
            Console.Write("Enter the Character's Name\n> ");
            name = Console.ReadLine();
            newCharacter.name = name;
            Console.Write("Enter the Character's Level\n> ");
            level = Convert.ToInt32(Console.ReadLine());
            newCharacter.level = level;
            Console.Write("Select the character's Class\n");
            for(int i = 0; i < classList.Count; i++) //List classes from the class list
            {
                Console.Write((choice + i) + " " + classList[i].name + "\n");
            }
            pathfinderClassChoice = Convert.ToInt32(Console.ReadLine());
            while(pathfinderClassChoice < 1 || pathfinderClassChoice > PathfinderClass.classList.Count)
            {
                Console.Write("Please select a valid option\n> ");
                pathfinderClassChoice = Convert.ToInt32(Console.ReadLine());
            }
            newCharacter.pathfinderClass = classList[choice-1];
            Console.Write("Select the character's Ancestry\n");
            Console.Write("Select the character's Heritage\n");
            Console.Write("Select the character's Background\n");
            //newCharacter.pathfinderClass.name = PathfinderClass.classList[0];
            /**
            Console.Write("Initiative - " + character.initiative + "\n");
            Console.Write("Strength - " + character.strength + "\n");
            Console.Write("Dexterity - " + character.dexterity + "\n");
            Console.Write("Constitution - " + character.constitution + "\n");
            Console.Write("Intelligence - " + character.intelligence + "\n");
            Console.Write("Wisdom - " + character.wisdom + "\n");
            Console.Write("Charisma - " + character.charisma + "\n");
            for (int i = 0; i < character.skills.Count; i++)
            {
                Console.Write(character.skills[i].name + " - " + character.skills[i].modifier + "\n");
            }
            */
            return newCharacter;
        }
        static void RemoveCharacter(PathfinderCharacter characterToRemove, List<PathfinderCharacter> characterList)
        {
            characterList.Remove(characterToRemove);
        }
        static void RemovePathfinderClass(PathfinderClass classToRemove, List<PathfinderClass> classList)
        {
            classList.Remove(classToRemove);
        }
        static PathfinderClass SearchForPathfinderClassFromName(string className, List<PathfinderClass> classList)
        {
            for (int i = 0; i < classList.Count; i++)
            {
                if(className == classList[i].name)
                {
                    return classList[i];
                }
            }
            return PathfinderClass.placeholder;
        }
    }
    public class Skill //TODO finish skill system to allow for adding/removing skills, actually setting the skill modifier properly
    {
        public string name { get; set; }
        public int trainingLevel { get; set; }
        public string attribute { get; set; }
        public int modifier { get; set; }
        public void SetModifier(int level, int attributeLevel)
        {
            if(trainingLevel > 0)
            {
                modifier = attributeLevel + 2 * trainingLevel + 1;
            }
            else
            {
                modifier = attributeLevel;
            }
        }

        public static List<string> skillList = new List<string>() 
        {
            "Perception", 
            "Acrobatics", 
            "Athletics", 
            "Crafting", 
            "Deception", 
            "Diplomacy", 
            "Intimidation", 
            "Medicine", 
            "Nature", 
            "Occultism",
            "Performance",
            "Religion",
            "Society",
            "Stealth",
            "Survival",
            "Thievery",
        };
    }

    public class ClassFeat
    {
        public string name { get; set; }
        public int level { get; set; }
        public string sourceClass { get; set; }
        public bool hasActions { get; set; }
        public string actionType { get; set; }
        public int actionCount { get; set; }
    }

    public class PathfinderCharacter
    {
        public string name { get; set; }
        public PathfinderClass pathfinderClass { get; set; }
        public int level { get; set; }
        public int strength { get; set; }
        public int dexterity { get; set; }
        public int constitution { get; set; }
        public int intelligence { get; set; }
        public int wisdom { get; set; }
        public int initiative { get; set; }
        public int charisma { get; set; }
        public IList<Skill> skills { get; set; }
        public IList<ClassFeat> classFeats { get; set; }
    }


    public class PathfinderClass
    {
        public string name { get; set; }
        public string spellcastingType { get; set; }

        public string keyAbilityOne { get; set; }
        public string keyAbilityTwo { get; set; }
        public int hitPoints { get; set; }

        public static List<string> classList = new List<string>()
        {
            "Fighter",
            "Ranger",
            "Druid"
        };
        public PathfinderClass(string _name, string _spellcastingType)
        {
            name = _name;
            spellcastingType = _spellcastingType;
        }
        public IList<Skill> skills { get; set; }
        public string keyAbilityScore { get; set; }
        public static PathfinderClass placeholder = new PathfinderClass("Placeholder", "Testcaster");
    }
    class PathfinderAncestry
    {

    }
    class PathfinderBackground
    {

    }
}
