﻿using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QuizBlazorApp.Server.Models;
using QuizBlazorProject.Server.Models;

namespace QuizBlazorApp.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<QuizGame> QuizGames { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizQuestionAnswer> QuizQuestionAnswers { get; set; }
        public DbSet<QuizResult> QuizResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuizQuestion>()
                .HasOne(p => p.QuizGame)
                .WithMany(p => p.QuizQuestions)
                .HasForeignKey(p => p.FKQuizGameId);

            modelBuilder.Entity<QuizQuestionAnswer>()
                .HasOne(p => p.Question)
                .WithMany(p => p.Answers)
                .HasForeignKey(p => p.FKQuestionId);

            modelBuilder.Entity<QuizResult>()
                .HasMany(qr => qr.ResultAnswers)
                .WithOne(qra => qra.QuizResult)
                .HasForeignKey(qra => qra.QuizResultId);

            base.OnModelCreating(modelBuilder);
        }
    }
}