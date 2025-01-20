using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

class Program
{
    static unsafe void MultiplyVectorsSse(float[] values, float[] multipliers, float[] results)
    {
        if (!Sse.IsSupported)
        {
            throw new PlatformNotSupportedException("SSE is not supported on this processor.");
        }

        // Ensure arrays are of appropriate lengths
        if (values.Length < 4 || multipliers.Length < 4 || results.Length < 4)
        {
            throw new ArgumentException("Arrays must have at least 4 elements.");
        }

        fixed (float* pValues = values, pMultipliers = multipliers, pResults = results)
        {
            // Load data into Vector128
            Vector128<float> vecValues = Sse.LoadVector128(pValues);
            Vector128<float> vecMultipliers = Sse.LoadVector128(pMultipliers);

            // Perform SIMD multiplication
            Vector128<float> vecResult = Sse.Multiply(vecValues, vecMultipliers);

            // Store the result back
            Sse.Store(pResults, vecResult);
        }
    }

    static void Main()
    {
        float[] values = { 1f, 2f, 3f, 4f };
        float[] multipliers = { 2f, 4f, 6f, 8f };
        float[] results = new float[4];

        MultiplyVectorsSse(values, multipliers, results);

        Console.WriteLine("Results: " + string.Join(", ", results));
    }
}
