using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HonorSystem.sakila;

public partial class ZerodropContext : DbContext
{
    public ZerodropContext()
    {
    }

    public ZerodropContext(DbContextOptions<ZerodropContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Boss> Bosses { get; set; }

    public virtual DbSet<BossDaFare> BossDaFares { get; set; }

    public virtual DbSet<Classifica> Classificas { get; set; }

    public virtual DbSet<ClassificaConOggetti> ClassificaConOggettis { get; set; }

    public virtual DbSet<ClassificaWithId> ClassificaWithIds { get; set; }

    public virtual DbSet<Droppeditemsrequest> Droppeditemsrequests { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistories { get; set; }

    public virtual DbSet<Honorentry> Honorentries { get; set; }

    public virtual DbSet<Honorentrytype> Honorentrytypes { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemRequestedPerBoss> ItemRequestedPerBosses { get; set; }

    public virtual DbSet<Itemrequest> Itemrequests { get; set; }

    public virtual DbSet<Leftiteminguildstorage> Leftiteminguildstorages { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=zerodrop;Uid=evildog;Pwd=evildogs2024tal;");

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

        modelBuilder.Entity<BossDaFare>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("boss_da_fare");

            entity.Property(e => e.BossName)
                .HasMaxLength(45)
                .HasColumnName("bossName");
            entity.Property(e => e.NumeroRichieste).HasPrecision(42);
        });

        modelBuilder.Entity<Classifica>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("classifica");

            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.TotalePunti).HasPrecision(32);
        });

        modelBuilder.Entity<ClassificaConOggetti>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("classifica_con_oggetti");

            entity.Property(e => e.ItemName)
                .HasMaxLength(45)
                .HasColumnName("itemName");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.TotalePunti).HasPrecision(32);
        });

        modelBuilder.Entity<ClassificaWithId>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("classifica_with_ids");

            entity.Property(e => e.IdMember).HasColumnName("idMember");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.TotalePunti).HasPrecision(32);
        });

        modelBuilder.Entity<Droppeditemsrequest>(entity =>
        {
            entity.HasKey(e => e.IdDroppedItemsRequests).HasName("PRIMARY");

            entity.ToTable("droppeditemsrequests");

            entity.HasIndex(e => e.IdLeftItemInGuildStorage, "fk_droppeditemsrequests_leftiteminguildstorage1");

            entity.HasIndex(e => e.IdMember, "fk_droppeditemsrequests_members1_idx");

            entity.Property(e => e.IdDroppedItemsRequests).HasColumnName("idDroppedItemsRequests");
            entity.Property(e => e.IdLeftItemInGuildStorage).HasColumnName("idLeftItemInGuildStorage");
            entity.Property(e => e.IdMember).HasColumnName("idMember");
            entity.Property(e => e.Reason)
                .HasMaxLength(300)
                .HasColumnName("reason");
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("requestDate");

            entity.HasOne(d => d.IdLeftItemInGuildStorageNavigation).WithMany(p => p.Droppeditemsrequests)
                .HasForeignKey(d => d.IdLeftItemInGuildStorage)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_droppeditemsrequests_leftiteminguildstorage1");

            entity.HasOne(d => d.IdMemberNavigation).WithMany(p => p.Droppeditemsrequests)
                .HasForeignKey(d => d.IdMember)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_droppeditemsrequests_members1");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
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

            entity.Property(e => e.IdItem).HasColumnName("idItem");
            entity.Property(e => e.IdBoss).HasColumnName("idBoss");
            entity.Property(e => e.Tier).HasColumnName("tier");
            entity.Property(e => e.ItemName)
                .HasMaxLength(45)
                .HasColumnName("itemName");

            entity.HasOne(d => d.IdBossNavigation).WithMany(p => p.Items)
                .HasForeignKey(d => d.IdBoss)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BossId");
        });

        modelBuilder.Entity<ItemRequestedPerBoss>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("item_requested_per_boss");

            entity.Property(e => e.BossName)
                .HasMaxLength(45)
                .HasColumnName("bossName");
            entity.Property(e => e.ItemName)
                .HasMaxLength(45)
                .HasColumnName("itemName");
            entity.Property(e => e.TotaleRichieste).HasColumnName("totaleRichieste");
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

            entity.HasIndex(e => e.DistributedTo, "DropAssignee_idx");

            entity.HasIndex(e => e.IdHonorEntry, "HonorEtry_ItemLeftInStorage_idx");

            entity.HasIndex(e => e.IdItem, "ItemId_idx");

            entity.Property(e => e.DistributedDate).HasColumnType("datetime");
            entity.Property(e => e.DropDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.DistributedToNavigation).WithMany(p => p.Leftiteminguildstorages)
                .HasForeignKey(d => d.DistributedTo)
                .HasConstraintName("DropAssignee");

            entity.HasOne(d => d.IdHonorEntryNavigation).WithMany(p => p.Leftiteminguildstorages)
                .HasForeignKey(d => d.IdHonorEntry)
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
