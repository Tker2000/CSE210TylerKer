using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter your grade percentage: ");
        int gradePercentage = int.Parse(Console.ReadLine());

        string letter = "";
        string sign = "";

        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        if (letter != "F") // F grades do not have a + or -
        {
            int lastDigit = gradePercentage % 10;
            if (lastDigit >= 7)
            {
                sign = "+";
            }
            else if (lastDigit < 3)
            {
                sign = "-";
            }
        }

        string finalGrade = letter + sign;
        Console.WriteLine($"Your grade is: {finalGrade}");
        
        if (gradePercentage >= 70)
        {
            Console.WriteLine("Congratulations, you passed the course!");
        }
        else
        {
            Console.WriteLine("Better luck next time! Keep working hard!");
        }
    }
}
