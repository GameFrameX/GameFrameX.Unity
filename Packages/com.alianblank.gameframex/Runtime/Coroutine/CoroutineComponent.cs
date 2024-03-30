using System.Collections;
using System.Collections.Concurrent;
using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 协程组件
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Coroutine")]
    public class CoroutineComponent : GameFrameworkComponent
    {
        /// <summary>
        /// 执行过的迭代器
        /// </summary>
        private readonly ConcurrentDictionary<IEnumerator, Coroutine> m_CoroutineMap = new ConcurrentDictionary<IEnumerator, Coroutine>();

        /// <summary>
        /// 开启一个协程
        /// </summary>
        /// <param name="enumerator"></param>
        /// <returns></returns>
        public new Coroutine StartCoroutine(IEnumerator enumerator)
        {
            var ret = base.StartCoroutine(enumerator);
            m_CoroutineMap[enumerator] = ret;
            return ret;
        }

        /// <summary>
        /// 终止一个协程
        /// </summary>
        /// <param name="enumerator"></param>
        public new void StopCoroutine(IEnumerator enumerator)
        {
            if (m_CoroutineMap.TryGetValue(enumerator, out var coroutine))
            {
                base.StopCoroutine(coroutine);
                m_CoroutineMap.TryRemove(enumerator, out _);
            }

            base.StopCoroutine(enumerator);
        }

        /// <summary>
        /// 终止一个协程
        /// </summary>
        /// <param name="coroutine"></param>
        public new void StopCoroutine(Coroutine coroutine)
        {
            base.StopCoroutine(coroutine);
            foreach (var item in m_CoroutineMap)
            {
                if (item.Value == coroutine)
                {
                    m_CoroutineMap.TryRemove(item.Key, out _);
                    break;
                }
            }
        }

        /// <summary>
        /// 终止全部协程
        /// </summary>
        public new void StopAllCoroutines()
        {
            foreach (var coroutine in m_CoroutineMap.Values)
            {
                base.StopCoroutine(coroutine);
            }

            m_CoroutineMap.Clear();
            base.StopAllCoroutines();
        }

        private readonly WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

        /// <summary>
        /// 等待当前帧结束
        /// </summary>
        /// <returns></returns>
        private IEnumerator _WaitForEndOfFrameFinish(System.Action callback)
        {
            yield return _waitForEndOfFrame;
            callback?.Invoke();
        }

        /// <summary>
        /// 等待当前帧结束
        /// </summary>
        /// <param name="callback"></param>
        public void WaitForEndOfFrameFinish(System.Action callback)
        {
            StartCoroutine(_WaitForEndOfFrameFinish(callback));
        }
    }
}