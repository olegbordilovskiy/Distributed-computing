using System;
using System.Collections.Generic;
using System.Numerics;
using Cassandra;
using Discussion.Entities;
using Discussion.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

public class NoteDbRepository : IRepository<Note>
{
	private readonly Cassandra.ISession _session;
	private readonly IDistributedCache _distributedCache;

	public NoteDbRepository(Cassandra.ISession session, IDistributedCache distributedCache)
	{
		_session = session;
		_distributedCache = distributedCache;
	}

	public Note GetById(int id)
	{
		string key = $"note-{id}";
		//string? cachedNote = _distributedCache.GetString(key);
		string cachedNote = null;

		Note? note;

		if (string.IsNullOrEmpty(cachedNote)) 
		{
			var query = $"SELECT * FROM tbl_note WHERE id = {id}";
			var row = _session.Execute(query).FirstOrDefault();

			if (row == null)
			{
				throw new InvalidOperationException("Entity not found in the database.");
			}

			note = new Note
			{
				Id = (int)row.GetValue<long>("id"),
				IssueId = (int)row.GetValue<long>("issue_id"),
				Content = row.GetValue<string>("content"),
			};

			_distributedCache.SetString(key, JsonConvert.SerializeObject(note));
			return note;
		}

		note = JsonConvert.DeserializeObject<Note>(cachedNote);
		return note;

	}

	public List<Note> GetAll()
	{
		var query = "SELECT * FROM tbl_note";
		var rows = _session.Execute(query);

		var notes = new List<Note>();
		foreach (var row in rows)
		{
			notes.Add(new Note
			{
				Id = (int)row.GetValue<long>("id"),
				IssueId = (int)row.GetValue<long>("issue_id"),
				Content = row.GetValue<string>("content"),
			});
		}

		return notes;
	}

	public Note Add(Note note)
	{
		var query = $"INSERT INTO tbl_note (id, content, issue_id) VALUES ({note.Id}, '{note.Content}', {note.IssueId})";

		_session.Execute(query);

		string key = $"label-{note.Id}";
		_distributedCache.SetString(key, JsonConvert.SerializeObject(note));

		return note;
	}

	public Note Update(int id, Note note)
	{
		var query = $"UPDATE tbl_note SET content = '{note.Content}' WHERE id = {id} AND issue_id = {note.IssueId}";
		_session.Execute(query);

		//string key = $"label-{note.Id}";
		//_distributedCache.SetString(key, JsonConvert.SerializeObject(note));

		return note;
	}

	public bool Delete(int id)
	{
		var query = $"DELETE FROM tbl_note WHERE id = {id}";
		try
		{
			string key = $"label-{id}";
			string? cachedNote = _distributedCache.GetString(key);
			if (!string.IsNullOrEmpty(cachedNote)) _distributedCache.Remove(key);

			_session.Execute(query);

			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public int GetCurrentId()
	{
		throw new NotImplementedException();
	}
}
