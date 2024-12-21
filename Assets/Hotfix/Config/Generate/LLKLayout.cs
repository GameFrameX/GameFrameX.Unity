
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

namespace Hotfix.Config
{
    public sealed partial class LLKLayout : LuBan.Runtime.BeanBase
    {
        public LLKLayout(int Col, int Row) 
        {
            this.Col = Col;
            this.Row = Row;
            PostInit();
        }

        public LLKLayout(JSONNode _buf)
        {
            { if(!_buf["col"].IsNumber) { throw new SerializationException(); }  Col = _buf["col"]; }
            { if(!_buf["row"].IsNumber) { throw new SerializationException(); }  Row = _buf["row"]; }

            // Localization Key Begin
            // Localization Key End
            PostInit();
        }

        public static LLKLayout DeserializeLLKLayout(JSONNode _buf)
        {
            return new LLKLayout(_buf);
        }

        /// <summary>
        /// 列
        /// </summary>
        public int Col { private set; get; }
        /// <summary>
        /// 行
        /// </summary>
        public int Row { private set; get; }
        public const int __ID__ = -505574955;
        public override int GetTypeId() => __ID__;

        public  void ResolveRef(TablesComponent tables)
        {
            
            
        }

        public void TranslateText(System.Func<string, string, string> translator)
        {
        }

        public override string ToString()
        {
            return "{ "
            + "col:" + Col + ","
            + "row:" + Row + ","
            + "}";
        }

        partial void PostInit();
    }
}