using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_Sharp_Belt.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public List<UserActivity> JoinedActivities { get; set; }
 
        public User()
        {
            JoinedActivities = new List<UserActivity>();
        }
    }
}