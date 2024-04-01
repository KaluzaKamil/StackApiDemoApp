using Microsoft.EntityFrameworkCore;
using StackApiDemo.Models.TagsModels;

namespace StackApiDemo.Contexts
{
    public class StackOverflowTagsContext : DbContext
    {
        public StackOverflowTagsContext(DbContextOptions<StackOverflowTagsContext> options) : base(options)
        {

        }

        public DbSet<TagsImport> TagsImports { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Collective> Collectives { get; set; }
        public DbSet<ExternalLink> ExternalLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TagsImport>()
                .HasMany(e => e.items)
                .WithOne(e => e.TagsImport)
                .HasForeignKey(e => e.TagsImportId)
                .IsRequired();

            modelBuilder.Entity<Tag>()
                .HasMany(e => e.collectives)
                .WithOne(e => e.Tag)
                .HasForeignKey(e => e.TagId)
                .IsRequired(false);

            modelBuilder.Entity<Collective>()
                .HasMany(e => e.external_links)
                .WithOne(e => e.Collective)
                .HasForeignKey(e => e.CollectiveId)
                .IsRequired();
        }        
    }
}
