using App.Domain.Core.CategoryAgg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infra.Data.Db.SqlServer.Ef.EntitiesConfig.CategoryAgg
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    Id = 1,
                    CreatedAt = new DateTime(2025, 10, 10),
                    Description = "کارهایی که مربوط به شخص شماست",
                    Title = "شخصی"
                },
                new Category
                {
                    Id = 2,
                    CreatedAt = new DateTime(2025, 10, 10),
                    Description = "کارهایی که مربوط به موارد دانشگاهی است",
                    Title = "دانشگاهی"
                },
                new Category
                {
                    Id = 3,
                    CreatedAt = new DateTime(2025, 10, 10),
                    Description = "کارهایی که مربوط به کار شماست",
                    Title = "کاری"
                },
                new Category
                {
                    Id = 4,
                    CreatedAt = new DateTime(2025, 10, 10),
                    Description = "سایر کارها",
                    Title = "سایر"
                }
           );
        }
    }
}
