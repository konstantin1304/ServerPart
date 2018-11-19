using ServerPart.DataBase.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPart.DataBase.Domain.Common
{
    public class DbRepository<T> : IDbRepository<T>
         where T : class, IDbEntity
    {
        private DbContext _context;

        public DbRepository(DbContext context)
        {
            _context = context;
        }
        public IQueryable<T> AllItems
        {
            get
            {
                return _context.Set<T>();
            }
        }
        public bool AddItem(T item)
        {
            try
            {
                _context.Set<T>().Add(item);
                _context.SaveChanges();
                return true;
            }
#pragma warning disable CS0168
            catch (Exception ex)
#pragma warning restore CS0168
            {
                return false;
            }
        }
        public bool AddItems(IEnumerable<T> items)
        {
            try
            {
                _context.Set<T>().AddRange(items);
                _context.SaveChanges();
                return true;
            }
#pragma warning disable CS0168
            catch (Exception ex)
#pragma warning restore CS0168
            {
                return false;
            }
        }

        public bool ChangeItem(T item)
        {
            try
            {
                T modifired = _context.Set<T>().Single(x => x.Id.CompareTo(item.Id) == 0);
                modifired = item;
                _context.SaveChanges();
                return true;
            }
#pragma warning disable CS0168
            catch (Exception ex)
#pragma warning restore CS0168
            {
                return false;
            }
        }

        public bool DeleteItem(int id)
        {
            try
            {
                T item = GetItem(id);
                _context.Set<T>().Remove(item);
                _context.SaveChanges();
                return true;
            }
#pragma warning disable CS0168
            catch (Exception ex)
#pragma warning restore CS0168
            {
                return false;
            }
        }

        public T GetItem(int id)
        {

            T item = _context.Set<T>().SingleOrDefault(x => x.Id.CompareTo(id) == 0);
            return item;
        }

        public bool SaveChanges()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
#pragma warning disable CS0168
            catch (Exception ex)
#pragma warning restore CS0168
            {
                return false;
            }
        }
    }
}
