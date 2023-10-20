

namespace Keepr.Interfaces
{
    public interface IRepository<T, Tid>
    {
        List<T> Get();

        T GetById(Tid id);

        T Create(T newData);

        public void Update(T updateData);

        internal void Delete(Tid id);
    }
}
// theses are the base methods generated for each
// Does NOT include get by ACCOUNT or VAULT id methods