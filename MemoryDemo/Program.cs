class Program
{
    static async Task Main()
    {
        byte[] buffer = new byte[1024];
        Memory<byte> memoryBuffer = buffer;

        using FileStream stream = new FileStream("example.txt", FileMode.OpenOrCreate);

        // Asynchronous read using Memory<T>
        int bytesRead = await stream.ReadAsync(memoryBuffer);
        Console.WriteLine($"Bytes Read: {bytesRead}");

        // Synchronous slice operation
        Memory<byte> slice = memoryBuffer.Slice(0, bytesRead);
        ProcessData(slice);
    }

    static void ProcessData(ReadOnlyMemory<byte> data)
    {
        Console.WriteLine($"Processing {data.Length} bytes...");
    }
}
