using System;
using System.Linq;
using Concurrency.AsyncBasics;
using Nito.AsyncEx;

namespace Concurrency
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                return AsyncContext.Run(() => PausingForAPeriodOfTime.DelayResult(result: 2, delay: TimeSpan.FromSeconds(value: 3)));
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return -1;
            }
        }
    }
}
