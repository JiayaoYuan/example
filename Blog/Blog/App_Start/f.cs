using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// f 的摘要说明
/// </summary>
public static class f
{
    /// <summary>
    /// 把表格转成json字符串
    /// </summary>
    /// <param name="dt">数据表格</param>
    /// <returns>json字符串</returns>
    public static string DtToJson(this DataTable dt)
    {
        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

        javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
        ArrayList arrayList = new ArrayList();
        foreach (DataRow dataRow in dt.Rows)
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();  //实例化一个参数集合
            foreach (DataColumn dataColumn in dt.Columns)
            {
                dictionary.Add(dataColumn.ColumnName, dataRow[dataColumn.ColumnName].ToString());
            }
            arrayList.Add(dictionary); //ArrayList集合中添加键值
        }

        return "{root:" + javaScriptSerializer.Serialize(arrayList) + "}";  //返回一个json字符串
    }

    /// <summary>
    /// 把对象转成json字符串
    /// </summary>
    /// <param name="o">对象</param>
    /// <returns>json字符串</returns>
    public static string OjToJson(object o)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
        json.Serialize(o, sb);
        return sb.ToString();
    }

    /// <summary>
    /// 把对象转换为JSON字符串
    /// </summary>
    /// <param name="o">对象</param>
    /// <returns>JSON字符串</returns>
    public static string StToJSON(this object o)
    {
        if (o == null)
        {
            return null;
        }
        return JsonConvert.SerializeObject(o);
    }
    /// <summary>
    /// 把Json文本转为实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="input"></param>
    /// <returns></returns>
    public static T FromJSON<T>(this string input)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(input);
        }
        catch (Exception ex)
        {
            return default(T);
        }
    }
}