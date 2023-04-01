using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using NsSchool.Data.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NsSchool.Data.Entites.UserEntity;

namespace NsSchool.Data.Context
{
	public class NsSchoolDbContext :DbContext
	{
		private readonly IDataProtector _dataProtector;
		public NsSchoolDbContext(DbContextOptions<NsSchoolDbContext> options, IDataProtectionProvider dataProtectionProvider) : base(options)
		{
			_dataProtector = dataProtectionProvider.CreateProtector("security");
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
			modelBuilder.ApplyConfiguration(new AnnouncementEntityConfiguration());
			modelBuilder.ApplyConfiguration(new CategoryEntityConfiguration());
			modelBuilder.ApplyConfiguration(new ClassRoomEntitiyConfiguration());
			modelBuilder.ApplyConfiguration(new MessageEntityConfiguration());
			modelBuilder.ApplyConfiguration(new ParentEntityConfiguration());
			modelBuilder.ApplyConfiguration(new PersonEntityConfiguraiton());
			modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
			modelBuilder.ApplyConfiguration(new StudenEntityConfiguration());
			modelBuilder.ApplyConfiguration(new UserEntityConfiguraiton());


			//modelBuilder.Entity<ParentEntity>().Ignore("Id");
			//modelBuilder.Entity<ParentEntity>().HasKey("IdentityPerson");
			//modelBuilder.Entity<ParentEntity>().Property(x=>x.IdentityPerson).ValueGeneratedNever();

			//modelBuilder.Entity<StudentEntity>()
			//	.HasOne(x => x.Parent)
			//	.WithMany(x => x.Students)
			//	.HasPrincipalKey(x => x.Id)
			//	.HasForeignKey(x => x.ParentId)
			//	.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<StudentEntity>()
				.HasOne(x => x.User)
				.WithOne(x => x.Student)
				.HasForeignKey<StudentEntity>(x => x.UserId)
				.OnDelete(DeleteBehavior.Restrict);


			modelBuilder.Entity<ClassRoomStudentEntity>().Ignore("Id");
			modelBuilder.Entity<ClassRoomStudentEntity>().HasKey("StudentId", "ClassRoomId");

			modelBuilder.Entity<ClassRoomTeacherEntity>().Ignore("Id");
			modelBuilder.Entity<ClassRoomTeacherEntity>().HasKey("ClassRoomId", "PersonId");


			var encryptedPassword = _dataProtector.Protect("123");

			modelBuilder.Entity<PersonEntity>()
				.HasData(new PersonEntity()
				{
					Id = 1,
					Adress = "Ankara",
					BirthDay = DateTime.Now,
					BirthPlace = "Ankara",
					CreatedDate = DateTime.Now,
					FirstName = "Enes",
					Gender = true,
					IdentityPerson = "11111111111",
					ImagePath = "",
					IsDeleted = false,
					LastName = "Baki",
					Password = encryptedPassword,
					PhoneNumber = "5555555",
					UserName = "admin",
					UserType = Enums.UserTypeEnum.Admin,
					Email = "aaa@hotmail.com",
					UserId= 1,

				});

			modelBuilder.Entity<UserEntity>()
				.HasData(new UserEntity()
				{
					Id = 1,
					CreatedDate = DateTime.Now,
					IsDeleted = false,
					ModifiedDate = DateTime.Now,		
					UserName = "admin",
					Password = encryptedPassword,
					UserType = Enums.UserTypeEnum.Admin,
				});

			base.OnModelCreating(modelBuilder);
		}

		

		public DbSet<AnnouncementEntity> Announcements => Set<AnnouncementEntity>();
		public DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
		public DbSet<ClassRoomEntity> ClassRooms => Set<ClassRoomEntity>();
		public DbSet<ClassRoomStudentEntity> ClassRoomStudents => Set<ClassRoomStudentEntity>();
		public DbSet<ClassRoomTeacherEntity> ClassRoomTeachers => Set<ClassRoomTeacherEntity>();
		public DbSet<MessageEntity> Messages => Set<MessageEntity>();
		public DbSet<ParentEntity> Parents => Set<ParentEntity>();
		public DbSet<PersonEntity> Persons => Set<PersonEntity>();
		public DbSet<ProductEntity> Products => Set<ProductEntity>();
		public DbSet<StudentEntity> Students => Set<StudentEntity>();
		public DbSet<UserEntity> Users => Set<UserEntity>();
		public DbSet<CartDetailEntity> CartDetails { get; set; }


	}
}
