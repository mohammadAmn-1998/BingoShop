﻿
using PostModule.Domain.StateEntity;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;

namespace PostModule.Domain.CityEntity
{
    public class City : BaseEntity<int>
    {
        public int StateId { get; private set; }
        public string Title { get; private set; }
        public CityStatus Status { get; private set; }
        public State State { get; private set; }
        public City(int stateId,string title,CityStatus status)
        {
            StateId = stateId;
            Title = title;
            Status = status;
        }
        public void Edit(string title, CityStatus status)
        {
            Title = title;
            Status = status;
        }
		public void ChangeStatus(CityStatus status)
		{
            Status = status;
		}
	}
}
