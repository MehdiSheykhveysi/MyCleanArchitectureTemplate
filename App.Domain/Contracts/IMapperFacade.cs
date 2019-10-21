namespace App.Domain.Contracts
{
    public interface IMapperFacade
    {
        TOutPut Map<TOutPut, TInput>(TInput input);
    }
}
