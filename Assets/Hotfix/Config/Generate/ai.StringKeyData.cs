
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using LuBan.Runtime;
using SimpleJSON;


namespace cfg.ai
{
    
    public sealed partial class StringKeyData : ai.KeyData
    {
        public StringKeyData(JSONNode _buf)  : base(_buf) 
        {
            { if(!_buf["value"].IsString) { throw new SerializationException(); }  Value = _buf["value"]; }
        }
    
        public static StringKeyData DeserializeStringKeyData(JSONNode _buf)
        {
            return new ai.StringKeyData(_buf);
        }
    
        public readonly string Value;
       
        public const int __ID__ = -307888654;
        public override int GetTypeId() => __ID__;
    
        public override void ResolveRef(Tables tables)
        {
            base.ResolveRef(tables);
            
        }
    
        public override string ToString()
        {
            return "{ "
            + "value:" + Value + ","
            + "}";
        }
    }

}