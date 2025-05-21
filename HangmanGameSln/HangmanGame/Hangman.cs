using System;
using System.Security.Cryptography.X509Certificates;
using HangmanRenderer.Renderer;

namespace Hangman.Core.Game
{
    public class HangmanGame
    {
        private GallowsRenderer _renderer;
        private readonly string[] WordList = new string[]
{
    "Occupation", "Computer", "Programming", "Science", "Adventure",
    "Mountain", "Library", "Elephant", "Sunshine", "Chocolate",
    "Rainbow", "Journey", "Mystery", "Festival", "Harmony",
    "Galaxy", "Treasure", "Courage", "Whisper", "Victory"
};
        private string SecretWord;
        private char[] lettersOfWord;
        private bool[] guessLetters;
        private int attempts = 6;
        private bool wordComplete = false;
        private char[] correctGuesses;
        Random random = new Random();

        public HangmanGame()
        {
            _renderer = new GallowsRenderer();
        }


        private void ResetGame()
        {
            // Guess the letter of the word.
            SecretWord = WordList[random.Next(WordList.Length)];
            lettersOfWord = SecretWord.ToUpper().ToCharArray();
            guessLetters = new bool[lettersOfWord.Length];
            correctGuesses = new char[lettersOfWord.Length];
            for (int i = 0; i < correctGuesses.Length; i++)
                correctGuesses[i] = '-';
            attempts = 6;
            wordComplete = false;
        }


        public void Run()
        {

            while (true)
            {
                ResetGame();
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


                }

                if (wordComplete)
                {
                    Console.WriteLine("Congratulations! You guessed the word!");
                }
                else if (attempts == 0)
                {
                    Console.WriteLine($"Game Over! The word was: {SecretWord}");
                }


                _renderer.Render(5, 5, attempts);
                Console.SetCursorPosition(0, 13);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Your current guess: ");
                Console.WriteLine(new string(correctGuesses));
            }


        }

    }
}