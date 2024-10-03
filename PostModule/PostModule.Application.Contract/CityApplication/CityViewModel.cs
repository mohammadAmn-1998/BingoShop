﻿
using Shared.Domain.Enums;

namespace PostModule.Application.Contract.CityApplication
{
    public class CityViewModel
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public string Title { get; set; }
        public CityStatus Status { get; set; }
        public string CreateDate { get; set; }
    }
}
