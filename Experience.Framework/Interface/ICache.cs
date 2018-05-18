using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experience.Framework
{
    public interface ICache
    {
         /// <summary>
        /// 添加一条缓存项
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Insert(string category, string key, object value);
        /// <summary>
        /// 添加一条缓存项，并且设置该缓存项的绝对过期时间
        /// </summary>
        /// <param name="category">分类</param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration">过期时间</param>
        void Insert(string category, string key, object value, DateTime absoluteExpiration);
        /// <summary>
        /// 添加一条缓存项，并且设置该缓存项的相对过期时间，该时间相对于最后一次访问的时间
        /// </summary>
        /// <param name="category"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slidingExpiration"></param>
        void Insert(string category, string key, object value, TimeSpan slidingExpiration);
        /// <summary>
        /// 删除一条缓存项
        /// </summary>
        /// <param name="category"></param>
        /// <param name="key"></param>
        void Remove(string category, string key);
        /// <summary>
        /// 获取缓存项
        /// </summary>
        /// <param name="category"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(string category, string key);
        object this[string category, string key] { get; set; }
        object this[string category, string key, TimeSpan slidingExpiration] { get; set; }
        int Count { get; }
    }
}
