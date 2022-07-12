using Beis.LearningPlatform.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Beis.LearningPlatform.Data.Configuration
{
    public class ExpressionOfInterestConfiguration : IEntityTypeConfiguration<ExpressionOfInterestEntity>
    {
        public void Configure(EntityTypeBuilder<ExpressionOfInterestEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}