using System;
using System.Collections.Generic; //TODO: Use Generic Collections rather than simple Arrays


namespace LingoBingoGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // minimum 24 items
            string[] csharpLingo = {
                "method", "inheritance", "class", "object", "instance", "property", "field", "constructor",
                "dot net", "array", "main", "curly brace", "string", "boolean", "semicolon", "parse",
                "try catch", "partial", "return", "call", "override", "keyword", "get or set", "static",
                "interface", "base", "virtual", "object", "abstract", "tryparse"
            };

            string[] emcommLingo = {
                "Wavelength", "Dipole", "Vertical", "Frequency", "FM", "ARES", "RACES", "Beacon",
                "Repeater", "Simplex", "ACS", "Exercise", "ICS", "Forms", "J-Pole", "Agency Served",
                "Tactical", "Call Sign", "Identify", "Pro Words", "I Spell", "Mixed Group", "EMCOMM",
                "Activation", "Operational Period", "ICP", "Field Agent", "Oregon ACES"
            };

            string[] nemcoLeadershipLingo = {
                "Traffic", "Data", "Participate", "Agency Served",
                "Agenda", "Exercise", "RACES", "Meeting", "Frequency/ies",
                "Lead", "Committee", "Drill", "Presentation", "Training",
                "Event(s)", "Calendar", "Repeater", "Antenna", "Field Day",
                "Plan", "Messages", "Feedback", "Report", "ICS",
                "Organization", "Message", "Formal", "Form", "Net",
                "WinLink", "Kenmore", "Lake Forest Park", "LFP", "NUD",
                "FS51", "Fire Station", "District", "Utility", "Credential",
                "NEMCo"
            };

            // Instantiate BingoBoard using an Array of lingo
            BingoBoard alpha = new BingoBoard(nemcoLeadershipLingo);

            // set the Console Window size
            int windowX = alpha.GetHorizontalFrameLength();
            Console.SetWindowSize(windowX + 1, 26); // might as well set WindowSize

            try
            {
                // Ask user for number of boards to generate
                Console.WriteLine($"Enter the number of boards you want to create (up to {(int)alpha.Phrases.Length}): ");
                int numBoards = int.Parse(Console.ReadLine());
                if (numBoards < 1) { numBoards = 1; }
                if (numBoards > (int)alpha.Phrases.Length) { numBoards = (int)alpha.Phrases.Length; }
                Console.Clear();

                // send the array to a static function that will "draw" the board
                for (int i = 0; i < numBoards; i++)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(alpha.MakeBoard());
                    Console.ResetColor();
                    Console.Write("Press <Enter> key to continue. . .");
                    _ = Console.ReadLine();
                    Console.Clear();
                }
                // reset console colors
                Console.ResetColor();
            }
            catch (FormatException fe)
            {
                Console.WriteLine($"Error! {fe.Message}\nUse only whole number digits.\n\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nERROR! Something unexpected happened.\n{ex.Message}");
                Console.WriteLine($"\nBase Exception: {ex.GetBaseException().ToString()}\n\n");
            }

            // Pause output before exiting program
            Console.WriteLine("Press the <Enter> key to exit. . .");
            _ = Console.ReadLine();
        }
    }
}
