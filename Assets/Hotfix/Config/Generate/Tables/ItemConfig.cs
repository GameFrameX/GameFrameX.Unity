
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LuBan.Runtime;
using GameFrameX.Config;
using SimpleJSON;

namespace Hotfix.Config.Tables
{
    public sealed partial class ItemConfig : LuBan.Runtime.BeanBase
    {
        public ItemConfig(int Id, string Name, ItemType Type, ItemSubType SubType, ItemEPrompt EPrompt, ItemCanUse CanUse, bool IsDecompose, int MaxNum, string Description, System.Collections.Generic.List<int> ComeLink, string Icon, string BgIcon, ItemLevelColor LevelColor, bool CanAnnounce, string LinkInfo, int FunctionID, ItemUseLimiteType UseLimiteType, bool Abandon, bool DoubleCheckDesc, bool CanTrade, int TradeCD, int TradeItemsLimit, int UseLinmit, long? ExpireTime, bool IsRecordLog) 
        {
            this.Id = Id;
            this.Name = Name;
            this.Type = Type;
            this.SubType = SubType;
            this.EPrompt = EPrompt;
            this.CanUse = CanUse;
            this.IsDecompose = IsDecompose;
            this.MaxNum = MaxNum;
            this.Description = Description;
            this.ComeLink = ComeLink;
            this.Icon = Icon;
            this.BgIcon = BgIcon;
            this.LevelColor = LevelColor;
            this.CanAnnounce = CanAnnounce;
            this.LinkInfo = LinkInfo;
            this.FunctionID = FunctionID;
            this.UseLimiteType = UseLimiteType;
            this.Abandon = Abandon;
            this.DoubleCheckDesc = DoubleCheckDesc;
            this.CanTrade = CanTrade;
            this.TradeCD = TradeCD;
            this.TradeItemsLimit = TradeItemsLimit;
            this.UseLinmit = UseLinmit;
            this.ExpireTime = ExpireTime;
            this.IsRecordLog = IsRecordLog;
            PostInit();
        }

        public ItemConfig(JSONNode _buf)
        {
            { if(!_buf["id"].IsNumber) { throw new SerializationException(); }  Id = _buf["id"]; }
            { if(!_buf["Name"].IsString) { throw new SerializationException(); }  Name = _buf["Name"]; }
            { if(!_buf["Type"].IsNumber) { throw new SerializationException(); }  Type = (ItemType)_buf["Type"].AsInt; }
            { if(!_buf["SubType"].IsNumber) { throw new SerializationException(); }  SubType = (ItemSubType)_buf["SubType"].AsInt; }
            { if(!_buf["EPrompt"].IsNumber) { throw new SerializationException(); }  EPrompt = (ItemEPrompt)_buf["EPrompt"].AsInt; }
            { if(!_buf["CanUse"].IsNumber) { throw new SerializationException(); }  CanUse = (ItemCanUse)_buf["CanUse"].AsInt; }
            { if(!_buf["IsDecompose"].IsBoolean) { throw new SerializationException(); }  IsDecompose = _buf["IsDecompose"]; }
            { if(!_buf["MaxNum"].IsNumber) { throw new SerializationException(); }  MaxNum = _buf["MaxNum"]; }
            { if(!_buf["description"].IsString) { throw new SerializationException(); }  Description = _buf["description"]; }
            { var __json0 = _buf["ComeLink"]; if(!__json0.IsArray) { throw new SerializationException(); } ComeLink = new System.Collections.Generic.List<int>(__json0.Count); foreach(JSONNode __e0 in __json0.Children) { int __v0;  { if(!__e0.IsNumber) { throw new SerializationException(); }  __v0 = __e0; }  ComeLink.Add(__v0); }   }
            { if(!_buf["Icon"].IsString) { throw new SerializationException(); }  Icon = _buf["Icon"]; }
            { if(!_buf["BgIcon"].IsString) { throw new SerializationException(); }  BgIcon = _buf["BgIcon"]; }
            { if(!_buf["LevelColor"].IsNumber) { throw new SerializationException(); }  LevelColor = (ItemLevelColor)_buf["LevelColor"].AsInt; }
            { if(!_buf["CanAnnounce"].IsBoolean) { throw new SerializationException(); }  CanAnnounce = _buf["CanAnnounce"]; }
            { if(!_buf["LinkInfo"].IsString) { throw new SerializationException(); }  LinkInfo = _buf["LinkInfo"]; }
            { if(!_buf["FunctionID"].IsNumber) { throw new SerializationException(); }  FunctionID = _buf["FunctionID"]; }
            { if(!_buf["UseLimiteType"].IsNumber) { throw new SerializationException(); }  UseLimiteType = (ItemUseLimiteType)_buf["UseLimiteType"].AsInt; }
            { if(!_buf["Abandon"].IsBoolean) { throw new SerializationException(); }  Abandon = _buf["Abandon"]; }
            { if(!_buf["doubleCheckDesc"].IsBoolean) { throw new SerializationException(); }  DoubleCheckDesc = _buf["doubleCheckDesc"]; }
            { if(!_buf["CanTrade"].IsBoolean) { throw new SerializationException(); }  CanTrade = _buf["CanTrade"]; }
            { if(!_buf["TradeCD"].IsNumber) { throw new SerializationException(); }  TradeCD = _buf["TradeCD"]; }
            { if(!_buf["TradeItemsLimit"].IsNumber) { throw new SerializationException(); }  TradeItemsLimit = _buf["TradeItemsLimit"]; }
            { if(!_buf["UseLinmit"].IsNumber) { throw new SerializationException(); }  UseLinmit = _buf["UseLinmit"]; }
            { var _j = _buf["ExpireTime"]; if (_j.Tag != JSONNodeType.None && _j.Tag != JSONNodeType.NullValue) { { if(!_j.IsNumber) { throw new SerializationException(); }  ExpireTime = _j; } } else { ExpireTime = null; } }
            { if(!_buf["IsRecordLog"].IsBoolean) { throw new SerializationException(); }  IsRecordLog = _buf["IsRecordLog"]; }

            // Localization Key Begin
            Name_Localization_Key = Name;
            Description_Localization_Key = Description;
            // Localization Key End
            PostInit();
        }

