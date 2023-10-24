using System;
using Leonardo;

using var context = new FibonacciDataContext();

var results = new Fibonacci(context).RunAsync(args);
Console.WriteLine($"Finished");
results.Wait();