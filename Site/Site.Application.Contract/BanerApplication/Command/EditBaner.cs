using Microsoft.AspNetCore.Http;
using Shared.Application;
using System.ComponentModel.DataAnnotations;
using Shared.Application.Utility;

namespace Site.Application.Contract.BanerApplication.Command
{
    public class EditBaner : CreateBaner
    {
        public long Id { get; set; }

       
    }
}
