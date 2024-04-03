using System.Threading.Tasks;
using UnityEngine.Scripting;

namespace GameFrameX.Config
{
    public interface IDataTable
    {
        /// <summary>
        /// 异步加载
        /// </summary>
        /// <returns></returns>
        Task LoadAsync();

        /// <summary>
        /// 获取数据表中对象的数量
        /// </summary>
        /// <returns></returns>
        int Count { get; }
    }

    /// <summary>
    /// 数据表基础接口
    /// </summary>
    [Preserve]
    public interface IDataTable<out T> : IDataTable
    {
        /// <summary>
        /// 根据ID获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(int id);

        /// <summary>
        /// 根据ID获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(string id);

        /// <summary>
        /// 根据ID获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T this[int id] { get; }

        /// <summary>
        /// 根据ID获取对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T this[string id] { get; }


        /// <summary>
        /// 获取数据表中第一个对象
        /// </summary>
        /// <returns></returns>
        T FirstOrDefault { get; }

        /// <summary>
        /// 获取数据表中最后一个对象
        /// </summary>
        /// <returns></returns>
        T LastOrDefault { get; }

        /// <summary>
        /// 获取数据表中所有对象
        /// </summary>
        /// <returns></returns>
        T[] All { get; }

        /// <summary>
        /// 获取数据表中所有对象
        /// </summary>
        /// <returns></returns>
        T[] ToArray();

        /// <summary>
        /// 根据条件查找
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        T Find(System.Func<T, bool> func);

        /// <summary>
        /// 根据条件查找
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        T[] FindList(System.Func<T, bool> func);
    }
}