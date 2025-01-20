using System;
using System.IO;

class LogParser
{
    public static void ProcessLogFile(string filePath)
    {
        foreach (string line in File.ReadLines(filePath))
        {
            // Convert the line into a Span<char>
            ReadOnlySpan<char> logLine = line.AsSpan();

            // Parse the timestamp
            int timestampEnd = logLine.IndexOf(']');
            ReadOnlySpan<char> timestamp = logLine.Slice(1, timestampEnd - 1);

            // Parse the log level
            int logLevelStart = logLine.IndexOf('[') + 1;
            int logLevelEnd = logLine.IndexOf(']');
            ReadOnlySpan<char> logLevel = logLine.Slice(logLevelStart, logLevelEnd - logLevelStart);

            // Parse the message
            ReadOnlySpan<char> message = logLine.Slice(logLevelEnd + 2);

            // Print the parsed data
            Console.WriteLine($"Timestamp: {timestamp}, LogLevel: {logLevel}, Message: {message}");
        }
    }

    static void Main()
    {
        // Simulate a log file path
        string filePath = "example.log";

        // Process the log file
        ProcessLogFile(filePath);
    }
}
