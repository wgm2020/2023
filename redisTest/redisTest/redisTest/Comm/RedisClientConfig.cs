using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace redisTest.Comm
{
    [XmlRoot(ElementName = "RedisClientConfig", Namespace = "http://www.w3.org/2001/XMLSchema-instance http://tempuri.org/RedisConfig.xsd", IsNullable = false)]
    [Serializable]
    public class RedisClientConfig
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RedisClientConfig()
        {

        }
        #region Redis配置
        /// <summary>
        ///     读写的地址
        /// </summary>
        [XmlElement(ElementName = "ReadWriteServer")]
        public string[] ReadWriteServers { get; set; }

        //本地测试
        /// <summary>
        ///     只读地址
        /// </summary>
        [XmlElement(ElementName = "ReadOnlyServer")]
        public string[] ReadOnlyServers { get; set; }

        //本地测试没做主从，所以读写同一个
        /// <summary>
        /// MaxWritePoolSize写的频率比读低
        /// </summary>
        [XmlElement()]
        public int MaxWritePoolSize { get; set; }

        /// <summary>
        /// MaxReadPoolSize读的频繁比较多
        /// Redis最大连接数虽然官方是说1W，但是连接数要控制。
        /// </summary>
        [XmlElement()]
        public int MaxReadPoolSize { get; set; }

        /// <summary>
        ///     连接最大的空闲时间 默认是240
        /// </summary>
        [XmlElement()]
        public int IdleTimeOutSecs { get; set; }

        /// <summary>
        ///     连接超时时间，毫秒
        /// </summary>
        [XmlElement()]
        public int ConnectTimeout { get; set; }

        /// <summary>
        ///     数据发送超时时间，毫秒
        /// </summary>
        [XmlElement()]
        public int SendTimeout { get; set; }

        /// <summary>
        ///     数据接收超时时间，毫秒
        /// </summary>
        public int ReceiveTimeout { get; set; }

        /// <summary>
        ///     连接池取链接的超时时间，毫秒
        /// </summary>
        [XmlElement()]
        public int PoolTimeout { get; set; }

        /// <summary>
        ///     默认的数据库，暂无用。内部默认值也是0
        /// </summary>
        [XmlElement()]
        public long DefaultDb { get; set; }
        #endregion

    }
}
