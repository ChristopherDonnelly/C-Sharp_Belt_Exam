using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_Sharp_Belt.Models
{
    public class UserActivity
    {
        
        public int UserActivityId { get; set; }

        public int ActivityId { get; set; }
        public Activities ActivityInfo { get; set; }

        public int JoinedUserId { get; set; }
        public User JoinedUser { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}