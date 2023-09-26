using System.Diagnostics;

namespace Leonardo;

public class Fibonacci
{
    public static int Run(int i)
    {
        if (i <= 2)
            return 1;
        return Run(i - 1) + Run(i - 2);
    }

    public static async Task<IList<int>> RunAsync(string[] args)
    {
        IList<int> results = new List<int>();
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var tasks = new List<Task<int>>();
        foreach(var arg in args)
        {
            var task = Task.Run(() =>
            {
                var result = Fibonacci.Run(int.Parse(arg));
                Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms {arg}");
                return result;
            });
            tasks.Add(task);
        }
        foreach (var task in tasks)
        {
            var result = await task;
            Console.WriteLine($"Result: {task.Result}");
            results.Add(task.Result);
        }
        stopwatch.Stop();
        Console.WriteLine("Total elapsed time: {0} ms", stopwatch.ElapsedMilliseconds);

        return results;
    }
}