using redisTest.Comm;
using ServiceStack.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace redisTest.Comm
{
    public interface IRedisHandle : IDisposable
    {
        /// <summary>
        /// 重启通道
        /// </summary>
        void SubscribeRestart();

        /// <summary>
        /// 获取其他数据库值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="DBStr"></param>
        /// <returns></returns>
        string GetValue(string key, long DBStr);

        /// <summary>
        /// 检查连接
        /// </summary>
        /// <returns></returns>
        bool Ping();

        /// <summary>
        /// 取消全部订阅
        /// </summary>
        void UnsubscribeAll();
        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="channel"></param>
        void Unsubscribe(string channel);
        /// <summary>
        /// 发布
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        long RedisPub<T>(string channel, T msg);
        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="subChannel"></param>
        /// <param name="deletegate"></param>
        void RedisSub(string subChannel, RedisHandle.RedisDeletegate deletegate);

        /// <summary>
        /// 批量塞值-字典
        /// </summary>
        /// <param name="map"></param>
        void SetAll(Dictionary<string, string> map);

        /// <summary>
        /// 批量塞值-列表
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        void SetAll(IEnumerable<string> keys, IEnumerable<string> values);

        /// <summary>
        /// 获取所有Key
        /// </summary>
        /// <returns></returns>
        List<string> GetAllKeys();

        /// <summary>
        /// 按正则条件删除。可以删除一张表前缀
        /// </summary>
        /// <param name="pattern"></param>
        void RemoveByRegex(string pattern);

        /// <summary>
        /// 按正则条件删除。可以删除一张表前缀
        /// </summary>
        /// <param name="pattern"></param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// 批量获取值-泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        List<T> GetValues<T>(List<string> keys);

        /// <summary>
        /// 批量获取值
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        List<string> GetValues(List<string> keys);

        /// <summary>
        /// 获取随机Key
        /// </summary>
        /// <returns></returns>
        string GetRandomKey();

        /// <summary>
        ///  追加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        long AppendToValue(string key, string value);

        /// <summary>
        /// 原子操作减N
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        long DecrementValueBy(string key, int count);

        /// <summary>
        /// 原子操作减1
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long DecrementValue(string key);

        /// <summary>
        /// 原子操作加N
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        long IncrementValueBy(string key, int count);

        /// <summary>
        /// 原子操作加1
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        long IncrementValue(string key);

        /// <summary>
        /// 存储数据到队列 先进先出
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="value"></param>
        void EnqueueItemOnList(string listId, string value);

        /// <summary>
        /// 从队列取数据 先进先出
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        string DequeueItemFromList(string listId);

        /// <summary>
        /// 根据key删除列表所有值
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="value"></param>
        void RemoveItemFromList(string listId, string value);

        /// <summary>
        /// 根据Key获取列表所有值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        List<string> GetAllItemsFromList(string key);

        /// <summary>
        /// 添加多个value 到现有List
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values"></param>
        void AddRangeToList(string key, List<string> values);

        /// <summary>
        /// 添加value 到现有List
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void AddItemToList(string key, string value);

        /// <summary>
        /// 取一个Hash的多个字段值时用此函数
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        List<string> GetValuesFromHash(string hashId, string[] keys);

        /// <summary>
        /// 取一个函数的单个字段值时，用此函数
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValueFromHash(string hashId, string key);

        /// <summary>
        /// 指定hashId key 存储Hash值
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetEntryInHash(string hashId, string key, string value);

        /// <summary>
        /// 指定Key 移除 hash值
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="key"></param>
        void RemoveEntryFromHash(string hashId, string key);

        /// <summary>
        /// 根据HashId 获取 hash values
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        List<string> GetHashValues(string hashId);

        /// <summary>
        /// 根据HashId 获取 hash keys
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        List<string> GetHashKeys(string hashId);

        /// <summary>
        /// 根据HashId 获取hash对象总数量
        /// </summary>
        /// <param name="hashId"></param>
        /// <returns></returns>
        int GetHashCount(string hashId);

        /// <summary>
        ///  将一组键值对写入一个hash。比如Dictionary<string,string>(){{"d1","1"},{"d2","2"}};，则生成的Hash存储是key为参数hashId，
        /// </summary>
        /// <param name="hashId"></param>
        /// <param name="keyValuePairs"></param>
        void SetRangeInHash(string hashId, IEnumerable<KeyValuePair<string, string>> keyValuePairs);

        /// <summary>
        /// 根据Id获取对象(存储的时候使用Hash)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetFromHash<T>(object id);

        /// <summary>
        /// 将一个对象存入Hash。比如对象User有Id=1和Name="aa"属性，则生成的Hash存储是key为urn:user:1，
        /// 第一行field为Id，它的value是1，第二行field为Name，它的value是"aa"。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        void StoreAsHash<T>(T entity);

        /// <summary>
        /// key如果不存在，则添加value，返回true；如果key已经存在，则不添加value，返回false。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="expirySeconds"></param>
        /// <returns></returns>
        bool SetIfNotExists<T>(string key, T entity, int expirySeconds = -1);

        /// <summary>
        /// 根据Key存储对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="expirySeconds"></param>
        void Set<T>(string key, T entity, int expirySeconds = -1);

        /// <summary>
        /// 根据Key获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 如果不存在key缓存，则添加，返回true。如果已经存在key缓存，则不作操作，返回false。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entity"></param>
        /// <param name="expirySeconds"></param>
        /// <returns></returns>
        bool AddIfNotExist<T>(string key, T entity, int expirySeconds = -1);

        /// <summary>
        /// 根据ID批量删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        void DeleteByIds<T>(ICollection ids) where T : class, new();

        /// <summary>
        /// 根据ID删除对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        void DeleteById<T>(object id) where T : class, new();

        /// <summary>
        /// 后台修改基础数据，要清基础数据的缓存时，可以全清。也可以单笔清DeleteById。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void DeleteAll<T>() where T : class, new();

        /// <summary>
        /// 常用。基础数据有时可以直接GetAll出来，然后对IList做过滤排序。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IList<T> GetAll<T>() where T : class, new();

        /// <summary>
        /// 常用。
        /// 后台关联出表数据时，可以用此函数取出对应用户，然后对表数据刷上用户名。        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ids"></param>
        /// <returns></returns>
        IList<T> GetByIds<T>(ICollection ids) where T : class, new();

        /// <summary>
        /// 获取object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById<T>(object id) where T : class, new();

        /// <summary>
        /// 可以Store泛型对象，此对象一定要包含Id字段，否则会报错。
        /// e.g.RedisHelper.StoreObject(new {Id = 101,Name = "name_11" });
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        object StoreObject(object entity);

        /// <summary>
        /// 可以对基础对象做StoreAll
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Store<T>(T entity) where T : class, new();

        /// <summary>
        /// 删除所有节点
        /// </summary>
        /// <param name="keys"></param>
        void RemoveAll(string[] keys);

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Remove(string key);

        /// <summary>
        /// 取值-字符
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValue(string key);

        /// <summary>
        /// 赋值-字符
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirySeconds"></param>
        void SetValue(string key, string value, int expirySeconds = -1);
    }
}