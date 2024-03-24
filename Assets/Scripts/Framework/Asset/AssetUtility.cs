using GameFrameX;
using GameFrameX.Runtime;

namespace Framework.Asset
{
    /// <summary>
    /// AB实用函数集，主要是路径拼接
    /// </summary>
    public static class AssetUtility
    {
        /// <summary>
        /// 获取声音文件路径
        /// </summary>
        /// <param name="soundName">声音名称</param>
        /// <param name="extension">扩展名</param>
        /// <returns></returns>
        public static string GetSoundPath(string soundName, string extension = ".mp3")
        {
            return $"{Utility.Asset.Path.BundlesPath}/Sound/{soundName}{extension}";
        }
    }
}