using App.Domain.Core.TaskAgg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace App.Infra.Data.Db.SqlServer.Ef.EntitiesConfig.TaskAgg
{
    public class TaskConfiguration : IEntityTypeConfiguration<MyTask>
    {
        public void Configure(EntityTypeBuilder<MyTask> builder)
        {
            builder.HasQueryFilter(t=>!t.IsDelete);
        }
    }
}
