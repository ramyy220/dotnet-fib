using System;
using Leonardo;

var results = Fibonacci.RunAsync(args);
Console.WriteLine($"Finished");
results.Wait();