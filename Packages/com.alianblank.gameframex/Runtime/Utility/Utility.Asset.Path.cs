namespace GameFrameX
{
    public static partial class Utility
    {
        /// <summary>
        /// AB实用函数集，主要是路径拼接
        /// </summary>
        public static class Asset
        {
            /// <summary>
            /// 路径
            /// </summary>
            public static class Path
            {
                /// <summary>
                /// 打包资源根路径
                /// </summary>
                public const string BundlesPath = "Assets/Bundles";

                /// <summary>
                /// 打包资源文件夹名称
                /// </summary>
                public const string BundlesDirectoryName = "Bundles";

                /// <summary>
                /// 获取配置文件路径
                /// </summary>
                /// <param name="fileName"></param>
                /// <param name="extension"></param>
                /// <returns></returns>
                public static string GetConfigPath(string fileName, string extension = ".bytes")
                {
                    return $"{BundlesPath}/Config/{fileName}{extension}";
                }

                /// <summary>
                /// 获取AOT元数据代码文件路径
                /// </summary>
                /// <param name="fileName"></param>
                /// <param name="extension"></param>
                /// <returns></returns>
                public static string GetAOTCodePath(string fileName, string extension = ".bytes")
                {
                    return $"{BundlesPath}/AOTCode/{fileName}{extension}";
                }

                /// <summary>
                /// 获取代码文件路径
                /// </summary>
                /// <param name="fileName"></param>
                /// <param name="extension"></param>
                /// <returns></returns>
                public static string GetCodePath(string fileName, string extension = ".bytes")
                {
                    return $"{BundlesPath}/Code/{fileName}{extension}";
                }

                /// <summary>
                /// 获取UI文件路径
                /// </summary>
                /// <param name="uiPackageName"></param>
                /// <param name="extension"></param>
                /// <returns></returns>
                public static string GetUIPackagePath(string uiPackageName, string extension = ".bytes")
                {
                    return $"{BundlesPath}/UI/{uiPackageName}/{uiPackageName}";
                }
            }
        }
    }
}