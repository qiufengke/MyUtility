using System;

namespace Utils.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class TypeExtension
    {
        /// <summary>
        /// 获取Type默认值
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetDefault(this Type type)
        {
            if (type.IsValueType)
            {
                if (type == typeof(void)) return null;
                return Activator.CreateInstance(type);
            }
            return null;
        }
    }
}