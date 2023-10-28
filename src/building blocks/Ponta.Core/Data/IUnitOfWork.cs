namespace Ponta.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
