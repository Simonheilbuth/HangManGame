using System;
using System.Collections.Generic;

namespace HangmanGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set up the game
            string wordToGuess = GetRandomWord();
            char[] guessedWord = InitializeGuessedWord(wordToGuess.Length);
            List<char> guessedLetters = new List<char>();
            int attempts = 6;

            Console.WriteLine("Welcome to Hangman!");

            // Main game loop
            while (attempts > 0)
            {
                DisplayGameState(guessedWord, attempts);
                char playerGuess = GetPlayerGuess(guessedLetters);

                if (ProcessGuess(playerGuess, wordToGuess, guessedWord))
                {
                    Console.WriteLine("Correct!");
                    if (CheckWin(guessedWord, wordToGuess))
                    {
                        Console.WriteLine($"\nCongratulations! You've guessed the word: {wordToGuess}");
                        break;
                    }
                }
                else
                {
                    attempts--;
                    Console.WriteLine("Incorrect.");
                }
            }

            if (attempts == 0)
            {
                Console.WriteLine($"\nGame Over! The word was: {wordToGuess}");
            }
        }

        static string GetRandomWord()
        {
            List<string> wordList = new List<string> { "programming", "computer", "hangman", "developer", "coding" };
            Random random = new Random();
            return wordList[random.Next(wordList.Count)];
        }

        static char[] InitializeGuessedWord(int length)
        {
            return new string('_', length).ToCharArray();
        }

        static void DisplayGameState(char[] guessedWord, int attempts)
        {
            Console.WriteLine($"\nWord: {new string(guessedWord)}");
            Console.WriteLine($"Attempts left: {attempts}");
        }

        static char GetPlayerGuess(List<char> guessedLetters)
        {
            char guess;
            do
            {
                Console.Write("Guess a letter: ");
                guess = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (guessedLetters.Contains(guess))
                {
                    Console.WriteLine("You already guessed that letter. Try a different one.");
                }
            } while (guessedLetters.Contains(guess));

            guessedLetters.Add(guess);
            return guess;
        }

        static bool ProcessGuess(char guess, string wordToGuess, char[] guessedWord)
        {
            bool correctGuess = false;
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (wordToGuess[i] == guess)
                {
                    guessedWord[i] = guess;
                    correctGuess = true;
                }
            }
            return correctGuess;
        }

        static bool CheckWin(char[] guessedWord, string wordToGuess)
        {
            return new string(guessedWord) == wordToGuess;
        }
    }
}
