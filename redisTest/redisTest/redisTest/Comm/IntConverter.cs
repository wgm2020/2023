﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

/****************************************************************
*   Copyright (c) 2018 江苏金恒,All rights reserved.
*   命名空间: LTN.CS.Base.Common
*   模块名称: intConverter 
*   作用：JSON转换-数值转换器
*   作者：016523-kolio
*	创建时间：2018/2/5 15:39:34
*   修改历史：
*****************************************************************/
namespace redisTest.Comm
{
    public class IntConverter : JsonConverter
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
                rs = (objectType == typeof(int) || objectType == typeof(int?));
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
            if (token.Type == JTokenType.Float || token.Type == JTokenType.Integer)
            {
                var rs = token.ToObject<int>();
                return rs;
            }
            if (token.Type == JTokenType.String)
            {
                // customize this to suit your needs
                return int.Parse(token.ToString(),
                       System.Globalization.CultureInfo.GetCultureInfo("es-ES"));
            }
            if (token.Type == JTokenType.Null && objectType == typeof(int?))
            {
                return null;
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
            int? d = default(int?);
            if (value != null)
            {
                d = value as int?;
                if (d.HasValue) // If value was a int?, then this is possible
                {
                    d = new int?(d.Value); // The ToDouble-conversion removes all unnessecary precision
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
