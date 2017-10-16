using System;
using System.Collections.Generic;

namespace LS.UtilityTools
{
    public class DataPagerContainer<T> where T : class
    {
        public IEnumerable<T> Data { get;  set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public Int32 PageCount
        {
            get
            {
                return (Int32)Math.Ceiling((Double)TotalCount / (Double)PageSize);
            }
        }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; private set; }

        public DataPagerContainer(IEnumerable<T> data, Int32 pageIndex, Int32 pageSize, Int32 totalCount)
        {
            this.Data = data ?? new List<T>();
            this.PageIndex = pageIndex == 0 ? 1 : pageIndex;
            this.PageSize = pageSize == 0 ? 1 : pageSize;
            this.TotalCount = totalCount;
        }
    }
}