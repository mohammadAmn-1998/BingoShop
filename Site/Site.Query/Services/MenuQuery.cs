
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;
using Site.Application.Contract.MenuApplication.Query;
using Site.Domain.MenuAgg;
using Site.Infrastructure;

namespace Site.Query.Services;

internal class MenuQuery : BaseRepository, IMenuQuery
{
	private readonly SiteContext _context;

	public MenuQuery(SiteContext context) : base(context)
	{
		_context = context;
	}

    public async Task<MenuPageAdminQueryModel> GetForAdmin(int parentId)
    {
        MenuPageAdminQueryModel model = new()
        {
            Id = parentId
        };
        if (parentId == 0)
        {
            model.PageTitle = "لیست منو های سردسته";
            model.Menus = Table<Menu>().Where(m => m.ParentId == null)
                .Select(m => new MenuForAdminQueryModel
                {
                    Active = m.Active,
                    CreationDate = m.CreateDate.ConvertToPersianDate(),
                    Id = m.Id,
                    Number = m.Number,
                    Status = m.Status,
                    Title = m.Title,
                    Url = m.Url,
                    ImageName = m.ImageName
                }).ToList();
        }
        else
        {
            var menuParent =await GetById<Menu>(parentId);
            model.PageTitle = $"لیست زیر منو های {menuParent.Title} - وضعیت {menuParent.Status.ToString().Replace("_"," ")}";
            model.Status = menuParent.Status;
            model.Menus = Table<Menu>().Where(m => m.ParentId == parentId)
               .Select(m => new MenuForAdminQueryModel
               {
                   Active = m.Active,
                   CreationDate = m.CreateDate.ConvertToPersianDate(),
                   Id = m.Id,
                   Number = m.Number,
                   Status = m.Status,
                   Title = m.Title,
                   Url = m.Url,
                   ImageName =  m.ImageName
               }).ToList();
        }
        return model;
    }

    public List<MenuForUi> GetForBlog()
    {
        List<MenuForUi> model = new();
        var menus = Table<Menu>().Where(b => b.Active &&
        (b.Status == MenuStatus.منوی_وبلاگ_لینک
        || b.Status == MenuStatus.منوی_وبلاگ_با_زیرمنوی_بدون_عکس
        || b.Status == MenuStatus.منوی_وبلاگ_با_زیر_منوی_عکس_دار));
        foreach(var item in menus)
        {
            MenuForUi menu = new()
            {
                Number = item.Number,
                Title = item.Title,
                Url = item.Url,
                Status = item.Status,
                Childs = new()
            };
            if(Table<Menu>().Any(m=>m.ParentId == item.Id && m.Active))
                menu.Childs = Table<Menu>().Where(m => m.Active && m.ParentId == item.Id)
                .Select(m => new MenuForUi
                {
                    ImageAlt = m.ImageAlt,
                    Childs = new(),
                    ImageName = m.ImageName,
                    Number = m.Number,
                    Title = m.Title,
                    Url = m.Url,
                    Status = m.Status
                }).ToList();

            model.Add(menu);
        }
        return model;
    }

    public List<MenuForUi> GetForFooter()
    {
        List<MenuForUi> model = new();
        var menus = Table<Menu>().Where(b => b.Active &&
        (b.Status == MenuStatus.تیتر_منوی_فوتر));
        foreach (var item in menus)
        {
            MenuForUi menu = new()
            {
                Number = item.Number,
                Title = item.Title,
                Url = item.Url,
                Status = item.Status,
                Childs = new()
            };
            if (Table<Menu>().Any(m => m.ParentId == item.Id && m.Active))
                menu.Childs = Table<Menu>().Where(m => m.Active && m.ParentId == item.Id)
                .Select(m => new MenuForUi
                {
                    Number = m.Number,
                    Title = m.Title,
                    Url = m.Url,
                    Status = m.Status
                }).ToList();

            model.Add(menu);
        }
        return model;
    }

    public List<MenuForUi> GetForIndex()
    {
        List<MenuForUi> model = new();
        var menus = _context.Menus.Where(b => b.Active &&
        (b.Status == MenuStatus.منوی_اصلی
        || b.Status == MenuStatus.منوی_اصلی_با_زیر_منو
        ));
        foreach (var item in menus)
        {
            MenuForUi menu = new()
            {
                Number = item.Number,
                Title = item.Title,
                Url = item.Url,
                ImageAlt = item.ImageAlt,
                ImageName = string.IsNullOrEmpty(item.ImageName) ? "" : item.ImageName,
                Childs = new(),
                Status = item.Status
            };
            if (_context.Menus.Any(m => m.ParentId == item.Id && m.Active))
                menu.Childs = Table<Menu>().Where(m => m.Active && m.ParentId == item.Id)
                .Select(m => new MenuForUi
                {
                    Id= m.Id,
                    Childs = new(),
                    Number = m.Number,
                    Title = m.Title,
                    Url = m.Url,
                    Status = m.Status
                }).ToList();

            model.Add(menu);
        }
        foreach(var item in model.Where(w=>w.Status == MenuStatus.منوی_اصلی_با_زیر_منو && w.Childs.Count() > 0))
        {
            foreach(var sub in item.Childs)
            {
                sub.Childs = Table<Menu>().Where(m => m.Active && m.ParentId == sub.Id)
                .Select(m => new MenuForUi
                {
                    Childs = new(),
                    Number = m.Number,
                    Title = m.Title,
                    Url = m.Url,
                    Status = m.Status
                }).ToList();
            }
        }
        return model;
    }
}
