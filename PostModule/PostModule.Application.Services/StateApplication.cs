﻿using PostModule.Application.Contract.StateApplication;
using PostModule.Domain.Services;
using PostModule.Domain.StateEntity;
using Shared.Application;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace PostModule.Application.Services
{
    internal class StateApplication : IStateApplication
    {
        private readonly IStateRepository _stateRepository;
        public StateApplication(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

		public bool ChangeStateClose(int id, List<int> stateCloses)
		{
            if (stateCloses.Count() < 1) return false;
            var state = _stateRepository.GetById(id);
            state.ChangeCloseStates(stateCloses);
            return _stateRepository.Update(state);
           
		}

		public OperationResult Create(CreateStateModel command)
        {
            if (_stateRepository.IsExists(s => s.Title == command.Title))
                return new(Status.BadRequest, ErrorMessages.DuplicateTitleError, nameof(command.Title));
            State state = new(command.Title);
            if (_stateRepository.Insert(state)) return new(Status.Success);
            return new(Status.InternalServerError,ErrorMessages.InternalServerError, nameof(command.Title)); 
        }

        public OperationResult Edit(EditStateModel command)
        {
			if (_stateRepository.IsExists(s => s.Title == command.Title && s.Id != command.Id))
				return new(Status.BadRequest, ErrorMessages.DuplicateTitleError, nameof(command.Title));
            State state = _stateRepository.GetById(command.Id);
            state.Edit(command.Title);
			state.ChangeCloseStates(command.CloseStates!);
            if (_stateRepository.Update(state)) return new(Status.Success);
			return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(command.Title));
		}

        public bool ExistTitleForCreate(string title) =>
            _stateRepository.IsExists(s => s.Title == title);

        public bool ExistTitleForEdit(string title, int id) =>
            _stateRepository.IsExists(s => s.Title == title && s.Id != id);

        public List<StateViewModel> GetAll() =>
            _stateRepository.GetAllStateViewModel();

        public EditStateModel GetStateForEdit(int id)
        {
            return _stateRepository.GetStateForEdit(id);
        }
    }
}
