using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pathfinder.Models
{
    public class Roller
    {
        public string Name { get; set; }
        public string RollString { get; set; }
        public string RollResult { get; set; }
        public string Equation { get; set; }

        public Roller() { }

        public Roller(string name, string rollString)
        {
            this.Name = name.Replace("_", " ");
            this.Equation = GenerateRollString(rollString);
            this.RollString = Equation.Replace("'", "");
        }

        private string GenerateRollString(string rollString)
        {
            rollString = rollString.Replace("p", " + ");
            rollString = rollString.Replace("m", " - ");
            rollString = rollString.Replace("t", " x ");
            rollString = rollString.Replace("div", " / ");

            return rollString;
        }

        public void Roll()
        {
            String equation = ConvertDice(this.Equation);

            Expression expression = new Expression(equation);
            string answer = expression.Evaluate().ToString();
            this.RollResult = "Result: " + answer;
        }

        private string ConvertDice(string equation)
        {
            if (equation.Contains("'") == false)
            {
                return equation;
            }
            else
            {
                int firstQuoteIndex = equation.IndexOf("'");
                int secondQuoteIndex = firstQuoteIndex + equation.Substring(firstQuoteIndex + 1).IndexOf("'") + 1;
                string prefix = equation.Substring(0, firstQuoteIndex);

                string dieString = equation.Substring(firstQuoteIndex + 1, secondQuoteIndex - (firstQuoteIndex + 1));
                int dieResult = convertDie(dieString);

                return prefix + dieResult + ConvertDice(equation.Substring(secondQuoteIndex + 1));
            }
        }

        private int convertDie(string dieString)
        {
            string[] numbers = dieString.Split('d');
            int numberOfDice = Int32.Parse(numbers[0]);
            int sidesOnDie = Int32.Parse(numbers[1]);
            int total = 0;

            for (int i = 0; i < numberOfDice; i++)
            {
                Random random = new Random();
                total += random.Next(sidesOnDie) + 1;
            }

            return total;
        }
    }
}