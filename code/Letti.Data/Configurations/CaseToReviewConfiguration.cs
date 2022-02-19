using Letti.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Letti.Data.Configurations
{
    public class CaseToReviewConfiguration : IEntityTypeConfiguration<CaseToReview>
    {
        public void Configure(EntityTypeBuilder<CaseToReview> builder)
        {
            builder.Ignore(c => c.Contractors);
            builder.HasMany(c => c.SuspiciousRelationships).WithOne(x => x.CaseToReview);
        }
    }
}
