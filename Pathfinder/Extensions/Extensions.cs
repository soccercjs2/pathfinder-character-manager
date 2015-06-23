using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NCalc;

namespace Pathfinder.Extensions
{
    public static class Extensions
    {
        public static string Beautify(this string equation)
        {
            return Beautify(equation, "");
        }

        public static string Beautify(string equation, string dice)
        {
            if (equation.Contains("d"))
            {
                //initialize index variables
                int dIndex = equation.IndexOf("d");
                int prefixStart = dIndex - 1;
                int suffixEnd = dIndex + 1;

                //calculate beginning and end of dice
                while (prefixStart - 1 > 0 && Char.IsNumber(equation, prefixStart - 1)) { prefixStart--; }
                while (suffixEnd < equation.Length && Char.IsNumber(equation, suffixEnd)) { suffixEnd++; }

                //find dice
                string diceFound = equation.Substring(prefixStart, suffixEnd - prefixStart);

                //recurse!
                string replacedEquation = equation.Substring(0, prefixStart) + "0";
                if (suffixEnd <= replacedEquation.Length) { replacedEquation += equation.Substring(suffixEnd + 1); }
                return Beautify(replacedEquation, dice + diceFound + " + ");
            }
            else
            {
                Expression expression = new Expression(equation, EvaluateOptions.NoCache);
                return dice + Math.Floor(Convert.ToDecimal(expression.Evaluate())).ToString();
            }
        }
    }
}