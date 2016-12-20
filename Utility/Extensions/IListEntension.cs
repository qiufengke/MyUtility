using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utils.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ListEntension
    {
        /// <summary>
        /// 将 List&lt;T1&gt; 转化为 List&lt;T2&gt;
        /// </summary>
        /// <param name="list"></param>
        /// <param name="mapColumns"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2">target obj</typeparam>
        /// <returns></returns>
        public static List<T2> MapToList<T1, T2>(this List<T1> list, List<string> mapColumns = null)
            where T2 : class, new()
            where T1 : class, new()
        {
            return OrmUtil.MapToList<T1, T2>(list, mapColumns);
        }

        /// <summary>
        /// 将 T1 映射到 T2 实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="mapColumns"></param>
        /// <typeparam name="T2">target obj</typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public static T2 MapToModel<T1, T2>(this T1 entity, List<string> mapColumns = null)
            where T2 : class, new()
            where T1 : class, new()
        {
            return OrmUtil.MapToModel<T1, T2>(entity, mapColumns);
        }
    }
}