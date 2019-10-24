namespace DiegoRangel.DotNet.Framework.CQRS.API.ViewModels.PagedSearch
{
    public class PagedSearchWithFiltersInputViewModel<TFiltro> : PagedSearchInputViewModel
    {
        public TFiltro Filters { get; set; }
    }
}