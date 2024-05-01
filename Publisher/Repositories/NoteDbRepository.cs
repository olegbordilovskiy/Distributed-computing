﻿using DC_REST.Data;
using DC_REST.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace DC_REST.Repositories
{
	public class NoteDbRepository : IRepository<Note>
	{
		private readonly DatabaseContext _dbContext;
		private readonly IDistributedCache _distributedCache;

		public NoteDbRepository(DatabaseContext dbContext, IDistributedCache distributedCache)
		{
			_dbContext = dbContext;
			_distributedCache = distributedCache;
		}

		public Note GetById(int id)
		{
			string key = $"note-{id}";
			string? cachedNote = _distributedCache.GetString(key);

			Note? note;
			if (string.IsNullOrEmpty(cachedNote))
			{
				note = _dbContext.tbl_note.Find(id);
				if (note == null)
				{
					return note;
				}

				_distributedCache.SetString(key, JsonConvert.SerializeObject(note));
				return note;
			}
			note = JsonConvert.DeserializeObject<Note>(cachedNote);
			return note;
		}

		public List<Note> GetAll()
		{
			return _dbContext.tbl_note.ToList();
		}

		public Note Add(Note note)
		{
			try
			{
				_dbContext.tbl_note.Add(note);
				_dbContext.SaveChanges();

				string key = $"note-{note.id}";
				_distributedCache.SetString(key, JsonConvert.SerializeObject(note));

				return note;
			}
			catch (Exception)
			{
				throw new DbUpdateException("Violation of uniqueness constraint");
			}
		}

		public Note Update(int id, Note note)
		{
			var existingNote = _dbContext.tbl_note.Find(id);

			if (existingNote == null)
			{
				throw new ArgumentException("There is no such issue");
			}

			try
			{
				existingNote.IssueId = note.IssueId;
				existingNote.Content = note.Content;
				_dbContext.SaveChanges();

				string key = $"note-{note.id}";
				_distributedCache.SetString(key, JsonConvert.SerializeObject(note));

			}
			catch (DbUpdateException)
			{
				throw new DbUpdateException("Violation of uniqueness constraint");
			}

			return existingNote;
		}

		public bool Delete(int id)
		{
			var note = _dbContext.tbl_note.Find(id);
			if (note == null)
				return false;

			string key = $"note-{note.id}";
			string? cachedNote = _distributedCache.GetString(key);
			if (!string.IsNullOrEmpty(cachedNote)) _distributedCache.Remove(key);

			_dbContext.tbl_note.Remove(note);
			_dbContext.SaveChanges();
			return true;
		}

		public int GetCurrentId()
		{
			throw new NotImplementedException();
		}
	}
}

