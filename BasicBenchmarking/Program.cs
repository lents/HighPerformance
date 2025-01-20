using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;

public class StringConcatenationBenchmark
{
    [Benchmark]
    public string ConcatUsingStringConcat()
    {
        return String.Concat("Hello", " ", "World");
    }

    [Benchmark]
    public string ConcatUsingInterpolation()
    {
        return $"Hello World";
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var summary = BenchmarkRunner.Run<StringConcatenationBenchmark>();
    }
}
