using System;
using System.Numerics;

class Program
{
    static void Main()
    {
        Vector<float> vectorA = new Vector<float>(1.0f);
        Vector<float> vectorB = new Vector<float>(2.0f);
        Vector<float> result = vectorA + vectorB;

        Console.WriteLine($"Result: {result}");
    }
}
