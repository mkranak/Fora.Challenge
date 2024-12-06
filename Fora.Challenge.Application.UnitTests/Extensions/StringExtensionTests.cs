using Fora.Challenge.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Challenge.Application.UnitTests.Extensions
{
    public class StringExtensionTests
    {
        [Theory]
        [MemberData(nameof(GetVowelTestData))]
        public void OnIsStartingWithVowel_WhenStringStartsWithVowel_ExpectTrue(string input)
        {
            // Arrange
            var expected = true;

            // Act
            var actual = input.IsStartingWithVowel();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(GetNonVowelTestData))]
        public void OnIsStartingWithVowel_WhenStringDoesNotStartsWithVowel_ExpectFalse(string input)
        {
            // Arrange
            var expected = false;

            // Act
            var actual = input.IsStartingWithVowel();

            // Assert
            Assert.Equal(expected, actual);
        }

        public static IEnumerable<object[]> GetNonVowelTestData()
        {
            var testData = new List<object[]>
            {
                new object[] { "" },
                new object[] { " " },
                new object[] { "bravo" },
                new object[] { "Bravo" },
                new object[] { "@" }
            };

            foreach (var data in testData)
            {
                yield return data;
            }
        }

        public static IEnumerable<object[]> GetVowelTestData()
        {
            var testData = new List<object[]>
            {
                new object[] { "alpha" },
                new object[] { "Alpha" },
                new object[] { "echo" },
                new object[] { "Echo" },
                new object[] { "india" },
                new object[] { "India" },
                new object[] { "october" },
                new object[] { "October" },
                new object[] { "uniform" },
                new object[] { "Uniform" }
            };

            foreach (var data in testData)
            {
                yield return data;
            }
        }
    }
}
