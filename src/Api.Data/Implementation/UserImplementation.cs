using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Entities;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;


namespace Api.Data.Implementation
{
    public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
    {
        private DbSet<UserEntity> _dataset;
        public UserImplementation(MyContext myContext) : base(myContext)
        {
            _dataset = myContext.Set<UserEntity>();
        }
        public async Task<UserEntity> FindByLogin(string email)
        {
            try
            {
                return await _dataset.FirstOrDefaultAsync(x => x.Email == email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
