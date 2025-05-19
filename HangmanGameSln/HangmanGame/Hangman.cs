using System;
using HangmanRenderer.Renderer;

namespace Hangman.Core.Game
{
    public class HangmanGame
    {
        private GallowsRenderer _renderer;
        private string SecretWord = "Occupation";
        private char[] lettersOfWord;
        private bool[] guessLetters;
        private int attempts = 5;
        private bool wordComplete = false;
        private char[] correctGuesses;


        public HangmanGame()
        {
            _renderer = new GallowsRenderer();
        }

        public void Run()
        {

            // Guess the letter of the word.
            lettersOfWord = SecretWord.ToUpper().ToCharArray();
            guessLetters = new bool[lettersOfWord.Length];
            correctGuesses = new char[lettersOfWord.Length];
            for (int i = 0; i < correctGuesses.Length; i++)
                correctGuesses[i] = '-';

            while (attempts > 0 && !wordComplete)
            {
                _renderer.Render(5, 5, attempts);
                Console.SetCursorPosition(0, 13);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Your current guess: ");
                Console.WriteLine(new string(correctGuesses));
                Console.SetCursorPosition(0, 15);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("What is your next guess: ");
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) || input.Length != 1)
                    continue;

                char guess = char.ToUpper(input[0]);
                bool found = false;

                for (int i = 0; i < lettersOfWord.Length; i++)
                {
                    if (lettersOfWord[i] == guess && !guessLetters[i])
                    {
                        guessLetters[i] = true;
                        correctGuesses[i] = guess;
                        found = true;
                    }
                }

                if (!found)
                    attempts--;

                wordComplete = Array.TrueForAll(guessLetters, b => b);

                if (wordComplete)
                {
                    Console.WriteLine("Congratulations! You guessed the word!");
                }
                else if (attempts == 0)
                {
                    Console.WriteLine($"Game Over! The word was: {SecretWord}");
                }

            }
  
        }

    }
}
