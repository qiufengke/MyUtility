using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

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
        /// <param name="onlyFields">需要转换的特定字段</param>
        /// <returns></returns>
        public static T MapToModel<T>(IDataReader dataReader, List<string> onlyFields = null)
        {
            var model = Activator.CreateInstance<T>();

            var t = typeof(T);

            var properties = typeof(T).GetProperties();

            for (var i = 0; i < dataReader.FieldCount; i++)
            {
                var columnName = dataReader.GetName(i);

                var property =
                    onlyFields == null
                        ? properties.FirstOrDefault(o => o.Name == columnName)
                        : properties.FirstOrDefault(o => o.Name == columnName && onlyFields.Contains(columnName));

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
        /// <param name="onlyFields"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> MapToList<T>(IDataReader dataReader, List<string> onlyFields = null)
        {
            var list = new List<T>();
            while (dataReader.Read())
            {
                list.Add(MapToModel<T>(dataReader, onlyFields));
            }
            return list;
        }
    }
}