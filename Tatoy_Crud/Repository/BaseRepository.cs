using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Tatoy_Crud.Contracts;

namespace Tatoy_Crud.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        private DbContext _db;
        public DbSet<T> Table;

        public BaseRepository()
        {
            _db = new CRUDEntities();
            Table = _db.Set<T>();
        }
        public T Get(object id)
        {
            return Table.Find(id);
        }

        public List<T> GetAll()
        {
            return Table.ToList();
        }
        public ErrorCode Create(T t)
        {
            try
            {
                Table.Add(t);
                _db.SaveChanges();
                return ErrorCode.Success;
            }
            catch (Exception ex)
            {

                return ErrorCode.Error;
            }
        }

        public ErrorCode Delete(object id)
        {
            try
            {
                var obj = Get(id);
                Table.Remove(obj);
                _db.SaveChanges();

                return ErrorCode.Success;
            }
            catch (Exception ex)
            {

                return ErrorCode.Error;
            }
        }

        public ErrorCode Update(object id, T t)
        {
            try
            {
                var oldObj = Get(id);
                _db.Entry(oldObj).CurrentValues.SetValues(t);
                _db.SaveChanges();

                return ErrorCode.Success;
            }
            catch (Exception ex)
            {

                return ErrorCode.Error;
            }
        }
    }
}