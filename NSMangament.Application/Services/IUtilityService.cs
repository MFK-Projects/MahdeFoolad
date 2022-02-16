using NSMangament.Application.Models;


namespace NSMangament.Application.Services
{
    public interface IUtilityService
    {
        string UrlBuilder(UrlBuilderModel model);
        string TaskUrlBuilder(UrlBuilderModel model);
    }
}
