using Shared.Application;
using PostModule.Application.Contract.PostApplication;
using PostModule.Domain.PostEntity;
using PostModule.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace PostModule.Application.Services
{
    internal class PostApplication : IPostApplication
    {
        private readonly IPostRepository _postRepository;
        public PostApplication(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public bool ActivationChange(int id)
        {
            var post = _postRepository.GetById(id);
            post.ActivationChange();
            return _postRepository.Save();
        }

        public OperationResult Create(CreatePost command)
        {
            if (_postRepository.IsExists(p => p.Title == command.Title))
                return new OperationResult(Status.BadRequest, ErrorMessages.DuplicateError, "Title");
            Post post = new(command.Title, command.Status, command.TehranPricePlus, command.StateCenterPricePlus,
               command.CityPricePlus, command.InsideStatePricePlus, command.StateClosePricePlus,
               command.StateNonClosePricePlus,command.Description);
            if (_postRepository.Insert(post))
                return new(Status.Success);

            return new OperationResult(Status.InternalServerError, ErrorMessages.InternalServerError, "Title");
        }

        public OperationResult Edit(EditPost command)
        {
            if (_postRepository.IsExists(p => p.Title == command.Title && p.Id != command.Id))
                return new OperationResult(Status.BadRequest, ErrorMessages.DuplicateTitleError, "Title");
            var post = _postRepository.GetById(command.Id);
             post.Edit(command.Title, command.Status, command.TehranPricePlus, command.StateCenterPricePlus,
               command.CityPricePlus, command.InsideStatePricePlus, command.StateClosePricePlus,
               command.StateNonClosePricePlus,command.Description);
            if (_postRepository.Save())
                return new(Status.Success);

            return new OperationResult(Status.InternalServerError, ErrorMessages.InternalServerError, "Title");
        }

        public EditPost GetForEdit(int id)
        {
            return _postRepository.GetForEdit(id);
        }

        public bool InsideCityChange(int id)
        {
            var post = _postRepository.GetById(id);
            post.InsideCityChange();
            return _postRepository.Save();
        }

        public bool OutSideCityChange(int id)
        {
            var post = _postRepository.GetById(id);
            post.OutSideCityChange();
            return _postRepository.Save();
        }
    }
}
