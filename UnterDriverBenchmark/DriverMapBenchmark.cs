using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using UnterDriver.data_classes;
using UnterDriver.management;

namespace UnterDriverBenchmark
{
    [Config(typeof(Config))]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class DriverMapBenchmark
    {
        private DriverMap driverMap = null!;
        private Random random = null!;

        [Params(100, 1000, 10000)]
        public int NumberOfDrivers { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            random = new Random(42);
            int mapWidth = 1000;
            int mapHeight = 1000;
            
            Coordinate order = new Coordinate(
                random.Next(0, mapWidth), 
                random.Next(0, mapHeight)
            );
            
            driverMap = new DriverMap(mapWidth, mapHeight, order);
            
            int driversAdded = 0;
            while (driversAdded < NumberOfDrivers)
            {
                Coordinate randomLocation = new Coordinate(
                    random.Next(0, mapWidth), 
                    random.Next(0, mapHeight)
                );
                
                if (!randomLocation.CoordinateEquals(order) && 
                    !driverMap.CheckDriverOnLocation(randomLocation))
                {
                    driverMap.AddDriver(new Driver(driversAdded, randomLocation, order));
                    driversAdded++;
                }
            }
        }

        [Benchmark(Baseline = true)]
        public List<Driver> BruteForce_Algorithm()
        {
            return driverMap.BruteForce();
        }

        [Benchmark]
        public List<Driver> OneWay_Algorithm()
        {
            return driverMap.OneWay();
        }

        [Benchmark]
        public List<Driver> Radar_Algorithm()
        {
            return driverMap.Radar();
        }
    }

    public class Config:ManualConfig
    {
        public Config()
        {
            AddExporter(HtmlExporter.Default);
            AddExporter(CsvExporter.Default);
            
            AddColumn(StatisticColumn.Median);
        }
    }
}