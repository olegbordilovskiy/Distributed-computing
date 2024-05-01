using DC_REST.Data;
using DC_REST.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace DC_REST.Repositories
{
	public class UserDbRepository : IRepository<User>
	{
		private readonly DatabaseContext _dbContext;
		private readonly IDistributedCache _distributedCache;

		public UserDbRepository(DatabaseContext dbContext, IDistributedCache distributedCache)
		{
			_dbContext = dbContext;
			_distributedCache = distributedCache;
		}

		public User GetById(int id)
		{
			string key = $"user-{id}";
			string? cachedUser = _distributedCache.GetString(key);

			User? user;
			if (string.IsNullOrEmpty(cachedUser))
			{
				user = _dbContext.tbl_user.Find(id);
				if (user == null)
				{
					return user;
				}

				_distributedCache.SetString(key, JsonConvert.SerializeObject(user));
				return user;
			}
			user = JsonConvert.DeserializeObject<User>(cachedUser);
			return user;
		}

		public List<User> GetAll()
		{
			return _dbContext.tbl_user.ToList();
		}

		public User Add(User user)
		{
			try
			{
				_dbContext.tbl_user.Add(user);
				_dbContext.SaveChanges();

				string key = $"user-{user.id}";
				_distributedCache.SetString(key, JsonConvert.SerializeObject(user));

				return user;
			}
			catch (Exception)
			{
				throw new DbUpdateException("Violation of uniqueness constraint");
			}
		}

		public User Update(int id, User user)
		{
			var existingUser = _dbContext.tbl_user.Find(id);

			if (existingUser == null)
			{
				throw new ArgumentException("There is no such issue");
			}

			try
			{
				existingUser.firstname = user.firstname;
				existingUser.lastname = user.lastname;
				existingUser.login = user.login;
				existingUser.password = user.password;

				_dbContext.SaveChanges();

				string key = $"user-{user.id}";
				_distributedCache.SetString(key, JsonConvert.SerializeObject(user));

			}
			catch (DbUpdateException)
			{
				throw new DbUpdateException("Violation of uniqueness constraint");
			}

			return existingUser;
		}

		public bool Delete(int id)
		{
			var user = _dbContext.tbl_user.Find(id);
			if (user == null)
				return false;

			string key = $"user-{user.id}";
			string? cachedUser = _distributedCache.GetString(key);
			if (!string.IsNullOrEmpty(cachedUser)) _distributedCache.Remove(key);

			_dbContext.tbl_user.Remove(user);
			_dbContext.SaveChanges();
			return true;
		}

		public int GetCurrentId()
		{
			throw new NotImplementedException();
		}
	}
}
