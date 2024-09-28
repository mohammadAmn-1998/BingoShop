using PostModule.Application.Contract.PostPriceApplication;
using PostModule.Domain.PostEntity;
using PostModule.Domain.Services;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace PostModule.Application.Services
{
    internal class PostPriceApplication : IPostPriceApplication
    {
        private readonly IPostPriceRepository _postPriceRepository;
        public PostPriceApplication(IPostPriceRepository postPriceRepository)
        {
            _postPriceRepository = postPriceRepository;
        }

        public OperationResult Create(CreatePostPrice command)
        {
            PostPrice postPrice = new(command.PostId, command.Start, command.End, command.TehranPrice,
                command.StateCenterPrice, command.CityPrice, command.InsideStatePrice,
                command.StateClosePrice, command.StateNonClosePrice);
            if (_postPriceRepository.Insert(postPrice))
                return new(Status.Success);

            return new(Status.InternalServerError, ErrorMessages.InternalServerError, "Start");
        }

        public OperationResult Edit(EditPostPrice command)
        {
            var postPrice = _postPriceRepository.GetById(command.Id);
            postPrice.Edit(command.Start, command.End, command.TehranPrice,
                command.StateCenterPrice, command.CityPrice, command.InsideStatePrice,
                command.StateClosePrice, command.StateNonClosePrice);
            if (_postPriceRepository.Save())
                return new(Status.Success);

            return new(Status.InternalServerError, ErrorMessages.InternalServerError, "Start");
        }

        public EditPostPrice GetForEdit(int id)
        {
            return _postPriceRepository.GetForEdit(id);
        }
    }
}
