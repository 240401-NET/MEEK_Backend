using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PokemonTeamBuilder.API.Model;

namespace PokemonTeamBuilder.API.DB;

public partial class PokemonTrainerDbContext : DbContext
{
    public PokemonTrainerDbContext()
    {
    }

    public PokemonTrainerDbContext(DbContextOptions<PokemonTrainerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ItemPokeApi> ItemPokeApis { get; set; }

    public virtual DbSet<PokemonAbility> PokemonAbilities { get; set; }

    public virtual DbSet<PokemonBaseStat> PokemonBaseStats { get; set; }

    public virtual DbSet<PokemonMove> PokemonMoves { get; set; }

    public virtual DbSet<PokemonMoveSet> PokemonMoveSets { get; set; }

    public virtual DbSet<PokemonPokeApi> PokemonPokeApis { get; set; }

    public virtual DbSet<PokemonSprite> PokemonSprites { get; set; }

    public virtual DbSet<PokemonStat> PokemonStats { get; set; }

    public virtual DbSet<PokemonTeam> PokemonTeams { get; set; }

    public virtual DbSet<PokemonTeamMember> PokemonTeamMembers { get; set; }

    public virtual DbSet<PokemonType> PokemonTypes { get; set; }

    public virtual DbSet<Trainer> Trainers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ItemPokeApi>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.ToTable("Item_PokeAPI", "PokemonTeamBuilder");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Sprite).HasMaxLength(255);
        });

        modelBuilder.Entity<PokemonAbility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC273C1B459E");

            entity.ToTable("PokemonAbility", "PokemonTeamBuilder");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<PokemonBaseStat>(entity =>
        {
            entity.ToTable("PokemonBaseStat", "PokemonTeamBuilder");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PkmApiId).HasColumnName("PKM_API_ID");
            entity.Property(e => e.Url).HasColumnName("URL");

            entity.HasOne(d => d.PkmApi).WithMany(p => p.PokemonBaseStats)
                .HasForeignKey(d => d.PkmApiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PokemonBaseStat_Pokemon_PokeAPI");
        });

        modelBuilder.Entity<PokemonMove>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC273D048BD9");

            entity.ToTable("PokemonMove", "PokemonTeamBuilder");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .HasColumnName("URL");

            entity.HasMany(d => d.PkmApis).WithMany(p => p.Moves)
                .UsingEntity<Dictionary<string, object>>(
                    "PkmapiMoveJunc",
                    r => r.HasOne<PokemonPokeApi>().WithMany()
                        .HasForeignKey("PkmApiId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PKMAPI_Move_Junc_Pokemon_PokeAPI"),
                    l => l.HasOne<PokemonMove>().WithMany()
                        .HasForeignKey("MoveId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PKMAPI_Move_Junc_PokemonMove"),
                    j =>
                    {
                        j.HasKey("MoveId", "PkmApiId")
                            .HasName("PokemonToMove")
                            .IsClustered(false);
                        j.ToTable("PKMAPI_Move_Junc", "PokemonTeamBuilder");
                        j.IndexerProperty<int>("MoveId").HasColumnName("MoveID");
                        j.IndexerProperty<int>("PkmApiId").HasColumnName("PKM_API_ID");
                    });
        });

        modelBuilder.Entity<PokemonMoveSet>(entity =>
        {
            entity.HasKey(e => e.PkmTmId);

            entity.ToTable("PokemonMoveSet", "PokemonTeamBuilder");

            entity.Property(e => e.PkmTmId)
                .ValueGeneratedNever()
                .HasColumnName("PKM_TM_ID");
            entity.Property(e => e.Move1)
                .HasMaxLength(50)
                .HasColumnName("move_1");
            entity.Property(e => e.Move2)
                .HasMaxLength(50)
                .HasColumnName("move_2");
            entity.Property(e => e.Move3)
                .HasMaxLength(50)
                .HasColumnName("move_3");
            entity.Property(e => e.Move4)
                .HasMaxLength(50)
                .HasColumnName("move_4");

            entity.HasOne(d => d.PkmTm).WithOne(p => p.PokemonMoveSet)
                .HasForeignKey<PokemonMoveSet>(d => d.PkmTmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PokemonMoveSet_Pokemon_TeamMember");
        });

        modelBuilder.Entity<PokemonPokeApi>(entity =>
        {
            entity.HasKey(e => e.Id)
                .HasName("PK__Pokemon___3214EC26604EAF0F")
                .IsClustered(false);

            entity.ToTable("Pokemon_PokeAPI", "PokemonTeamBuilder");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(30);

            entity.HasMany(d => d.Abilities).WithMany(p => p.PkmApis)
                .UsingEntity<Dictionary<string, object>>(
                    "PkmapiAbilityJunc",
                    r => r.HasOne<PokemonAbility>().WithMany()
                        .HasForeignKey("AbilityId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PKMAPI_Ability_Junc_PokemonAbility"),
                    l => l.HasOne<PokemonPokeApi>().WithMany()
                        .HasForeignKey("PkmApiId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PKMAPI_Ability_Junc_Pokemon_PokeAP"),
                    j =>
                    {
                        j.HasKey("PkmApiId", "AbilityId")
                            .HasName("PokmeonToAbility")
                            .IsClustered(false);
                        j.ToTable("PKMAPI_Ability_Junc", "PokemonTeamBuilder");
                        j.IndexerProperty<int>("PkmApiId").HasColumnName("PKM_API_ID");
                        j.IndexerProperty<int>("AbilityId").HasColumnName("AbilityID");
                    });

            entity.HasMany(d => d.Types).WithMany(p => p.PkmApis)
                .UsingEntity<Dictionary<string, object>>(
                    "PkmapiTypeJunc",
                    r => r.HasOne<PokemonType>().WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PKMAPI_Type_Junc_PokemonType"),
                    l => l.HasOne<PokemonPokeApi>().WithMany()
                        .HasForeignKey("PkmApiId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_PKMAPI_Type_Junc_Pokemon_PokeAPI"),
                    j =>
                    {
                        j.HasKey("PkmApiId", "TypeId")
                            .HasName("PokemonToType")
                            .IsClustered(false);
                        j.ToTable("PKMAPI_Type_Junc", "PokemonTeamBuilder");
                        j.IndexerProperty<int>("PkmApiId").HasColumnName("PKM_API_ID");
                        j.IndexerProperty<int>("TypeId").HasColumnName("TypeID");
                    });
        });

        modelBuilder.Entity<PokemonSprite>(entity =>
        {
            entity.HasKey(e => e.PkmApiId);

            entity.ToTable("PokemonSprite", "PokemonTeamBuilder");

            entity.Property(e => e.PkmApiId)
                .ValueGeneratedNever()
                .HasColumnName("PKM_API_ID");
            entity.Property(e => e.FrontDefault).HasMaxLength(255);
            entity.Property(e => e.FrontFemale).HasMaxLength(255);
            entity.Property(e => e.FrontShiny).HasMaxLength(255);
            entity.Property(e => e.FrontShinyFemale).HasMaxLength(255);

            entity.HasOne(d => d.PkmApi).WithOne(p => p.PokemonSprite)
                .HasForeignKey<PokemonSprite>(d => d.PkmApiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PokemonID_Sprite");
        });

        modelBuilder.Entity<PokemonStat>(entity =>
        {
            entity.ToTable("PokemonStat", "PokemonTeamBuilder");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PkmTmId).HasColumnName("PKM_TM_ID");
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .HasColumnName("URL");

            entity.HasOne(d => d.PkmTm).WithMany(p => p.PokemonStats)
                .HasForeignKey(d => d.PkmTmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PokemonStat_Pokemon_TeamMember");
        });

        modelBuilder.Entity<PokemonTeam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PokemonT__3214EC27AE892B21");

            entity.ToTable("PokemonTeam", "PokemonTeamBuilder");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.TrainerId).HasColumnName("TrainerID");

            entity.HasOne(d => d.Trainer).WithMany(p => p.PokemonTeams)
                .HasForeignKey(d => d.TrainerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TrainerID_Team");
        });

        modelBuilder.Entity<PokemonTeamMember>(entity =>
        {
            entity.ToTable("Pokemon_TeamMember", "PokemonTeamBuilder");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.ChosenAbilityId).HasColumnName("ChosenAbilityID");
            entity.Property(e => e.HeldItemId).HasColumnName("HeldItemID");
            entity.Property(e => e.Nature).HasMaxLength(50);
            entity.Property(e => e.NickName).HasMaxLength(50);
            entity.Property(e => e.PkmApiId).HasColumnName("PKM_API_ID");
            entity.Property(e => e.PokemonTeamId).HasColumnName("PokemonTeamID");
            entity.Property(e => e.TeraType).HasMaxLength(50);

            entity.HasOne(d => d.ChosenAbility).WithMany(p => p.PokemonTeamMembers)
                .HasForeignKey(d => d.ChosenAbilityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pokemon_TeamMember_PokemonAbility");

            entity.HasOne(d => d.HeldItem).WithMany(p => p.PokemonTeamMembers)
                .HasForeignKey(d => d.HeldItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pokemon_TeamMember_Item_PokeAPI");

            entity.HasOne(d => d.PkmApi).WithMany(p => p.PokemonTeamMembers)
                .HasForeignKey(d => d.PkmApiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pokemon_TeamMember_Pokemon_PokeAPI");

            entity.HasOne(d => d.PokemonTeam).WithMany(p => p.PokemonTeamMembers)
                .HasForeignKey(d => d.PokemonTeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pokemon_TeamMember_PokemonTeam");
        });

        modelBuilder.Entity<PokemonType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC27C57EA4A8");

            entity.ToTable("PokemonType", "PokemonTeamBuilder");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Url).HasMaxLength(50);
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC27D0BCCEE9");

            entity.ToTable("Trainer", "PokemonTeamBuilder");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name)
                .HasMaxLength(26)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
