string data = "12345-67890";
        
// Convert to ReadOnlySpan<char> for efficient slicing
ReadOnlySpan<char> span = data.AsSpan();
        
ReadOnlySpan<char> part1 = span.Slice(0, 5); // "12345"
ReadOnlySpan<char> part2 = span.Slice(6, 5); // "67890"
        
Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

