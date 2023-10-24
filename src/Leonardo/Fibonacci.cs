using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

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
        await using var context = new FibonacciDataContext();
        
        IList<int> results = new List<int>();
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        var tasks = new List<Task<int>>();
        foreach (var arg in args)
        {
            var tfibonacci = await context.TFibonaccis
                .Where(t => t.FibInput == int.Parse(arg))
                .FirstOrDefaultAsync();

            if (tfibonacci == null)
            {



                var task = Task.Run(() =>
                {
                    var result = Fibonacci.Run(int.Parse(arg));
                    Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms {arg}");
                    return result;
                });
                tasks.Add(task);
            }
            else
            {
                tasks.Add(Task.FromResult((int)tfibonacci.FibOutput));
            }
        }

        foreach (var task in tasks)
        {
            
            var result = await task;
            context.TFibonaccis.Add(new TFibonacci()
            {
                FibOutput = result,
                FibInput = int.Parse(args[tasks.IndexOf(task)]),
                
                
            });
            Console.WriteLine($"Result: {task.Result}");
            results.Add(task.Result);
        }
        stopwatch.Stop();
        Console.WriteLine("Total elapsed time: {0} ms", stopwatch.ElapsedMilliseconds);
        
        await context.SaveChangesAsync();

        return results;
    }
}