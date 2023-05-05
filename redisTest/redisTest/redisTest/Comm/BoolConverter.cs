using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/****************************************************************
*   Copyright (c) 2018 江苏金恒,All rights reserved.
*   命名空间: LTN.CS.Base.Common  
*   模块名称: BoolConverter 
*   作用：
*   作者：016523-kolio
*	创建时间：2018/12/25 21:24:48
*   修改历史：
*****************************************************************/
namespace redisTest.Comm
{
    public class BoolConverter : JsonConverter
    {     /// <summary>
          /// 检查是否可以转换
          /// </summary>
          /// <param name="objectType"></param>
          /// <returns></returns>
        public override bool CanConvert(Type objectType)
        {
            bool rs = false;
            try
            {
                rs = (objectType == typeof(bool) || objectType == typeof(bool?) || objectType == typeof(Boolean?));
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
            if (token.Type == JTokenType.Null && objectType == typeof(bool?))
            {
                return null;
            }
            if (token.Type == JTokenType.Boolean)
            {
                var rs = token.ToObject<bool>();
                return rs;
            }
            if (token.Type == JTokenType.String)
            {
                // customize this to suit your needs
                return token.ToString();
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
            bool? d = default(bool);
            if (value != null)
            {
                d = value as bool?;
                if (d.HasValue) // If value was a decimal?, then this is possible
                {
                    d = d.Value; // The ToDouble-conversion removes all unnessecary precision
                }
            }
            if (d != null)
            {
                JToken.FromObject(d).WriteTo(writer);
            }
            else
            {
                JToken.FromObject(string.Empty).WriteTo(writer);
            }
        }
    }
}
