using System;
using System.ComponentModel;
using System.Linq;

namespace Utility
{
    /// <summary>
    /// 枚举辅助类
    /// </summary>
    public class EnumHelper
    {
        private static T GetCustomAttribute<T>(Enum source) where T : Attribute
        {
            var sourceType = source.GetType();
            var name = Enum.GetName(sourceType, source);
            var filed = sourceType.GetField(name);
            var attributes = filed.GetCustomAttributes(typeof(T), false);

            return attributes.OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// 获取枚举值的描述
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetDesciption(Enum e)
        {
            var attr = GetCustomAttribute<DescriptionAttribute>(e);

            if (attr == null) return null;

            return attr.Description;
        }
    }
}