using DatabaseEntity.CodeFirst.Entity.UserData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntity.CodeFirst.Configuration.UserData
{
    internal class UserDetailConfigurator : IEntityTypeConfiguration<UserDetail>
    {
        public void Configure(EntityTypeBuilder<UserDetail> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.HasOne(p => p.Users)
                .WithOne(p => p.UserDetail)
                .HasForeignKey<UserDetail>(p => p.Id_User);
        }
    }
}
