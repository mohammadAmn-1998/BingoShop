using System.ComponentModel.DataAnnotations;
using Shared.Application.Utility;

namespace PostModule.Application.Contract.CityApplication
{
	public class CreateCityModel
    {
        public int StateId { get; set; }
		[Display(Name = "نام شهر")]
		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
		[MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
		public string Title { get; set; }
    }
}
