using Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PostModule.Application.Contract.StateApplication;
using PostModule.Domain.Services;
using PostModule.Domain.StateEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Shared.Infrastructure.BaseRepository;

namespace PostModule.Infrastracture.EF.Repositories;

internal class StateRepository : Repository<int, State>, IStateRepository
{
    private readonly PostContext _context;
    public StateRepository(PostContext context) : base(context)
    {
        _context = context;
    }

    public List<StateViewModel> GetAllStateViewModel()
    {
        return GetAll().Select(s => new StateViewModel { 
            CreateDate=s.CreateDate.ToString(),
            Id=s.Id,
            Title=s.Title

        }).ToList();

    }

    public EditStateModel GetStateForEdit(int id)
    {
        var state = GetBy(x=> x.Id ==id);
        return new()
        {
            Id=state.Id,
            Title=state.Title,
			CloseStates = state.CloseStates == "" ? new List<int>(): state.CloseStates.Split("-").Select(int.Parse).ToList() 
        };
    }
}
