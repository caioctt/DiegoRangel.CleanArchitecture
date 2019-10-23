using System.Collections.Generic;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.PagedSearch
{
    public class PagedSearchList<T>
    {
        public long TotalResults { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public IList<T> List { get; set; }
    }
}