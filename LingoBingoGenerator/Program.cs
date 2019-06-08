using System;
using System.Collections.Generic;


namespace LingoBingoGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // set the Console Window size
            Console.SetWindowSize(150, 40);

            // minimum 24 items
            string[] csharpLingo = {"method", "inheritance", "class", "object", "instance", "property", "field", "constructor",
                                    "dot net", "array", "main", "curly brace", "string", "boolean", "semicolon", "parse",
                                    "try catch", "partial", "return", "call", "override", "keyword", "get or set", "static"};

            string[] emcommLingo = { "Wavelength", "Dipole", "Vertical", "Frequency", "FM", "ARES", "RACES", "Beacon",
                                    "Repeater", "Simplex", "ACS", "Exercise", "ICS", "Forms", "J-Pole", "Agency Served",
                                    "Tactical", "Call Sign", "Identify", "Pro Words", "I Spell", "Mixed Group", "EMCOMM", "Activation"};

            string[] lingoArray = emcommLingo;
            List<string> lingoList = new List<string> ();
            foreach(string term in lingoArray) // TODO: Validate using a List is simpler than an array of strings
            {  // this could be used to read-in values rather than having a static set ala lingoArray[]
                lingoList.Add(term);
                // Console.Write($"{term},\n");
            }
            Console.WriteLine();

            // Instantiate BingoBoard using lingoArray (for now)
            BingoBoard alpha = new BingoBoard(lingoArray);
            
            // send the array to a static function that will "draw" the board
            Console.WriteLine(alpha.GetBoard());
            
            // Pause output before exiting program
            _ = Console.ReadLine();
        }
    }
}
