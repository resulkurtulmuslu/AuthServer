﻿using Core.AuthServer.CoreLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.AuthServer.DataLayer.Configurations
{
    public class UserRefreshTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.Code).IsRequired().HasMaxLength(200);
        }
    }
}
