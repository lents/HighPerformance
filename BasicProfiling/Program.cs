using System;

class Program
{
    static void Main(string[] args)
    {
        int n = 50;
        Console.WriteLine(Fibonacci(n));
    }

    static long Fibonacci(int n)
    {
        if (n <= 1)
            return n;
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    static long FibonacciIterative(int n)
    {
        long a = 0, b = 1;
        for (int i = 2; i <= n; i++)
        {
            long temp = a + b;
            a = b;
            b = temp;
        }
        return b;
    }

}
