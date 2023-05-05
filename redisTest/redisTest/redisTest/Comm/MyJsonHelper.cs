using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redisTest.Comm
{
    public static class MyJsonHelper
    {
        //static ILog logObj = LogManager.GetLogger("MyJsonHelper");
        static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            Formatting = Formatting.None,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            Converters = new List<JsonConverter> { new DecimalConverter(), new DateTimeConverter(), new BoolConverter(), new IntConverter() },
        };
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            string json = string.Empty;
            if (o != null)
            {
                try
                {
                    json = JsonConvert.SerializeObject(o, MyJsonHelper.settings);
                }
                catch (Exception)
                {
                    //logObj.Error(ex.Message);
                }
            }
            return json;
        }
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject<T>(object o)
        {
            string json = string.Empty;
            if (o != null)
            {
                json = JsonConvert.SerializeObject(o, typeof(T), MyJsonHelper.settings);
            }
            return json;
        }
        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json)
        {
            T t = default(T);
            try
            {
                if (!string.IsNullOrEmpty(json))
                {
                    t = JsonConvert.DeserializeObject<T>(json, MyJsonHelper.settings);
                }
            }
            catch (Exception)
            {
                //logObj.Error(ex.Message);
            }
            return t;
        }
        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json)
        {
            List<T> list = default(List<T>);
            if (!string.IsNullOrEmpty(json))
            {
                JsonSerializer serializer = JsonSerializer.CreateDefault(MyJsonHelper.settings);
                StringReader sr = new StringReader(json);
                object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
                list = o as List<T>;
            }
            return list;
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject, MyJsonHelper.settings);
            return t;
        }

    }
}
