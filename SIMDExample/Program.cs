using System;
using System.Numerics; // Import the SIMD library for Vector types

class SIMDExample
{
    public static void Main()
    {
        int size = 1000000;
        int[] array1 = new int[size];
        int[] array2 = new int[size];
        int[] result = new int[size];

        // Fill the arrays with sample data
        for (int i = 0; i < size; i++)
        {
            array1[i] = i;
            array2[i] = i * 2;
        }

        // Without SIMD
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        AddArraysWithoutSIMD(array1, array2, result);
        stopwatch.Stop();
        Console.WriteLine($"Without SIMD: {stopwatch.ElapsedMilliseconds}ms");

        // With SIMD
        stopwatch.Restart();
        AddArraysWithSIMD(array1, array2, result);
        stopwatch.Stop();
        Console.WriteLine($"With SIMD: {stopwatch.ElapsedMilliseconds}ms");
    }

    // Standard loop (non-SIMD)
    static void AddArraysWithoutSIMD(int[] array1, int[] array2, int[] result)
    {
        for (int i = 0; i < array1.Length; i++)
        {
            result[i] = array1[i] + array2[i];
        }
    }

    // SIMD-based addition using System.Numerics.Vectors
    static void AddArraysWithSIMD(int[] array1, int[] array2, int[] result)
    {
        int i = 0;
        Vector<int> v1, v2, vResult;
        // Process in chunks of Vector<int> (typically 4 or 8 depending on the architecture)
        for (; i <= array1.Length - Vector<int>.Count; i += Vector<int>.Count)
        {
            v1 = new Vector<int>(array1, i);
            v2 = new Vector<int>(array2, i);
            vResult = v1 + v2; // SIMD operation
            vResult.CopyTo(result, i);
        }

        // Process any remaining elements
        for (; i < array1.Length; i++)
        {
            result[i] = array1[i] + array2[i];
        }
    }
}
