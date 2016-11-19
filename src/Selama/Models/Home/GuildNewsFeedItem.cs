using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Selama.Models.Home
{
    [Table("GuildNewsFeed")]
    public class GuildNewsFeedItem
    {
        #region Properties
        #region Public properties
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public string Content { get; set; }
        #endregion
        #endregion
    }
}