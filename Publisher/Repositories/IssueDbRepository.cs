using DC_REST.Data;
using DC_REST.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;

namespace DC_REST.Repositories
{
	public class IssueDbRepository : IRepository<Issue>
	{
		private readonly DatabaseContext _dbContext;
		private readonly IDistributedCache _distributedCache;

		public IssueDbRepository(DatabaseContext dbContext, IDistributedCache distributedCache)
		{
			_dbContext = dbContext;
			_distributedCache = distributedCache;
		}

		public Issue GetById(int id)
		{
			string key = $"issue-{id}";
			string? cachedIssue = _distributedCache.GetString(key);

			Issue? issue;
			if (string.IsNullOrEmpty(cachedIssue))
			{
				issue = _dbContext.tbl_issue.Find(id);

				if (issue == null)
				{
					return issue;
				}

				_distributedCache.SetString(key, JsonConvert.SerializeObject(issue));
				return issue;
			}
			issue = JsonConvert.DeserializeObject<Issue>(cachedIssue);
			return issue;
		
		}

		public List<Issue> GetAll()
		{
			return _dbContext.tbl_issue.ToList();
		}

		public Issue Add(Issue issue)
		{
			
			try
			{
				_dbContext.tbl_issue.Add(issue);
				_dbContext.SaveChanges();

				string key = $"issue-{issue.id}";
				_distributedCache.SetString(key, JsonConvert.SerializeObject(issue));	

				return issue;
			}
			catch (Exception)
			{
				throw new DbUpdateException("Violation of uniqueness constraint");
			}
		}

		public Issue Update(int id, Issue issue)
		{
			
			var existingIssue = _dbContext.tbl_issue.Find(id);

			if (existingIssue == null)
			{
				throw new ArgumentException("There is no such issue");
			}

			try
			{
				existingIssue.UserId = issue.UserId;
				existingIssue.Content = issue.Content;
				existingIssue.Title = issue.Title;
				_dbContext.SaveChanges();

				string key = $"issue-{issue.id}";
				_distributedCache.SetString(key, JsonConvert.SerializeObject(issue));

			}
			catch (DbUpdateException) 
			{
				throw new DbUpdateException("Violation of uniqueness constraint");
			}

			return existingIssue;
		}

		public bool Delete(int id)
		{
			var issue = _dbContext.tbl_issue.Find(id);
			if (issue == null)
				return false;

			string key = $"issue-{issue.id}";
			string? cachedIssue = _distributedCache.GetString(key);
			if (!string.IsNullOrEmpty(cachedIssue)) _distributedCache.Remove(key);

			_dbContext.tbl_issue.Remove(issue);
			_dbContext.SaveChanges();
			return true;
		}

		public int GetCurrentId()
		{
			throw new NotImplementedException();
		}
	}
}
