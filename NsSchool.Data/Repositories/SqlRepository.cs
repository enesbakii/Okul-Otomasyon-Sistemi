using Microsoft.EntityFrameworkCore;
using NsSchool.Data.Context;
using NsSchool.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Data.Repositories
{
	public class SqlRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
	{
		private readonly NsSchoolDbContext _db;
		private readonly DbSet<TEntity> _dbSet;
		public SqlRepository(NsSchoolDbContext db)
		{
			_db = db;
			_dbSet=db.Set<TEntity>();
		}

		public void Add(TEntity entity)
		{
			_dbSet.Add(entity);
			_db.SaveChanges();
		}

		public void BulkAdd(List<TEntity> entities)
		{
			_dbSet.AddRange(entities);
			_db.SaveChanges();
		}

		public void BulkUpdate(List<TEntity> entities)
		{
			_dbSet.UpdateRange(entities);
			_db.SaveChanges();
		}

		public void Delete(TEntity entity)
		{
			entity.ModifiedDate = DateTime.Now;
			entity.IsDeleted= true;
			_dbSet.Update(entity);
			_db.SaveChanges();
		}

		public void Delete(int id)
		{
			var entity= GetById(id);
			Delete(entity);
		}

		public TEntity Get(Expression<Func<TEntity, bool>> predicate)
		{
			return _dbSet.FirstOrDefault(predicate);
		}

		public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
		{
			return predicate is not null ? _dbSet.Where(predicate) : _dbSet;
		}

		public TEntity GetById(int id)
		{
			 return _dbSet.Find(id);
		
		}
		public TEntity GetById(string id)
		{
			return _dbSet.Find(id);

		}

		public void HardDelete(TEntity entity)
		{
			
				entity.ModifiedDate = DateTime.Now;
				entity.IsDeleted = true;
				_dbSet.Remove(entity);
				_db.SaveChanges();
			
		}

		public void Update(TEntity entity)
		{
			entity.ModifiedDate= DateTime.Now;
			_dbSet.Update(entity);
			_db.SaveChanges();

		}
	}
}
