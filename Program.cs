using System;
using System.Collections.Generic;
using static System.Random;
using System.Text;
using System.ComponentModel.Design;
using Microsoft.Data.Sqlite;
using static System.Formats.Asn1.AsnWriter;

namespace HangmanAppTest
{
    internal class Program
    {
        // Prints out the hangman as the player gets the guesses wrong
        private static void printHangman(int wrong)
        {
            if (wrong == 0)
            {
                Console.Clear();

                Console.WriteLine("\n+---+");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 1)
            {
                Console.Clear();
                Console.WriteLine("\n+---+");
                Console.WriteLine("O   |");
                Console.WriteLine("    |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 2)
            {
                Console.Clear();

                Console.WriteLine("\n+---+");
                Console.WriteLine("O   |");
                Console.WriteLine("|   |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 3)
            {
                Console.Clear();

                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|  |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 4)
            {
                Console.Clear();

                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("    |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 5)
            {
                Console.Clear();

                Console.WriteLine("\n+---+");
                Console.WriteLine(" O  |");
                Console.WriteLine("/|\\ |");
                Console.WriteLine("/   |");
                Console.WriteLine("   ===");
            }
            else if (wrong == 6)
            {
                Console.Clear();

                Console.WriteLine("\n+---+");
                Console.WriteLine(" O   |");
                Console.WriteLine("/|\\  |");
                Console.WriteLine("/ \\  |");
                Console.WriteLine("    ===");
            }
        }


        private static void victoryScreen()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                Console.WriteLine("  O   ");
                Console.WriteLine(" /|\\  ");
                Console.WriteLine(" / \\  ");
                Thread.Sleep(500);
                Console.Clear();

                Console.WriteLine(" \\O/   ");
                Console.WriteLine("  |  ");
                Console.WriteLine(" / \\ ");
                Thread.Sleep(500);
                Console.Clear();

            }
        }

        private static int getWord(List<char> guessedLetters, String randomWord)
        {
            int counter = 0;
            int correctGuess = 0;
            Console.Write("\r\n");
            foreach (char c in randomWord)
            {
                if (guessedLetters.Contains(c))
                {
                    Console.Write(c + " ");
                    correctGuess += 1;
                }
                else
                {
                    Console.Write("  ");
                }
                counter += 1;
            }
            return correctGuess;
        }

        private static void printLines(String randomWord)
        {
            Console.Write("\r");
            // Creates the small _ _ _ _ lines for however many characters are in the words. 
            foreach (char c in randomWord)
            {
                Console.Write("\u0305 ");
            }
        }


 


        static void Main(string[] args)
        {



            Console.WriteLine("Welcome to hangman!!!");
            Console.WriteLine("The theme for this game is Video games :)");
            Console.WriteLine("-----------------------------------------");

            Random random = new Random();
            List<string> wordDictionary = new List<string> { "halo", "fallout", "zelda", "pokemon", "ori", "phasmophobia", "watchdogs", "lastofus", "spiderman" };
            int index = random.Next(wordDictionary.Count);
            String randomWord = wordDictionary[index];

            foreach (char x in randomWord)
            {
                Console.Write("_ ");

            }

            int lengthOfWordToGuess = randomWord.Length;
            int amountOfTimesWrong = 0;
            List<char> currentLettersGuessed = new List<char>();
            int currentLettersRight = 0;

            while (amountOfTimesWrong != 6 && currentLettersRight != lengthOfWordToGuess)
            {
                Console.Write("\nLetters guessed so far: ");
                foreach (char letter in currentLettersGuessed)
                {
                    Console.Write(letter + " ");
                }
                // Prompt user for input
                Console.Write("\nGuess a letter: ");
                char letterGuessed = Console.ReadLine()[0];
                // Check if that letter has already been guessed
                if (currentLettersGuessed.Contains(letterGuessed))
                {
                    Console.Write("\r\n You have already guessed this letter");
                    printHangman(amountOfTimesWrong);
                    currentLettersRight = getWord(currentLettersGuessed, randomWord);
                    printLines(randomWord);
                }
                else
                {
                    // Check if letter is in randomWord
                    bool right = false;
                    for (int i = 0; i < randomWord.Length; i++) { if (letterGuessed == randomWord[i]) { right = true; } }
                    // User is right
                    if (right)
                    {
                        printHangman(amountOfTimesWrong);

                        // Print word
                        currentLettersGuessed.Add(letterGuessed);
                        currentLettersRight = getWord(currentLettersGuessed, randomWord);
                        Console.Write("\r\n");
                        printLines(randomWord);
                        Console.Write("\nYou got it Right :)");

                    }
                    // User was incorrect
                    else
                    {
                        amountOfTimesWrong += 1;
                        currentLettersGuessed.Add(letterGuessed);
                        // Update hangman drawing
                        printHangman(amountOfTimesWrong);
                        Console.Write("Oh no! You got it wrong!");
                        // Print word
                        currentLettersRight = getWord(currentLettersGuessed, randomWord);
                        Console.Write("\r\n");
                        printLines(randomWord);
                    }
                }
            }
            
            //Add Win screen
            if (currentLettersRight == lengthOfWordToGuess) 
            {
             
                    Console.WriteLine("Congratulations! You've guessed the word correctly and won the game");
                victoryScreen()
            }
            else
            {
                Console.WriteLine("Game Over! The hangman has been hanged :( ");
                Console.WriteLine("Try Again!!!");
            }



            Console.WriteLine("\r\n The game is over!!!!! Thank you for playing :)");
        }
    }
}