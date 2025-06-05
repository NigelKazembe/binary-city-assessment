using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace bcity_assessment;

public partial class BcityAssessmentContext : DbContext
{
    public BcityAssessmentContext()
    {
    }

    public BcityAssessmentContext(DbContextOptions<BcityAssessmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("name=ConnectionStrings:ClientContactsDatabase", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.41-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("client_");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClientCode)
                .HasMaxLength(6)
                .HasColumnName("client_code");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name_");

            entity.HasMany(d => d.Contacts).WithMany(p => p.Clients)
                .UsingEntity<Dictionary<string, object>>(
                    "ClientContact",
                    r => r.HasOne<Contact>().WithMany()
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("client_contact_ibfk_2"),
                    l => l.HasOne<Client>().WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("client_contact_ibfk_1"),
                    j =>
                    {
                        j.HasKey("ClientId", "ContactId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("client_contact");
                        j.HasIndex(new[] { "ContactId" }, "fk_client_contact_contact");
                        j.IndexerProperty<int>("ClientId").HasColumnName("client_id");
                        j.IndexerProperty<int>("ContactId").HasColumnName("contact_id");
                    });
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("contact");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name_");
            entity.Property(e => e.Surname)
                .HasMaxLength(100)
                .HasColumnName("surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
