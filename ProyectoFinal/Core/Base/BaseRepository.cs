using System;
using Persistence;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tools;

namespace Core.Base
{
    public class BaseRepository<T> : IDisposable
        where T : class
    {
        protected readonly SVContext dbContext;
        public DbSet<T> Entity => this.dbContext.Set<T>();

        public BaseRepository(SVContext context)
        {
            this.dbContext = context;
        }

        public virtual IQueryable<T> Select(Expression<Func<T, bool>> expression = null)
        {
            var result = this.Entity.AsQueryable();

            return expression != null ? result.Where(expression) : result;
        }

        //public virtual async Task<IPagination<T>> Select(Expression<Func<T, bool>> expression = null, int Page = 1, int Length = 10)
        //{
        //    return await Task.Run(()=>
        //    {
        //        var paginator = new Paginator<T>(this.Select(expression), Page, Length);
        //        return paginator.Pagination;
        //    });
        //}

        public async virtual Task Update(T entity, params Expression<Func<T, object>>[] Properties)
        {
            await Task.Run(() =>
            {
                if (!Properties.Any())
                    throw new Exception("");

                var dbEntity = this.dbContext.Entry(entity);

                foreach (var property in Properties)
                {
                    dbEntity.Property(property).IsModified = true;
                }
            });
        }

        // public async virtual Task<bool> Remove(T entity)
        // {
        //     this.dbContext.Set<T>().Remove(entity);
        //     await this.dbContext.SaveChangesAsync();
        //     return true;
        // }

        // public async virtual Task<bool> Remove(Expression<Func<T, bool>> expression)
        // {
        //     var t = this.dbContext.Set<T>().FirstOrDefault(expression);
        //     this.dbContext.Set<T>().Remove(t);
        //     await this.dbContext.SaveChangesAsync();
        //     return true;
        // }

        public async Task ChangeEstado(int id)
        {
            try
            {
                await Task.Run(() =>
                {
                    var entity = this.dbContext.Set<T>().FirstOrDefault(y =>
                        int.Parse(y.GetType().GetProperty("Id").GetValue(y).ToString()) == id);
                    var value = entity.GetType().GetProperty("Estado").GetValue(entity).ToString();
                    entity.GetType().GetProperty("Estado").SetValue(entity, !bool.Parse(value));
                    this.dbContext.Set<T>().Update(entity);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //protected async Task<IPagination<TResult>> Paged<T, TResult>(IQueryable<T> content, int Page, int LengthForPage)
        //{
        //    return await Task.Run(() =>
        //    {
        //        var paginator = new Paginator<T, TResult>(content, Page, LengthForPage);
        //        return paginator.Pagination;
        //    });
        //}

        ~BaseRepository()
        {
            this.Dispose();
        }
        public void Dispose()
        {
            this.dbContext.Dispose();
        }
    }
}