        public static ItemConfig DeserializeItemConfig(JSONNode _buf)
        {
            return new Tables.ItemConfig(_buf);
        }

        /// <summary>
        /// ID
        /// </summary>
        public int Id { private set; get; }
        public string Name { private set; get; }
        private readonly string Name_Localization_Key;
        /// <summary>
        /// 道具类型
        /// </summary>
        public ItemType Type { private set; get; }
        /// <summary>
        /// 子类别
        /// </summary>
        public ItemSubType SubType { private set; get; }
        /// <summary>
        /// 是否提示使用
        /// </summary>
        public ItemEPrompt EPrompt { private set; get; }
        /// <summary>
        /// 能否使用
        /// </summary>
        public ItemCanUse CanUse { private set; get; }
        /// <summary>
        /// 是否可分解
        /// </summary>
        public bool IsDecompose { private set; get; }
        /// <summary>
        /// 叠加数
        /// </summary>
        public int MaxNum { private set; get; }
        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description { private set; get; }
        /// <summary>
        /// 描述信息 的多语言Key
        /// </summary>
        private readonly string Description_Localization_Key;
        /// <summary>
        /// 来源链接
        /// </summary>
        public System.Collections.Generic.List<int> ComeLink { private set; get; }
        /// <summary>
        /// 图标链接
        /// </summary>
        public string Icon { private set; get; }
        /// <summary>
        /// 背景图链接
        /// </summary>
        public string BgIcon { private set; get; }
        /// <summary>
        /// 颜色品级
        /// </summary>
        public ItemLevelColor LevelColor { private set; get; }
        /// <summary>
        /// 是否公告
        /// </summary>
        public bool CanAnnounce { private set; get; }
        /// <summary>
        /// 点击使用后<br/>跳转位置
        /// </summary>
        public string LinkInfo { private set; get; }
        /// <summary>
        /// 关联的功能序号
        /// </summary>
        public int FunctionID { private set; get; }
        /// <summary>
        /// 使用限制类别
        /// </summary>
        public ItemUseLimiteType UseLimiteType { private set; get; }
        /// <summary>
        /// 是否废弃
        /// </summary>
        public bool Abandon { private set; get; }
        /// <summary>
        /// 使用时需要弹窗二次确认的描述
        /// </summary>
        public bool DoubleCheckDesc { private set; get; }
        /// <summary>
        /// 能否交换
        /// </summary>
        public bool CanTrade { private set; get; }
        /// <summary>
        /// 交换冷却CD<br/>
        /// </summary>
        public int TradeCD { private set; get; }
        /// <summary>
        /// 赠送数量限制
        /// </summary>
        public int TradeItemsLimit { private set; get; }
        /// <summary>
        /// 可使用总次数
        /// </summary>
        public int UseLinmit { private set; get; }
        /// <summary>
        /// 到期时间
        /// </summary>
        public long? ExpireTime { private set; get; }
        /// <summary>
        /// 是否记录日志
        /// </summary>
        public bool IsRecordLog { private set; get; }
        public const int __ID__ = -1046574242;
        public override int GetTypeId() => __ID__;

        public  void ResolveRef(TablesComponent tables)
        {
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
            
        }

        public void TranslateText(System.Func<string, string, string> translator)
        {
            Name = translator(Name_Localization_Key, Name);
            Description = translator(Description_Localization_Key, Description);
        }

        public override string ToString()
        {
            return "{ "
            + "id:" + Id + ","
            + "Name:" + Name + ","
            + "Type:" + Type + ","
            + "SubType:" + SubType + ","
            + "EPrompt:" + EPrompt + ","
            + "CanUse:" + CanUse + ","
            + "IsDecompose:" + IsDecompose + ","
            + "MaxNum:" + MaxNum + ","
            + "description:" + Description + ","
            + "ComeLink:" + StringUtil.CollectionToString(ComeLink) + ","
            + "Icon:" + Icon + ","
            + "BgIcon:" + BgIcon + ","
            + "LevelColor:" + LevelColor + ","
            + "CanAnnounce:" + CanAnnounce + ","
            + "LinkInfo:" + LinkInfo + ","
            + "FunctionID:" + FunctionID + ","
            + "UseLimiteType:" + UseLimiteType + ","
            + "Abandon:" + Abandon + ","
            + "doubleCheckDesc:" + DoubleCheckDesc + ","
            + "CanTrade:" + CanTrade + ","
            + "TradeCD:" + TradeCD + ","
            + "TradeItemsLimit:" + TradeItemsLimit + ","
            + "UseLinmit:" + UseLinmit + ","
            + "ExpireTime:" + ExpireTime + ","
            + "IsRecordLog:" + IsRecordLog + ","
            + "}";
        }

        partial void PostInit();
    }
}
