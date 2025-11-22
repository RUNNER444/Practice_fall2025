using BenchmarkDotNet.Running;

namespace UnterDriverBenchmark;
class Program
{
    static void Main(string[] args)
    {
        BenchmarkRunner.Run<DriverMapBenchmark>();
    }
}
