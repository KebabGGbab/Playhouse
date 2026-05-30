namespace Playhouse.ViewModels.Services.ViewModelFactories.Abstractions
{
    public interface IViewModelFactory<TViewModel, TModel>
    {
        TViewModel Create();

        TViewModel Create(TModel model);
    }
}
