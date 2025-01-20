using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Runtime.InteropServices;

class Program
{
    static unsafe void AddArraysAvx(float[] a, float[] b, float[] result)
    {
        if (!Avx.IsSupported)
        {
            throw new PlatformNotSupportedException("AVX is not supported on this platform.");
        }

        int length = a.Length;

        if (length % 8 != 0)
        {
            throw new ArgumentException("Array length must be a multiple of 8 for AVX operations.");
        }

        fixed (float* ptrA = a, ptrB = b, ptrResult = result)
        {
            for (int i = 0; i < length; i += 8) // 8 floats per 256-bit vector
            {
                // Load 8 elements at a time into Vector256
                var vecA = Avx.LoadVector256(ptrA + i);
                var vecB = Avx.LoadVector256(ptrB + i);

                // Perform SIMD addition
                var vecResult = Avx.Add(vecA, vecB);

                // Store the result back into the array
                Avx.Store(ptrResult + i, vecResult);
            }
        }
    }

    static void Main()
    {
        float[] arrayA = new float[16];
        float[] arrayB = new float[16];
        float[] result = new float[16];

        for (int i = 0; i < 16; i++)
        {
            arrayA[i] = i;
            arrayB[i] = i * 2;
        }

        AddArraysAvx(arrayA, arrayB, result);

        Console.WriteLine("Result of SIMD addition:");
        Console.WriteLine(string.Join(", ", result));
    }
}
