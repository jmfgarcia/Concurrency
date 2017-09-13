using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Concurrency.AsyncBasics
{
    /// <summary>
    /// You need to (asynchronously) wait for a period of time. 
    /// This can be useful when unit testing or implementing retry delays.
    /// This solution can also be useful for simple timeouts.
    /// </summary>
    public class PausingForAPeriodOfTime
    {
        public static async Task<T> DelayResult<T>(T result, TimeSpan delay)
        {
            await Task.Delay(delay);
            return result;
        }

        static async Task<string> DownloadStringWithRetries(string uri)
        {
            using (var client = new HttpClient())
            {
                // Retry after 1 second, then after 2 seconds, then 4.
                TimeSpan nextDelay = TimeSpan.FromSeconds(value: 1);
                for (int i = 0; i != 3; ++i)
                {
                    try
                    {
                        return await client.GetStringAsync(uri);
                    }
                    catch
                    {
                    }
                    await Task.Delay(nextDelay);
                    nextDelay = nextDelay + nextDelay;
                }
                // Try one last time, allowing the error to propogate.
                return await client.GetStringAsync(uri);
            }
        }

        /// <summary>
        /// This final example uses Task.Delay as a simple timeout; 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns>
        /// in this case, the desired semantics are to return null if the service does not respond within three seconds.
        /// </returns>
        static async Task<string> DownloadStringWithTimeout(string uri)
        {
            using (var client = new HttpClient())
            {
                Task<string> downloadTask = client.GetStringAsync(uri);
                Task timeoutTask = Task.Delay(millisecondsDelay: 3000);
                Task completedTask = await Task.WhenAny(downloadTask, timeoutTask);
                if (completedTask == timeoutTask)
                    return null;
                return await downloadTask;
            }
        }

    }
}
