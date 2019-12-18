using System;

namespace FunctionalProgrammingInCSharp.Excersises2
{
    public class BmiConsole
    {
        public void Run(Func<string> read, Action<string> write)
        {
            var height = Read(read, write, "height in meters");
            var weight = Read(read, write, "weight in kilograms");

            double bmi = CalculateBmi(height, weight);
            write($"Your BMI is {bmi}");
            string bmiResult = GetBmiResult(bmi);
            write($"Your are {bmiResult}");
        }

        public static string GetBmiResult(double bmi)
        {
            return bmi < 18.5 ? "underweight" : (bmi > 25) ? "overweight" : "at healthy weight";
        }

        public static double CalculateBmi(double height, double weight)
        {
            return weight / (height * height);
        }

        private static double Read(Func<string> read, Action<string> write, string what)
        {
            double result;

            bool parsed = false;
            do
            {
                write($"What is your {what}?");

                parsed = double.TryParse(read(), out result);
            } while (!parsed);
            return result;
        }
    }
}
