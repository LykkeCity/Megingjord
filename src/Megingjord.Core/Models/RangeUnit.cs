using System.Runtime.Serialization;

namespace Megingjord.Core.Models
{
    public enum RangeUnit
    {
        [EnumMember(Value = "block")]
        Block,
        [EnumMember(Value = "time")]
        Time
    }
}