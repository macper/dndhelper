using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DnDHelper.UpdateService
{
    public class DataContext : DbContext
    {
        public DbSet<DnDRepository> Repositories { get; set; }
        public DbSet<DnDEntity> Entities { get; set; }
        public DbSet<DnDSetting> Settings { get; set; }

        public DataContext() : base("DNDConnection")
        {
        }
    }
    
    public class DnDSetting
    {
        public string Id { get; set; }

        public string StringValue { get; set; }

        public int? IntValue { get; set; }

        public long? LongValue { get; set; }
    }

    public class DnDRepository
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<DnDEntity> DnDEntities { get; set; }
    }

    public class DnDEntity
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public long LastChange { get; set; }

        public int DnDRepositoryId { get; set; }
        public virtual DnDRepository DnDRepository { get; set; }

        public bool? Deleted { get; set; }
    }
}