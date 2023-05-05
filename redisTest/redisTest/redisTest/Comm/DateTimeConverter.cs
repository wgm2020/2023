using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

/****************************************************************
*   Copyright (c) 2018 江苏金恒,All rights reserved.
*   命名空间: LTN.CS.Base.Common
*   模块名称: DateTimeConverter 
*   作用：JSON转换-时间转换器
*   作者：016523-kolio
*	创建时间：2018/2/5 15:39:34
*   修改历史：
*****************************************************************/
namespace redisTest.Comm
{
    public class DateTimeConverter : JsonConverter
    {
        /// <summary>
        /// 检查是否可以转换
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            bool rs = false;
            try
            {
                rs = (objectType == typeof(DateTime) || objectType == typeof(DateTime?));
            }
            catch (Exception)
            {
                
            }
            return rs;
        }
        /// <summary>
        /// OBJ->JSON转换主逻辑
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Date)
            {
                // customize this to suit your needs
                return MyDateTimeHelper.ConvertToDateTimeDefaultNull(token.ToString());
            }
            if (objectType == typeof(DateTime))
            {
                // customize this to suit your needs
                return MyDateTimeHelper.ConvertToDateTimeDefaultNull(token.ToString());
            }
            if (objectType == typeof(DateTime?))
            {
                return MyDateTimeHelper.ConvertToDateTimeNDefaultNull(token.ToString());
            }
            throw new JsonSerializationException(string.Format("Unexpected token type: {0}", token.Type));
        }

        /// <summary>
        /// JSON->OBJ转换主逻辑
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string rsTemp = string.Empty;
            DateTime? d = default(DateTime?);
            if (value != null)
            {
                d = value as DateTime?;
                if (d.HasValue) // If value was a decimal?, then this is possible
                {
                    rsTemp = string.Format("{0} {1}", d.Value.ToLongDateString(), d.Value.ToLongTimeString()); // The ToDouble-conversion removes all unnessecary precision
                    if (string.IsNullOrEmpty(rsTemp))
                    {
                        return;
                    }
                }
            }
            JToken.FromObject(rsTemp).WriteTo(writer);
        }
    }
}
