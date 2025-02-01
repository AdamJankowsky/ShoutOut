using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoutOut.Database.Entities;

namespace ShoutOut.Database.Configuration;

public class UsersConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.Property(x => x.Email).HasMaxLength(150);

        builder.Property(x => x.Password).HasMaxLength(200);
        builder.Property(x => x.Salt).HasMaxLength(30);

        builder.Property(x => x.NickName).HasMaxLength(50);
    }
}