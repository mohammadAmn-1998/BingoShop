using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Models;
using Shared.Application.Utility;

namespace Users1.Application.Contract.UserService.Query
{
    public class FilteredUsersQueryModel : BasePagination
    {

        public FilterParams FilterParams { get; set; }

        public List<UserQueryModel> Users { get; set; }

    }
}
