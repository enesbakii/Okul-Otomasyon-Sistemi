using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Data.Entites
{
	public class MessageEntity : BaseEntity //Mesajlar
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public int SenderUserId{ get; set; }
		public int ReceiverUserId { get; set; }
	}

	public class MessageEntityConfiguration : BaseConfiguration<MessageEntity>
	{
		public override void Configure(EntityTypeBuilder<MessageEntity> builder)
		{
           
            builder.Property(x => x.Title)
				.IsRequired()
				.HasMaxLength(30);

			builder.Property(x => x.Description)
				.IsRequired()
				.HasMaxLength(300);

			builder.Property(x => x.SenderUserId)
				.IsRequired();

			builder.Property(x => x.ReceiverUserId)
				.IsRequired();

			base.Configure(builder);
		}
	}
}
