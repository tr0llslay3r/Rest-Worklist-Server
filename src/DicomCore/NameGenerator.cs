using System;

namespace DicomCore
{
    public class NameGenerator
    {
        private static readonly Random m_Random = new Random();
        
        public static string GenerateNew()
        {
            var value = string.Empty;

            for (var i = 0; i < m_Random.Next(2, 5); i++)
            {
                value += NameHelper.Syllables[m_Random.Next(0, NameHelper.Syllables.Length)];
            }

            return value.UppercaseFirstLetter();
        }

        public static string GenerateDicomFirstAndLastName()
        {
            return string.Format("{0}^{1}", GenerateNew(), GenerateNew());
        }
    }
}