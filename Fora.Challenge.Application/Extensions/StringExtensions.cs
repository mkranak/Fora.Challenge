namespace Fora.Challenge.Application.Extensions
{
    public static class StringExtensions
    {
        private static readonly HashSet<char> Vowels = ['a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U'];

        /// <summary>Determines whether the string starts with a vowel.</summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the string starts with a vowel; otherwise, <c>false</c>.</returns>
        public static bool IsStartingWithVowel(this string input)
        {
            return !string.IsNullOrEmpty(input) && Vowels.Contains(input[0]);
        }
    }
}
