using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Aquarius.Weixin.Entity
{
    /// <summary>
    /// 枚举<see cref="Enum"/>的扩展辅助操作方法
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举项上的<see cref="DescriptionAttribute"/>特性的文字描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            var type = value.GetType();
            var member = type.GetMember(value.ToString()).FirstOrDefault();
            var descAttr = member.GetCustomAttribute<DescriptionAttribute>();
            return (member != null && descAttr != null) ? descAttr.Description : value.ToString();
        }
    }
}
