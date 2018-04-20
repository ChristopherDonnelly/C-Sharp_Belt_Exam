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


        // Update to references to joined tables in DB - Many to Many relationship
        // This specifically refers to a join table
        // public List<WeddingPlan> Plans { get; set; }
 
        // public User()
        // {
        //     Plans = new List<WeddingPlan>();
        // }
    }
}