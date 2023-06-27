using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace CharacterBuilderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CHARACTER_FILE_PATH = "F:\\Repos\\Pathfinder-Character-Creator\\ConsoleApp\\Characters\\Characters.JSON";
            int mainMenuChoice;
            List<PathfinderCharacter> characters = JsonConvert.DeserializeObject<List<PathfinderCharacter>>(File.ReadAllText(CHARACTER_FILE_PATH));
            //Display start menu with the option for characters, homebrew, and Pathfinder Data
            DisplayIntro();
            //Upon selecting characters, show the list of characters and an add character button
            //Upon selecting a character, display detailed information about that character and give the option to edit or detete
            //Upon selecting homebrew, show homebrew packages
            //Upon selecting a package, show all the elements of the package and an add function
            //Upon selecting an element of a package, show edit or remove
            //Upon selecting edit, ask what to edit

            Console.Write(characters[0].name + "\n" + characters[0].level + "\n" + characters[0].pathfinderClass + "\n");
            Console.Write(characters[1].name + "\n" + characters[1].strength + "\n" + characters[1].skills[2].modifier);
            Console.ReadLine();
        }
        public static void DisplayIntro()
        {
            Console.Write("Welcome to Meshach's Pathfinder Character Creator!\nPlease select an option.\n> ");
        }
        static void DisplayMainMenu()
        {

        }

    }
    public class Skill
    {
        public string name { get; set; }
        public int trainingLevel { get; set; }
        public string attribute { get; set; }
        public int modifier { get; set; }
    }
    public class PathfinderCharacter
    {
        public string name { get; set; }
        public string pathfinderClass { get; set; }
        public int level { get; set; }
        public int strength { get; set; }
        public int dexterity { get; set; }
        public int constitution { get; set; }
        public int intelligence { get; set; }
        public int wisdom { get; set; }
        public int initiative { get; set; }
        public IList<Skill> skills { get; set; }
        public void RemoveCharacter(PathfinderCharacter character)
        {

        }
    }
    class PathfinderClass
    {

    }
    class PathfinderAncestry
    {

    }
    class PathfinderBackground
    {

    }


}
