using System;
using System.Collections.Generic;
using System.Linq;

class NumberList
{
    static void Main()
    {
        List<int> numbers = new List<int>();
        
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        
        int number;
        do
        {
            Console.Write("Enter number: ");
            number = int.Parse(Console.ReadLine());
            
            if (number != 0)
            {
                numbers.Add(number);
            }
        } while (number != 0);
        
        int sum = numbers.Sum();
        double average = numbers.Average();
        int largest = numbers.Max();
        
        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {largest}");


        int smallestPositive = numbers.Where(n => n > 0).Min();
        List<int> sortedNumbers = numbers.OrderBy(n => n).ToList();
        
        Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        
        Console.WriteLine("The sorted list is:");
        foreach (var num in sortedNumbers)
        {
            Console.WriteLine(num);
        }
    }
}
