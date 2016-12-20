using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Utils
{
    /// <summary>
    /// ORM 映射类
    /// </summary>
    public class OrmUtil
    {
        /// <summary>
        /// 将 DataReader 转化为 对应实体对象 ,只对属性赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader"></param>
        /// <param name="mapColumns">需要转换的特定属性</param>
        /// <returns></returns>
        public static T MapToModel<T>(IDataReader dataReader, List<string> mapColumns = null)
        {
            var model = Activator.CreateInstance<T>();

            var properties = typeof(T).GetProperties();

            for (var i = 0; i < dataReader.FieldCount; i++)
            {
                var columnName = dataReader.GetName(i);

                var property =
                    mapColumns == null
                        ? properties.FirstOrDefault(o => o.Name == columnName)
                        : properties.FirstOrDefault(o => o.Name == columnName && mapColumns.Contains(columnName));

                if (property != null)
                {
                    var isGenericType = property.PropertyType.IsGenericType;
                    if (isGenericType && dataReader[i] == DBNull.Value)
                    {
                        property.SetValue(model, null);
                    }
                    else
                    {
                        #region switch

                        //判断是否是 可空类型 eg: int? DateTime?
                        var type = property.PropertyType.IsGenericType
                            ? property.PropertyType.GetGenericArguments()[0]
                            : property.PropertyType;

                        switch (type.ToString())
                        {
                            case "System.String":
                                var s = "";
                                s = dataReader[i] == DBNull.Value ? "" : dataReader[i].ToString();
                                property.SetValue(model, s);
                                break;
                            case "System.Int16":
                            case "System.Int64":
                            case "System.Int32":
                                var result = 0;
                                int.TryParse(dataReader[i] == DBNull.Value ? "0" : dataReader[i].ToString(), out result);
                                property.SetValue(model, result);
                                break;
                            case "System.DateTime":
                                DateTime d;
                                DateTime.TryParse(
                                    dataReader[i] == DBNull.Value
                                        ? DateTime.MinValue.ToString()
                                        : dataReader[i].ToString(), out d);
                                property.SetValue(model, d);
                                break;
                            case "System.Boolean":
                                var b = false;
                                var valType = dataReader.GetFieldType(i);
                                //兼容性处理，大于1-> true 小于1 ->false
                                if (valType.ToString() == "System.Int32")
                                {
                                    int v;
                                    int.TryParse(dataReader[i] == DBNull.Value ? "0" : dataReader[i].ToString(), out v);
                                    b = v > 0;
                                }
                                else
                                {
                                    bool.TryParse(dataReader[i] == DBNull.Value ? "false" : dataReader[i].ToString(),
                                        out b);
                                }
                                property.SetValue(model, b);
                                break;
                            case "Decimal": //浮点型
                            case "Double":
                                double dV = 0;
                                double.TryParse(dataReader[i] == DBNull.Value ? "0" : dataReader[i].ToString(), out dV);
                                property.SetValue(model, dV);
                                break;
                            case "System.Nullable`1":
                                break;
                        }

                        #endregion
                    }
                }
            }

            return model;
        }

        /// <summary>
        /// 将 DataReader 转化为 实体对象集合
        /// </summary>
        /// <param name="dataReader"></param>
        /// <param name="mapColumns"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> MapToList<T>(IDataReader dataReader, List<string> mapColumns = null)
        {
            var list = new List<T>();
            while (dataReader.Read())
            {
                list.Add(MapToModel<T>(dataReader, mapColumns));
            }
            return list;
        }

        /// <summary>
        /// 将 T1 映射到 T2 实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="mapColumns">映射的字段或属性</param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public static T2 MapToModel<T1, T2>(T1 entity, List<string> mapColumns = null)
        {
            var properties = typeof(T1).GetProperties();
            var fields = typeof(T1).GetFields();

            var dic = properties.ToDictionary<PropertyInfo, string, object>(p => p.Name, p => p);
            foreach (var f in fields)
            {
                dic.Add(f.Name, f);
            }


            var skipMapDefined = mapColumns == null || !mapColumns.Any();

            var model = Activator.CreateInstance<T2>();
            var targetFields = typeof(T2).GetFields();
            foreach (var item in targetFields)
            {
                if (skipMapDefined || mapColumns.Contains(item.Name))
                {
                    var v = GetValue(entity, dic, item.Name, item.FieldType);
                    if (v == null) continue;
                    item.SetValue(model, v);
                }
            }

            var targetProperties = typeof(T2).GetProperties();
            foreach (var item in targetProperties)
            {
                if (skipMapDefined || mapColumns.Contains(item.Name))
                {
                    var v = GetValue(entity, dic, item.Name, item.PropertyType);
                    if (v == null) continue;
                    item.SetValue(model, v);
                }
            }

            return model;
        }

        private static object GetValue<T1>(T1 entity, Dictionary<string, object> dic, string name, Type type)
        {
            var d = dic.FirstOrDefault(x => x.Key == name);
            // dic 没有 key
            if (d.Key == null) return null;
            var info = d.Value;
            var fieldInfo = info as FieldInfo;
            var v = fieldInfo != null ? fieldInfo.GetValue(entity) : ((PropertyInfo)info).GetValue(entity);
            return v;
        }

        /// <summary>
        /// 讲 List&lt;T1&gt; 转化为 List&lt;T2&gt;
        /// </summary>
        /// <param name="list"></param>
        /// <param name="mapColumns">映射的字段或属性</param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        public static List<T2> MapToList<T1, T2>(List<T1> list, List<string> mapColumns = null)
        {
            return list.Select(o => MapToModel<T1, T2>(o, mapColumns)).ToList();
        }
    }
}