using System.Diagnostics;
using ObjectMapper;

internal class Program
{
    private static void Main(string[] args)
    {
        List<int> lengths = new List<int>() { 1, 100, 1000, 1000000 };

        foreach (var length in lengths)
        {
            Console.WriteLine("for: " + length);
            test1(length);
            test2(length);
            Console.WriteLine("");
        }

        /*
            test1: Geçen süre: 15.4538 ms
            test2: Geçen süre: 0.0009 ms

            Uzunluk: 100
            test1: Geçen süre: 0.0509 ms
            test2: Geçen süre: 0.0046 ms

            Uzunluk: 1000
            test1: Geçen süre: 0.3847 ms
            test2: Geçen süre: 0.0267 ms

            Uzunluk: 1000000
            test1: Geçen süre: 349.178 ms
            test2: Geçen süre: 20.5197 ms
         */
    }

    static void test1(int length)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        Mapper<Source, Target> mapper = new Mapper<Source, Target>();

        for (int i = 0; i < length; i++)
        {
            Source source = new Source()
            {
                Id = 1,
                Name = "Onur",
                Age = 8
            };

            Target target = mapper.MapObjects(source);
        }

        stopwatch.Stop();
        TimeSpan elapsed = stopwatch.Elapsed;
        Console.WriteLine("Test1 geçen süre: " + elapsed.TotalMilliseconds + " ms");
    }

    static void test2(int length)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        for (int i = 0; i < length; i++)
        {
            Source source = new Source()
            {
                Id = 1,
                Name = "Onur",
                Age = 8
            };

            Target target = new Target();
            target.Age = source.Age;
        }

        stopwatch.Stop();
        TimeSpan elapsed = stopwatch.Elapsed;
        Console.WriteLine("Test2 geçen süre: " + elapsed.TotalMilliseconds + " ms");
    }
}