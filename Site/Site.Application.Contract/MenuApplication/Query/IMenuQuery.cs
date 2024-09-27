

namespace Site.Application.Contract.MenuApplication.Query
{
    public interface IMenuQuery
    {
        Task<MenuPageAdminQueryModel> GetForAdmin(int parentId);
        List<MenuForUi> GetForIndex();
        List<MenuForUi> GetForFooter();
        List<MenuForUi> GetForBlog();

    }
}
