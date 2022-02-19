using Letti.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Letti.Data.Configurations
{
    public class PersonOfInterestConfiguration : IEntityTypeConfiguration<PersonOfInterest>
    {
        public void Configure(EntityTypeBuilder<PersonOfInterest> builder)
        {
            builder.Ignore(c => c.FullName);
        }
    }
}
