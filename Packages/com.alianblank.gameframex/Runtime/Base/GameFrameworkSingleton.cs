namespace Base
{
    /// <summary>
    /// 游戏框架单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GameFrameworkSingleton<T> where T : class, new()
    {
        private static T _instance;

        protected GameFrameworkSingleton()
        {
        }

        /// <summary>
        /// 单例对象
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }
    }
}