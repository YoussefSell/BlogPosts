using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using ResultNet;
using System;

namespace ToThrowOrToReturn
{
    static class Program
    {
        static void Main() => BenchmarkRunner.Run<PerformanceBenchmark>();
    }

    public class PerformanceBenchmark
    {
        readonly int _itemsToProcess = 10000;

        [Benchmark]
        public void UsingExceptions_WithoutErrors()
        {
            for (int i = 0; i < _itemsToProcess; i++)
            {
                try
                {
                    EmailService.SendNotificationEmail_WithExceptions(
                        userEmail: "example@email.com",
                        templateId: "template_id");
                }
                catch (Exception)
                {
                    // custom code
                }
            }
        }

        [Benchmark]
        public void UsingResultObject_WithoutErrors()
        {
            for (int i = 0; i < _itemsToProcess; i++)
            {
                var result = EmailService.SendNotificationEmail_WithResultObject(
                        userEmail: "example@email.com",
                        templateId: "template_id");

                if (result.IsSuccess())
                {
                    // custom code
                }
            }
        }

        [Benchmark]
        public void UsingExceptions_WithErrors()
        {
            for (int i = 0; i < _itemsToProcess; i++)
            {
                try
                {
                    EmailService.SendNotificationEmail_WithExceptions(
                        userEmail: "",
                        templateId: "");
                }
                catch (Exception)
                {
                    // custom code
                }
            }
        }

        [Benchmark]
        public void UsingResultObject_WithErrors()
        {
            for (int i = 0; i < _itemsToProcess; i++)
            {
                var result = EmailService.SendNotificationEmail_WithResultObject("", "");
                if (result.IsSuccess())
                {
                    // custom code
                }
            }
        }
    }
}
