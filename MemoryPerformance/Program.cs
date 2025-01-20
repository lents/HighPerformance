using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace MemoryBenchmark
{
    public class MemoryChunkProcessing
    {
        private int[] numbers;

        [Params(100, 1000, 10000)] // Test different array sizes
        public int ArraySize;

        [Params(10, 50, 100)] // Test different chunk sizes
        public int ChunkSize;

        [GlobalSetup]
        public void Setup()
        {
            numbers = new int[ArraySize];
            for (int i = 0; i < numbers.Length; i++) numbers[i] = i + 1;
        }

        [Benchmark]
        public void ProcessWithMemory()
        {
            ProcessChunksWithMemory(numbers, ChunkSize);
        }

        [Benchmark]
        public void ProcessWithoutMemory()
        {
            ProcessChunksWithoutMemory(numbers, ChunkSize);
        }        

        static void ProcessChunksWithoutMemory(int[] numbers, int chunkSize)
        {
            int totalChunks = (numbers.Length + chunkSize - 1) / chunkSize;

            for (int i = 0; i < totalChunks; i++)
            {
                int start = i * chunkSize;
                int length = Math.Min(chunkSize, numbers.Length - start);

                // Create a new array for each chunk
                int[] chunk = new int[length];
                Array.Copy(numbers, start, chunk, 0, length);

                Console.WriteLine($"Processing Chunk {i + 1}: {string.Join(", ", chunk)}");
            }
        }

        public static void ProcessChunksWithMemory(int[] numbers, int chunkSize)
        {
            Memory<int> memory = numbers;
            int totalChunks = (memory.Length + chunkSize - 1) / chunkSize;

            for (int i = 0; i < totalChunks; i++)
            {
                int start = i * chunkSize;
                int length = Math.Min(chunkSize, memory.Length - start);

                Memory<int> chunk = memory.Slice(start, length);
                // Simulate processing
                _ = chunk.Span[0]; // Access to avoid optimization removal
            }
        }

        public static async Task ProcessChunksAsync(int[] numbers, int chunkSize)
        {
            Memory<int> memory = numbers;
            int totalChunks = (memory.Length + chunkSize - 1) / chunkSize;

            for (int i = 0; i < totalChunks; i++)
            {
                int start = i * chunkSize;
                int length = Math.Min(chunkSize, memory.Length - start);

                Memory<int> chunk = memory.Slice(start, length);
                // Simulate async work
                _ = chunk.Span[0]; // Access to avoid optimization removal
                await Task.Delay(1); // Minimal delay to mimic async operation
            }
        }
    }

    public class Program
    {
        public static async Task Main(string[] args)
        {
            BenchmarkRunner.Run<MemoryChunkProcessing>();

            // Optional: Run methods outside benchmark for testing
            int[] numbers = new int[100];
            for (int i = 0; i < numbers.Length; i++) numbers[i] = i + 1;

            Console.WriteLine("Running Synchronous Processing:");
            MemoryChunkProcessing.ProcessChunksWithMemory(numbers, 10);

            Console.WriteLine("\nRunning Asynchronous Processing:");
            await MemoryChunkProcessing.ProcessChunksAsync(numbers, 10);
        }
    }
}
