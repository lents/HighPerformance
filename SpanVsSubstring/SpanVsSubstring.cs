using BenchmarkDotNet.Attributes;
using System.Text;

public class SpanVsSubstring
{
    private string SampleString = new string('A', 1_000_000) + new string('B', 1_000_000);
    private const int PARTS = 100;
    [Benchmark]
    public string UsingSubstring()
    {
        var result = new StringBuilder();
        int partSize = SampleString.Length / PARTS;

        for (int i = 0; i < PARTS; i++)
        {
            string part = SampleString.Substring(i * partSize, partSize);
            result.Append(part); // Append instead of using `+=`
        }

        return result.ToString();
    }

    [Benchmark]
    public string UsingSpan()
    {
        ReadOnlySpan<char> span = SampleString.AsSpan();
        var result = new StringBuilder();
        int partSize = span.Length / PARTS;

        for (int i = 0; i < PARTS; i++)
        {
            ReadOnlySpan<char> part = span.Slice(i * partSize, partSize);
            result.Append(part); // Directly append Span<char>
        }

        return result.ToString();
    }
}

