using System;
using System.Threading;

namespace Aquarius.Weixin.Concurrency
{
    /// <summary>
    /// 异步锁（通过信号量）
    /// </summary>
    public class AsyncLock : IDisposable
    {
        private static readonly Semaphore semaphore = new Semaphore(1, 1);

        /// <summary>
        /// 加锁
        /// </summary>
        public AsyncLock()
        {
            semaphore.WaitOne();
        }

        /// <summary>
        /// 释放锁
        /// </summary>
        public void Dispose()
        {
            semaphore.Release();
        }
    }
}
