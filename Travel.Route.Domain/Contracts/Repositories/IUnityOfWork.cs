namespace Travel.Route.Domain.Contracts.Repositories
{
    public interface IUnityOfWork
    {
        void Rollback();

        Task<int> CommitAsync();

        void BeginTransaction();

        void CommitTransaction();

        void RollBackTransaction();
    }
}
