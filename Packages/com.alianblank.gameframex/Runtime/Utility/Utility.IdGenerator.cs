using System;
using System.Threading;

namespace GameFrameX
{
    public static partial class Utility
    {
        public static class IdGenerator
        {
            private static readonly DateTime s_UtcTimeStart = new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // 共享计数器
            private static long _counter = (long)(DateTime.UtcNow - s_UtcTimeStart).TotalSeconds;

            /// <summary>
            /// 使用Interlocked.Increment生成唯一ID的方法
            /// </summary>
            /// <returns></returns>
            public static long GetNextUniqueId()
            {
                // 原子性地递增值
                return Interlocked.Increment(ref _counter);
            }
        }
    }
}