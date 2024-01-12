using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure
{
    public static class Randomizer
    {
        private static char[] numArr = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        private static char[] upperAlphaArr = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private static char[] lowerAlphaArr = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        private static char[] specialCharArr = new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+' };

        /// <summary>
        /// 生成随机字符串
        /// </summary>
        /// <param name="length">生成字符串长度</param>
        /// <param name="exceptChar">排除字符</param>
        /// <param name="hasNumbers">是否包含数字</param>
        /// <param name="hasLowerAlphabets">是否包含小写字母</param>
        /// <param name="hasUpperAlphabets">是否包含大写字母</param>
        /// <param name="hasSpecialChars">是否包含特殊字符</param>
        /// <returns></returns>
        public static string Next(int length, char[] exceptChar = default, bool hasNumbers = true, bool hasLowerAlphabets = true, bool hasUpperAlphabets = true, bool hasSpecialChars = true)
        {
            var bags = new List<char>();
            if (hasNumbers)
            {
                bags.AddRange(numArr);
            }

            if (hasLowerAlphabets)
            {
                bags.AddRange(lowerAlphaArr);
            }

            if (hasUpperAlphabets)
            {
                bags.AddRange(upperAlphaArr);
            }

            if (hasSpecialChars)
            {
                bags.AddRange(specialCharArr);
            }

            if (exceptChar != null)
            {
                bags = bags.Except(exceptChar).ToList();
            }

            var random = new Random();
            var randomArr = new char[length];

            while (length > 0)
            {
                randomArr[length - 1] = bags[random.Next(0, bags.Count)];
                length--;
            }

            return new string(randomArr);
        }
    }
}
