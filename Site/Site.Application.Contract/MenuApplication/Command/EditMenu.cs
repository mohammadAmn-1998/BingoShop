using Microsoft.AspNetCore.Http;
using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace Site.Application.Contract.MenuApplication.Command
{
    public class EditMenu : UbsertMenu
    {
        public long Id { get; set; }
        public long ParentId { get; set; }
        public string? ImageName { get; set; }
    }
}
