using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NsSchool.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Data.Entites
{
	public class UserEntity : BaseEntity
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public UserTypeEnum UserType { get; set; }

		//relational property

		public StudentEntity Student { get; set; }
		public PersonEntity Person { get; set; }
		public ParentEntity Parent { get; set; }
		public List<CartDetailEntity> CartDetails { get; set; }
		public class UserEntityConfiguraiton : BaseConfiguration<UserEntity> 
		{
			public override void Configure(EntityTypeBuilder<UserEntity> builder)
			{
				builder.Property(x => x.UserName).IsRequired();
				builder.Property(x=>x.Password).IsRequired();

				base.Configure(builder);
			}
		}
	}
}
