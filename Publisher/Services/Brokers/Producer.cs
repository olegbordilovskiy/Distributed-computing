using Confluent.Kafka;
using DC_REST.DTOs.Request;
using DC_REST.DTOs.Response;
using DC_REST.Entities;
using DC_REST.Repositories;
using DC_REST.Services.Interfaces;
using DC_REST.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Publisher.Services.Brokers;

namespace DC_REST.Services.Brokers
{
	public class Producer : INoteService
	{
		private ConsumerConfig config = new ConsumerConfig
		{
			BootstrapServers = "localhost:9092",
			GroupId = "Kafka",
			AutoOffsetReset = AutoOffsetReset.Earliest
		};
		private readonly IDistributedCache _distributedCache;
		private readonly IValidator<NoteRequestTo> _noteValidator;
		private readonly IRepository<Issue> _issueRepository;
		public Producer(IDistributedCache distributedCache, IValidator<NoteRequestTo> noteValidator, IRepository<Issue> issueRepository)
		{
			_distributedCache = distributedCache;
			_noteValidator = noteValidator;
			_issueRepository = issueRepository;
		}

		public NoteResponseTo CreateNote(NoteRequestTo noteRequestTo)
		{
			if (!_noteValidator.Validate(noteRequestTo)) throw new ArgumentException("fail");
			if (_issueRepository.GetById(noteRequestTo.IssueId) == null) throw new ArgumentException("Invalid note data");
			var rand = new Random();
			noteRequestTo.Id = rand.Next();
			Produce(noteRequestTo.IssueId.ToString(), "create", JsonConvert.SerializeObject(noteRequestTo));
			var noteResponseDTO = new NoteResponseTo()
			{
				Content = noteRequestTo.Content,
				Id = noteRequestTo.Id,
				IssueId = noteRequestTo.IssueId,
			};
			string key = $"note-{noteResponseDTO.Id}";
			_distributedCache.SetString(key, JsonConvert.SerializeObject(noteResponseDTO));
			return noteResponseDTO;
		}

		public bool DeleteNote(int id)
		{
			var data = new Dictionary<string, int>(){
				{ "id", id }
			};
			Produce("extra", "delete", JsonConvert.SerializeObject(data));
			var resString = Consume();
			bool res = false;
			if (resString != null)
			{
				var temp = JsonConvert.DeserializeObject<Message>(resString);
				res = bool.Parse(temp.Data);
			}
			return res;
		}

		public NoteResponseTo GetNoteById(int id)
		{
			string key = $"note-{id}";
			string? cachedNote = _distributedCache.GetString(key);
			Note? note = null;
			NoteResponseTo? noteResponseDTO = null;

			if (string.IsNullOrEmpty(cachedNote))		
			{
				var data = new Dictionary<string, int>(){ { "id", id }};
				Produce("extra", "get", JsonConvert.SerializeObject(data));
				var resString = Consume();
				if (resString != null)
				{
					var temp = JsonConvert.DeserializeObject<Message>(resString);
					note = JsonConvert.DeserializeObject<Note>(temp.Data);
				}

				_distributedCache.SetString(key, JsonConvert.SerializeObject(note));
				noteResponseDTO = new NoteResponseTo() { Id = note.id, Content = note.Content, IssueId = note.IssueId };
				return noteResponseDTO;
			}

			note = JsonConvert.DeserializeObject<Note>(cachedNote);
			noteResponseDTO = new NoteResponseTo() { Id = note.id, Content = note.Content, IssueId = note.IssueId };
			return noteResponseDTO;
		}

		public List<NoteResponseTo> GetAllNotes()
		{
			Produce("test", "getAll", "");
			Thread.Sleep(5000);
			var resString = Consume();

			List<NoteResponseTo> res;
			if (resString != null)
			{
				var temp = JsonConvert.DeserializeObject<Message>(resString);
				res = JsonConvert.DeserializeObject<List<NoteResponseTo>>(temp.Data);

			}
			else
				throw new Exception("tested");
			return res;
		}

		public NoteResponseTo UpdateNote(int id, NoteRequestTo noteRequestDto)
		{
			if (!_noteValidator.Validate(noteRequestDto)) throw new ArgumentException("fail");
			var key = noteRequestDto.IssueId != null ? noteRequestDto.IssueId.ToString().ToString() : "extra";
			Produce(key, "update", JsonConvert.SerializeObject(noteRequestDto));
			var resString = Consume();
			NoteResponseTo noteResponseDTO;
			if (resString != null)
			{
				var temp = JsonConvert.DeserializeObject<Message>(resString);
				noteResponseDTO = JsonConvert.DeserializeObject<NoteResponseTo>(temp.Data);
				var note = new Note { id = noteResponseDTO.Id, IssueId = noteResponseDTO.IssueId, Content = noteResponseDTO.Content };
				_distributedCache.SetString($"note-{id}", JsonConvert.SerializeObject(note));
			}
			else
				throw new Exception("tested");
			return noteResponseDTO;
		}

		private string? Consume()
		{
			using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
			{
				consumer.Subscribe("OutTopic");
				try
				{
					var consumeResult = consumer.Consume();
					return consumeResult.Value;
				}
				finally
				{
					consumer.Close();
				}
			}
		}

		private void Produce(string key, string command, string data)
		{
			Message message = new Message();
			message.Command = command;
			message.Data = data;
			using (var producer = new ProducerBuilder<string, string>(config).Build())
			{
				producer.Produce("InTopic", new Message<string, string>
				{
					Value = JsonConvert.SerializeObject(message),
					Key = key,
				});
				producer.Flush();
			}
		}
	}
}