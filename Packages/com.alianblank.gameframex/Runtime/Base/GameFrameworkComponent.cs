//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using UnityEngine;

namespace GameFrameX.Runtime
{
    /// <summary>
    /// 游戏框架组件抽象类。
    /// </summary>
    public abstract class GameFrameworkComponent : MonoBehaviour
    {
        /// <summary>
        /// 实现类的类型
        /// </summary>
        protected Type ComponentType = null;

        /// <summary>
        /// 接口类的类型
        /// </summary>
        protected Type InterfaceComponentType = null;

        /// <summary>
        /// 游戏框架组件类型。
        /// </summary>
        [SerializeField] protected string m_ComponentType = string.Empty;

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        protected virtual void Awake()
        {
            GameEntry.RegisterComponent(this);
            GameFrameworkGuard.NotNull(ComponentType, nameof(ComponentType));
            GameFrameworkGuard.NotNull(InterfaceComponentType, nameof(InterfaceComponentType));
            GameFrameworkEntry.RegisterModule(InterfaceComponentType, ComponentType);
        }
    }
}