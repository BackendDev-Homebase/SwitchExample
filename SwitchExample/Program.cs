using BenchmarkDotNet.Running;
using System;

namespace SwitchExample;

internal class Program
{
    static void Main(string[] args)
    {
        // RELEASE build is necessary!
        BenchmarkRunner.Run(typeof(SwitchExample).Assembly);
        Console.ReadKey();
    }
}