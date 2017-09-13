using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concurrency.Async_Basics
{
    /// <summary>
    /// You need to implement a synchronous method with an asynchronous signature.
    /// This situation can arise if you are inheriting from an asynchronous interface or base class
    /// but wish to implement it synchronously.This technique is particularly useful when unit
    /// testing asynchronous code, when you need a simple stub or mock for an asynchronous interface.
    /// </summary>
    interface IMyAsyncInterface
    {
        Task<int> GetValueAsync();
    }

    public class ReturningCompletedTasks : IMyAsyncInterface
    {
        //If you regularly use Task.FromResult with the same value, consider caching the actual task
        private static readonly Task<int> zeroTask = Task.FromResult(result: 0);

        public Task<int> GetValueAsync()
        {
            //Task.FromResult provides synchronous tasks only for successful results
            return Task.FromResult(result: 13);
            //return zeroTask;
        }

        /// <summary>
        /// If you need a task with a different kind of result(e.g., a task that is completed with a
        /// NotImplementedException), then you can create your own helper method using Task CompletionSource
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        static Task<T> NotImplementedAsync<T>()
        {
            //Conceptually, Task.FromResult is just a shorthand for TaskCompletionSource,
            var tcs = new TaskCompletionSource<T>();
            tcs.SetException(new NotImplementedException());
            return tcs.Task;
        }
    }
}
