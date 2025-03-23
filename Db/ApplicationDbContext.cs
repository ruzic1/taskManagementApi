using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Models;
using Task = TaskManagementAPI.Models.Task;

namespace TaskManagementAPI.Db
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        //public DbSet<TaskAssignment> TaskAssignments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Primer konfiguracije - postavljanje maksimalne duzine za ime
            //modelBuilder.Entity<User>()
            //    .Property(u => u.Name)
            //    .HasMaxLength(100);

            //modelBuilder.Entity<TaskAssignment>()
            //    .HasKey(t => t.Id);

            modelBuilder.Entity<TaskAssignment>()
                .HasOne(ta => ta.Task)
                .WithMany(t => t.TaskAssignments)
                .HasForeignKey(tc => tc.TaskId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<TaskAssignment>()
                .HasOne(ta => ta.User)
                .WithMany(t => t.TaskAssignments)
                .HasForeignKey(tc => tc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.Tasks)
            //    .WithOne(t => t.User)
            //    .HasForeignKey(t => t.UserId);

            #region UserModelBuilder

            //modelBuilder.Entity<User>()
            //    .HasMany(u => u.Tasks)
            //    .WithOne(t => t.User)
            //    .HasForeignKey(t => t.UserId)
            //    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Company)
                .WithMany(y => y.Users)
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .HasColumnType("nvarchar(30)")
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
                

            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .HasMaxLength(40)

                .HasColumnType("nvarchar(40)");

            modelBuilder.Entity<User>()
                .Property(u=>u.Role)
                .HasConversion(
            v => v.ToString(),      // Enum -> String pre upisa u bazu
            v => (UserRole)Enum.Parse(typeof(UserRole), v) // String -> Enum pri čitanju iz baze
        );

            #endregion

            #region TaskModelBuilder


            //modelBuilder.Entity<Task>()
            //    .HasOne(t => t.User)
            //    .WithMany(u => u.Tasks)
            //    .HasForeignKey(t => t.UserId);

            //modelBuilder.Entity<Task>()
            //    .HasOne(t => t.User)
            //    .WithMany(u => u.Tasks)
            //    .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Task>()
                .Property(t => t.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Task>()
                .Property(t => t.Title)
                .IsRequired(true);

            modelBuilder.Entity<Task>()
                .Property(x=>x.TaskPriority)
                .HasConversion(
                    v => v.ToString(),
                    v => (TaskPriority)Enum.Parse(typeof(TaskPriority), v));

            modelBuilder.Entity<Task>()
                .Property(t => t.TaskStatus)
                .HasConversion(
                    v => v.ToString(),
                    v => (TaskStatus)Enum.Parse(typeof(TaskStatus), v));

            #endregion


            #region CommentModelBuilder
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Task)
                .WithMany(t => t.Comments)
                .HasForeignKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(t => t.Comments)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
            #endregion

            #region CompanyModelBuilder
            //modelBuilder.Entity<Company>()


            #endregion
            //modelBuilder.Entity<Comment>()
            //    .HasOne(c=>c.Task)
            //    .WithMany(t=>t.Comments)
            //    .HasForeignKey(t => t.TaskId);

            //modelBuilder.Entity<Comment>()
            //    .HasOne(c => c.User)
            //    .WithMany(t => t.Comments)
            //    .HasForeignKey(t => t.UserId);

            //modelBuilder.Entity<Comment>()
            //    .Property(c => c.CreatedAt)
            //    .HasDefaultValue("GETUTCDATE()");

        }
    }
}
