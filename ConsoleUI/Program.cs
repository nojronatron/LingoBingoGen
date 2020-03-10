using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LingoBingoGenerator;

namespace ConsoleUI
{
    class Program
    {
        //  run this exe to display a bingo board using ASCII characters
        //  must:
        //  take input from a user
        //  use an existing source of words
        //  use LingoGenerator to set words, GetRandomizedList, get LongestWordLen
        //  have an ability to generate multiple, randomized boards
        //  properly set the FREE space at index 11
        //  properly draw the ASCII board with one word 

        static void Main(string[] args)
        {
            DrawMenuSystem();
            LingoGenerator Words = new LingoGenerator();
            //  Words.LingoWords(List<string> or string[])



        }
        static void DrawMenuSystem()
        {
            Console.WriteLine($"Placeholder: Draws a menu system for user input.");
        }
    }
}
