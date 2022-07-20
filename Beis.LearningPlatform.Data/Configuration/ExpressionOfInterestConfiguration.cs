using Beis.LearningPlatform.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beis.LearningPlatform.Data.Configuration
{
    public class ExpressionOfInterestConfiguration : IEntityTypeConfiguration<ExpressionOfInterest>
    {
        public void Configure(EntityTypeBuilder<ExpressionOfInterest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}