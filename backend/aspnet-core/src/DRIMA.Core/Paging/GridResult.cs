using System.Collections.Generic;

namespace DRIMA.Paging
{
    public class GridResult<T> where T : class
    {
        public int TotalCount { get; set; }
        public IReadOnlyList<T> Items { get; set; }

        public GridResult(IReadOnlyList<T> items, int total)
        {
            Items = items;
            TotalCount = total;
        }
    }
}
