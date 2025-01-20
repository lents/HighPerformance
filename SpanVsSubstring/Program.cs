using BenchmarkDotNet.Running;

var summary = BenchmarkRunner.Run<SpanVsSubstring>();