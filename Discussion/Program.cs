using Discussion.DTOs.Request;
using Discussion.Entities;
using Discussion.Repositories;
using Discussion.Services;
using Discussion.Services.Brokers;
using Discussion.Services.Interfaces;
using Discussion.Services.Mappers;
using Discussion.Validators;

namespace Discussion
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			builder.WebHost.UseUrls("http://localhost:24130");

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddTransient<IRepository<Note>, NoteDbRepository>();
			builder.Services.AddTransient<IValidator<NoteRequestTo>, NoteValidator>();
			builder.Services.AddTransient<INoteService, NoteService>();
			//builder.Services.AddTransient<INoteService, Consumer>();

			builder.Services.AddTransient<IConsumer,Consumer>();

			var cassandraContactPoints = "127.0.0.1"; 
			var cassandraKeyspace = "distcomp"; 
			var cassandraConnector = new CassandraConnector(cassandraContactPoints, cassandraKeyspace);
			var session = cassandraConnector.GetSession();
			builder.Services.AddSingleton(session);

			builder.Services.AddStackExchangeRedisCache(options =>
			{
				options.Configuration = builder.Configuration.GetConnectionString("RedisConnection");
			});

			builder.Services.AddAutoMapper(typeof(NoteMapper));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();

			app.MapControllers();


			var kafka = app.Services.GetService<IConsumer>();
			Thread kafkaThread = new Thread(kafka.StartConsuming);
			kafkaThread.Start();


			app.Run();
		}
	}
}
