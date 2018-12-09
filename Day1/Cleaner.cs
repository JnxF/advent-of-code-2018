using System;
using System.Collections.Generic;
using System.Text;

namespace Day1
{
    static class Cleaner
    {
        public static int CleanNumberString(string number)
        {
            if (number == "") return 0;
            if (number[0] == '+') return int.Parse(number.Substring(1));
            else return int.Parse(number);
        }
    }
}
