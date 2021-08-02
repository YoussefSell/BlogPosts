using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ResultNet;
using System;

namespace ToThrowOrToReturn
{
    class Program
    {
        static void Main() => BenchmarkRunner.Run<PerformanceBenchmark>();
    }

    public class PerformanceBenchmark
    {
        readonly int _itemsToProcess = 100000;

        [Benchmark]
        public void UsingExceptions()
        {
            for (int i = 0; i < _itemsToProcess; i++)
            {
                try
                {
                    EmailService.SendNotificationEmail_WithExceptions("", "");
                }
                catch (Exception)
                {

                }
            }
        }

        [Benchmark]
        public void UsingResultObject()
        {
            for (int i = 0; i < _itemsToProcess; i++)
            {
                var result = EmailService.SendNotificationEmail_WithResultObject("", "");
                if (result.IsSuccess())
                {

                }
            }
        }


    }
}
