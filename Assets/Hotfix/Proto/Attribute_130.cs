// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
//
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
//
//  本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//  This project is dual-licensed under the MIT License and Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
//
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes and liabilities arising from secondary development based on this project
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
//
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System.Collections.Generic;
using ProtoBuf;
using GameFrameX.Network.Runtime;

namespace Hotfix.Proto
{
    /// <summary>
    /// 玩家属性类型。与服务端属性系统（GameFrameX.Server#88）一一映射。
    /// 缓存以 int 为键，兼容未来扩展的、不在本枚举内的属性 id。
    /// </summary>
    public enum PlayerAttributeType
    {
        /// <summary>
        /// 未知/占位
        /// </summary>
        None = 0,

        /// <summary>
        /// 生命
        /// </summary>
        Hp = 1,

        /// <summary>
        /// 物理攻击
        /// </summary>
        PhysAtk = 2,

        /// <summary>
        /// 魔法攻击
        /// </summary>
        MagicAtk = 3,

        /// <summary>
        /// 物理防御
        /// </summary>
        PhysDef = 4,

        /// <summary>
        /// 魔法防御
        /// </summary>
        MagicDef = 5,

        /// <summary>
        /// 暴击
        /// </summary>
        Crit = 6,

        /// <summary>
        /// 暴击伤害
        /// </summary>
        CritDamage = 7,

        /// <summary>
        /// 精准
        /// </summary>
        Accuracy = 8,

        /// <summary>
        /// 格挡
        /// </summary>
        Block = 9,
    }

    /// <summary>
    /// 请求玩家属性快照
    /// </summary>
    [ProtoContract]
    [MessageTypeHandler(((130) << 16) + 10)]
    public sealed class ReqPlayerAttribute : MessageObject, IRequestMessage
    {
        public override void Clear()
        {
        }
    }

    /// <summary>
    /// 返回玩家属性快照。AttributeDic 为全量属性（key:属性id，value:属性值）。
    /// 客户端收到后必须清空旧缓存再填充，避免旧属性残留。
    /// </summary>
    [ProtoContract]
    [MessageTypeHandler(((130) << 16) + 11)]
    public sealed class RespPlayerAttribute : MessageObject, IResponseMessage
    {
        /// <summary>
        /// 玩家属性集合。key:属性id，value:属性值（long）
        /// </summary>
        [ProtoMember(1)]
        [ProtoMap(DisableMap = true)]
        public Dictionary<int, long> AttributeDic { get; set; } = new Dictionary<int, long>();

        /// <summary>
        /// 返回的错误码
        /// </summary>
        [ProtoMember(2047)]
        public int ErrorCode { get; set; }

        public override void Clear()
        {
            AttributeDic.Clear();
            ErrorCode = default;
        }
    }

    /// <summary>
    /// 通知玩家属性变化（增量）。AttributeDic 仅携带本次发生变化的属性 id 到其最终值；
    /// 未携带的属性保持不变。客户端按 key 写入并做去重。
    /// </summary>
    [ProtoContract]
    [MessageTypeHandler(((130) << 16) + 12)]
    public sealed class NotifyPlayerAttributeChanged : MessageObject, INotifyMessage
    {
        /// <summary>
        /// 变化的属性，key:属性id，value:最终值（long）
        /// </summary>
        [ProtoMember(1)]
        [ProtoMap(DisableMap = true)]
        public Dictionary<int, long> AttributeDic { get; set; } = new Dictionary<int, long>();

        public override void Clear()
        {
            AttributeDic.Clear();
        }
    }
}
