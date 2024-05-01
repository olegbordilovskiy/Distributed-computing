using DC_REST.Data;
using DC_REST.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace DC_REST.Repositories
{
	public class LabelDbRepository:IRepository<Label>
	{
		private readonly DatabaseContext _dbContext;
		private readonly IDistributedCache _distributedCache;

		public LabelDbRepository(DatabaseContext dbContext, IDistributedCache distributedCache)
		{
			_dbContext = dbContext;
			_distributedCache = distributedCache;
		}

		public Label GetById(int id)
		{
			string key = $"label-{id}";
			string? cachedLabel = _distributedCache.GetString(key);

			Label? label;
			if (string.IsNullOrEmpty(cachedLabel))
			{
				label = _dbContext.tbl_label.Find(id);
				if (label == null)
				{
					return label;
				}

				_distributedCache.SetString(key, JsonConvert.SerializeObject(label));
				return label;
			}
			label = JsonConvert.DeserializeObject<Label>(cachedLabel);
			return label;
		}

		public List<Label> GetAll()
		{
			return _dbContext.tbl_label.ToList();
		}

		public Label Add(Label label)
		{
			try
			{
				_dbContext.tbl_label.Add(label);
				_dbContext.SaveChanges();

				string key = $"label-{label.id}";
				_distributedCache.SetString(key, JsonConvert.SerializeObject(label));

				return label;
			}
			catch (Exception)
			{
				throw new DbUpdateException("Violation of uniqueness constraint");
			}
		}

		public Label Update(int id, Label label)
		{
			var existingLabel = _dbContext.tbl_label.Find(id);

			if (existingLabel == null)
			{
				throw new ArgumentException("There is no such issue");
			}

			try
			{
				existingLabel.Name = label.Name;
				_dbContext.SaveChanges();

				string key = $"label-{label.id}";
				_distributedCache.SetString(key, JsonConvert.SerializeObject(label));

			}
			catch (DbUpdateException)
			{
				throw new DbUpdateException("Violation of uniqueness constraint");
			}

			return existingLabel;
		}

		public bool Delete(int id)
		{
			var label = _dbContext.tbl_label.Find(id);
			if (label == null)
				return false;

			string key = $"label-{label.id}";
			string? cachedLabel = _distributedCache.GetString(key);
			if (!string.IsNullOrEmpty(cachedLabel)) _distributedCache.Remove(key);

			_dbContext.tbl_label.Remove(label);
			_dbContext.SaveChanges();
			return true;
		}

		public int GetCurrentId()
		{
			throw new NotImplementedException();
		}
	}
}
