using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DatabaseLibrary;

public partial class KnowledgeBaseContext : DbContext
{
    public KnowledgeBaseContext()
    {
    }

    public KnowledgeBaseContext(DbContextOptions<KnowledgeBaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Soft> Softs { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=KnowledgeBase;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("Answers_pkey");

            entity.Property(e => e.RowId).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.Value).HasColumnType("character varying");
        });

        modelBuilder.Entity<Soft>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("Systems_pkey");

            entity.Property(e => e.RowId).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.Value).HasColumnType("character varying");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.RowId).HasName("Tags_pkey");

            entity.Property(e => e.RowId).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.Value).HasColumnType("character varying");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
