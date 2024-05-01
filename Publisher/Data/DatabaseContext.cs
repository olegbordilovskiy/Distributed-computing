using DC_REST.Entities;
using Microsoft.EntityFrameworkCore;

namespace DC_REST.Data
{
	public class DatabaseContext : DbContext
	{
		public DbSet<Issue> tbl_issue { get; set; }
		public DbSet<Label> tbl_label { get; set; }
		public DbSet<Note> tbl_note { get; set; }
		public DbSet<User> tbl_user { get; set; }

		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Issue>().HasKey(i => i.id);
			modelBuilder.Entity<Issue>().HasIndex(i => i.Title).IsUnique();
			modelBuilder.Entity<Issue>().HasOne(i => i.User).WithMany(u => u.Issues).HasForeignKey(i => i.UserId);
				
			modelBuilder.Entity<Note>().HasKey(n => n.id);
			modelBuilder.Entity<Note>().HasOne(n => n.Issue).WithMany(i => i.Notes).HasForeignKey(n => n.IssueId);

			modelBuilder.Entity<User>().HasKey(u => u.id);
			modelBuilder.Entity<User>().HasIndex(i => i.login).IsUnique();

			modelBuilder.Entity<Issue_Label>().HasKey(il => il.id);
			modelBuilder.Entity<Issue_Label>().HasOne(il => il.Label).WithMany(l => l.Issue_Labels).HasForeignKey(il => il.LabelId);
			modelBuilder.Entity<Issue_Label>().HasOne(il => il.Issue).WithMany(i => i.Issue_Labels).HasForeignKey(il => il.IssueId);

			modelBuilder.Entity<Label>().HasKey(l => l.id);
			modelBuilder.Entity<Label>().HasIndex(i => i.Name).IsUnique();

		}
	}
}
