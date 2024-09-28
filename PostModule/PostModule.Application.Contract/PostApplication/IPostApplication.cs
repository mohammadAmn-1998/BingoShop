using Shared.Application.Utility;

namespace PostModule.Application.Contract.PostApplication
{
    public interface IPostApplication
    {
        OperationResult Create(CreatePost command);
        OperationResult Edit(EditPost command);
        EditPost GetForEdit(int id);
        bool ActivationChange(int id);
        bool InsideCityChange(int id);
        bool OutSideCityChange(int id);
    }
}
