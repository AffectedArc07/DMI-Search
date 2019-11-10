using DMISharp;
using System;
using System.IO;

namespace DMI_Search {
    class Program {
        static void Main(string[] args) {
            if (args.Length != 1) {
                Console.WriteLine("Invalid arguments. Please supply 1 argument, the path of your repository");
                return;
            }
            string repoDirectory = args[0];
            // Print some stuff
            Console.WriteLine("DMI-Search by AffectedArc07");
            Console.WriteLine("Searches all DMIs in all repo folders (" + repoDirectory + ") for a specific state.");
            // Ask what they want
            Console.Write("Enter search term: ");
            string searchTerm = Console.ReadLine();

            // Now search
            string[] allFiles = Directory.GetFiles(repoDirectory, "*.dmi", SearchOption.AllDirectories);
            Console.WriteLine(allFiles.Length + " DMI files found. Searching all files for the icon state \"" + searchTerm + "\".");
            Int16 matches = 0;
            foreach(string fileName in allFiles) {
                try {
                    using (DMIFile dmi = new DMIFile(fileName)) {
                        foreach (DMIState state in dmi.States) {
                            if (state.Name == searchTerm) {
                                matches += 1;
                                Console.WriteLine("MATCH FOUND: State \"" + state.Name + "\" found in file \"" + fileName + "\".");
                            }
                        }
                    }
                } catch {
                    continue;
                }
                
            }
            Console.WriteLine("Total matches found: " + matches);
            Console.Write("Press enter to close ");
            Console.ReadLine();
        }
    }
}
