using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HonorSystem.sakila;

public partial class EvildogsContext : DbContext
{
    public EvildogsContext()
    {
    }

    public EvildogsContext(DbContextOptions<EvildogsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Boss> Bosses { get; set; }

    public virtual DbSet<Honorentry> Honorentries { get; set; }

    public virtual DbSet<Honorentrytype> Honorentrytypes { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Itemrequest> Itemrequests { get; set; }

    public virtual DbSet<Leftiteminguildstorage> Leftiteminguildstorages { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=evildogs;Uid=evildog;Pwd=evildogs2024tal;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Boss>(entity =>
        {
            entity.HasKey(e => e.IdBoss).HasName("PRIMARY");

            entity.ToTable("boss");

            entity.Property(e => e.IdBoss).HasColumnName("idBoss");
            entity.Property(e => e.BossName)
                .HasMaxLength(45)
                .HasColumnName("bossName");
            entity.Property(e => e.IsArch).HasColumnName("isArch");
            entity.Property(e => e.IsField).HasColumnName("isField");
            entity.Property(e => e.IsGuild).HasColumnName("isGuild");
        });

        modelBuilder.Entity<Honorentry>(entity =>
        {
            entity.HasKey(e => e.IdHonorEntry).HasName("PRIMARY");

            entity.ToTable("honorentry");

            entity.HasIndex(e => e.PlayerId, "MemberId_idx");

            entity.HasIndex(e => e.HonorEntryTypeId, "_idx");

            entity.Property(e => e.IdHonorEntry).HasColumnName("idHonorEntry");
            entity.Property(e => e.AssignedPoints).HasComment("defaults to the related type default number.");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.EntryDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

            entity.HasOne(d => d.HonorEntryType).WithMany(p => p.Honorentries).HasForeignKey(d => d.HonorEntryTypeId);

            entity.HasOne(d => d.Player).WithMany(p => p.Honorentries)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MemberId");
        });

        modelBuilder.Entity<Honorentrytype>(entity =>
        {
            entity.HasKey(e => e.IdHonorEntryType).HasName("PRIMARY");

            entity.ToTable("honorentrytype");

            entity.HasIndex(e => e.Type, "Type_UNIQUE").IsUnique();

            entity.Property(e => e.IdHonorEntryType).HasColumnName("idHonorEntryType");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.Type).HasMaxLength(45);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.IdItem).HasName("PRIMARY");

            entity.ToTable("item");

            entity.HasIndex(e => e.IdBoss, "BossId_idx");

            entity.HasIndex(e => e.ItemName, "itemName_UNIQUE").IsUnique();

            entity.Property(e => e.IdItem).HasColumnName("idItem");
            entity.Property(e => e.IdBoss).HasColumnName("idBoss");
            entity.Property(e => e.ItemName)
                .HasMaxLength(45)
                .HasColumnName("itemName");

            entity.HasOne(d => d.IdBossNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.IdBoss)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BossId");
        });

        modelBuilder.Entity<Itemrequest>(entity =>
        {
            entity.HasKey(e => e.IdItemRequest).HasName("PRIMARY");

            entity.ToTable("itemrequest");

            entity.HasIndex(e => e.ItemId, "request_itemId_idx");

            entity.HasIndex(e => e.PlayerId, "request_playerID_idx");

            entity.Property(e => e.IdItemRequest).HasColumnName("idItemRequest");
            entity.Property(e => e.IsSupplied).HasColumnName("isSupplied");
            entity.Property(e => e.ItemId).HasColumnName("itemId");
            entity.Property(e => e.PlayerId).HasColumnName("playerId");

            entity.HasOne(d => d.Item).WithMany(p => p.Itemrequests)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("request_itemId");

            entity.HasOne(d => d.Player).WithMany(p => p.Itemrequests)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("request_playerId");
        });

        modelBuilder.Entity<Leftiteminguildstorage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("leftiteminguildstorage");

            entity.HasIndex(e => e.IdHonorEntry, "HonorEtry_ItemLeftInStorage_idx");

            entity.HasIndex(e => e.IdItem, "ItemId_idx");

            entity.Property(e => e.DropDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdHonorEntryNavigation).WithMany(p => p.Leftiteminguildstorages)
                .HasForeignKey(d => d.IdHonorEntry)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("HonorEtry_ItemLeftInStorage");

            entity.HasOne(d => d.IdItemNavigation).WithMany(p => p.Leftiteminguildstorages)
                .HasForeignKey(d => d.IdItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ItemId");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.IdMembers).HasName("PRIMARY");

            entity.ToTable("members");

            entity.HasIndex(e => e.CharacterName, "character_name_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.IdMembers).HasColumnName("idMembers");
            entity.Property(e => e.CharacterName)
                .HasMaxLength(45)
                .HasColumnName("character_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isActive");
            entity.Property(e => e.IsStillInGuild)
                .HasDefaultValueSql("'1'")
                .HasColumnName("isStillInGuild");
            entity.Property(e => e.JoinDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.SecondaryCharacterName)
                .HasMaxLength(45)
                .HasColumnName("secondary_character_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
