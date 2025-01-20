int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Slice the array without creating a new one
Span<int> slice = numbers.AsSpan(2, 5);

Console.WriteLine("Sliced Span:");
foreach (var number in slice)
{
    Console.WriteLine(number); // Outputs: 3, 4, 5, 6, 7
}

// Modify the slice and observe changes in the original array
slice[0] = 99;
Console.WriteLine("Updated Original Array:");
Console.WriteLine(string.Join(", ", numbers)); // Outputs: 1, 2, 99, 4, 5, 6, 7, 8, 9, 10
