using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MyContext _myContext;
        private DbSet<T> _dataset;
        public BaseRepository(MyContext myContext) // O método busca o contexto
        {
            _myContext = myContext;
            _dataset = _myContext.Set<T>(); // Adiciona a tabela ao contexto => Ex: _dataset.Users, dataset.Prices
        }


        public async Task<bool> ExistAsync(Guid id)
        {
            // Any retona true ou false
            var result = await _dataset.AnyAsync(x => x.Id == id);
            return result;
        }

        public async Task<IEnumerable<T>> SelectAllAsync()
        {
            try
            {
                var result = await _dataset.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<T> SelectByIdAsync(Guid id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(x => x.Id == id);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == Guid.Empty) // Guid.Empty => valor guid vazio
                {
                    item.Id = Guid.NewGuid();
                }
                item.CreateAt = DateTime.UtcNow; // Data atual

                await _dataset.AddAsync(item);
                await _myContext.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(x => x.Id == item.Id);
                if (result == null)
                {
                    return null;
                }
                item.Id = result.Id; // Para garantir que não irá mudar
                item.CreateAt = result.CreateAt; // Para garantir que nã oirá mudar
                item.UpdateAt = DateTime.UtcNow; // Datya atual

                _myContext.Entry(result).CurrentValues.SetValues(item); // Set os valores atuais (result) pelos novos (item)
                await _myContext.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var result = await _dataset.SingleOrDefaultAsync(x => x.Id == id);
                if (result == null)
                {
                    return false;
                }
                _dataset.Remove(result);
                await _myContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
