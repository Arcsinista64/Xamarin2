using System;
using System.Collections.Generic;
using System.Text;

namespace Phoneword
{
    public static class PhonewordTranslator
    {
        private static string[] digits =
        {
            "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
        };

        static int? TranslateToNumber(char c)
        {
            for(int i = 0; i < digits.Length; i++)
            {
                if(digits[i].Contains(c.ToString()))
                {
                    return 2 + i;
                }      
            }
            return null;
        }

        public static string ToNumber(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return null;

            word = word.ToUpper();

            var newNumber = new StringBuilder();
            foreach(var c in word)
            {
                if(" -0123456789".Contains(c.ToString()))
                {
                    newNumber.Append(c);
                }
                else
                {
                    var result = TranslateToNumber(c);
                    if(result != null)
                    {
                        newNumber.Append(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return newNumber.ToString();
        }
    }
}
