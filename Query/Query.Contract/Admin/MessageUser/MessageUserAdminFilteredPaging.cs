using System.ComponentModel.DataAnnotations;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Query.Contract.Admin.MessageUser;

public class MessageUserAdminFilteredPaging : BasePagination
{
	public FilterParams FilterParams { get; set; }

	public List<MessageUserAdminQueryModel> Messages { get; set; }

	[Display(Name = "جستجو بر اساس وضعیت")]
	public MessageStatus? MessageStatus { get; set; }
}