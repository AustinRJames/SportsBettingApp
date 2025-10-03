using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyBackend.Models;

namespace MyBackend.Data;

public partial class SportsDbContext : DbContext
{
    public SportsDbContext()
    {
    }

    public SportsDbContext(DbContextOptions<SportsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Prediction> Predictions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("matches_pkey");

            entity.ToTable("matches");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.ScoreA).HasColumnName("score_a");
            entity.Property(e => e.ScoreB).HasColumnName("score_b");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'scheduled'::text")
                .HasColumnName("status");
            entity.Property(e => e.TeamA)
                .HasDefaultValueSql("0")
                .HasColumnName("team_a");
            entity.Property(e => e.TeamB)
                .HasDefaultValueSql("0")
                .HasColumnName("team_b");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Prediction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("predictions_pkey");

            entity.ToTable("predictions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.IsSettled)
                .HasDefaultValue(false)
                .HasColumnName("is_settled");
            entity.Property(e => e.IsWin)
                .HasDefaultValue(false)
                .HasColumnName("is_win");
            entity.Property(e => e.MatchId).HasColumnName("match_id");
            entity.Property(e => e.PointsBet)
                .HasDefaultValue(0)
                .HasColumnName("points_bet");
            entity.Property(e => e.ScoreA)
                .HasDefaultValue(0)
                .HasColumnName("score_a");
            entity.Property(e => e.ScoreB)
                .HasDefaultValue(0)
                .HasColumnName("score_b");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Match).WithMany(p => p.Predictions)
                .HasForeignKey(d => d.MatchId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("predictions_match_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Predictions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("predictions_user_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasColumnName("last_name");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Streak)
                .HasDefaultValue(0)
                .HasColumnName("streak");
            entity.Property(e => e.TotalPoints)
                .HasDefaultValue(0)
                .HasColumnName("total_points");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}