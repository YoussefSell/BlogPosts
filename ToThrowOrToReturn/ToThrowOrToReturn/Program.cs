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
        readonly int _itemsToProcess = 1000;

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

                }
            }
        }
    }
}
