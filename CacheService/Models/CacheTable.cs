using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CacheService.Models
{
    [Table("AspNet_SqlCacheTablesForChangeNotification")]
    public class TableVersion
    {
        [Key]
        public string TableName { get; set; }
        public DateTime NotificationCreated { get; set; }
        public int ChangeId { get; set; }
    }
}
