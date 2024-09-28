using Shared.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;

namespace PostModule.Application.Contract.StateApplication
{
    public class CreateStateModel
    {
        [Display(Name = "نام استان")]
        [Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        [MaxLength(250,ErrorMessage = ErrorMessages.MaxLengthError)]
        public string Title { get; set; }
    }
}
