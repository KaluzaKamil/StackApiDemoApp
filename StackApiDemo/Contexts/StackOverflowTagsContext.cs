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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tag>()
                .HasOne(t => t.TagsImport)
                .WithMany(ti => ti.items)
                .HasForeignKey(t => t.TagsImportId)
                .IsRequired();

            modelBuilder.Entity<Collective>()
                .HasOne(c => c.Tag)
                .WithMany(t => t.collectives)
                .HasForeignKey(c => c.TagId)
                .IsRequired();

            modelBuilder.Entity<ExternalLink>()
                .HasOne(el => el.Collective)
                .WithMany(c => c.external_links)
                .HasForeignKey(el => el.CollectiveId)
                .IsRequired();
        }        
    }
}
