using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Data.Repositories
{
	public interface IRepository<TEntity> where TEntity : class
	{
		void Add(TEntity entity);

		void BulkAdd(List<TEntity> entities);

		void Update(TEntity entity);
		void BulkUpdate(List<TEntity> entity);
		void Delete(int id);
		void Delete(TEntity entity);

		public void HardDelete(TEntity entity);
	

		TEntity GetById(int id);
		TEntity GetById(string id);
		TEntity Get(Expression<Func<TEntity, bool>> predicate);
		IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);


	}
}
