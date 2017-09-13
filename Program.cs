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
                //return AsyncContext.Run(() => PausingForAPeriodOfTime.DelayResult(result: 2, delay: TimeSpan.FromSeconds(value: 3)));
                var result = AsyncContext.Run(() => PausingForAPeriodOfTime.DownloadStringWithRetries(uri: new Uri(uriString: "http://www.marca.com").ToString()));
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return -1;
            }
        }
    }
}
