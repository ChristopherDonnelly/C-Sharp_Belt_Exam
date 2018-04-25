using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace C_Sharp_Belt.Models
{

    public class Activities
    {
        [Key]
        public int ActivityId { get; set; }

        [Display(Name = "Title: ")]
        [Required(ErrorMessage="Title is required!")]
        [MinLength(2, ErrorMessage = "Title must contain at least 2 characters!")]
        public string Title { get; set; }

        [Display(Name = "Description: ")]
        [Required(ErrorMessage="Description is required!")]
        [MinLength(10, ErrorMessage = "Description must contain at least 10 characters!")]
        public string Description { get; set; }

        [Display(Name = "Date: ")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage="Date is required!")]
        [CurrentDate(ErrorMessage = "Date must be a future date!")]
        public DateTime EventDate { get; set; } = DateTime.Now;

        [Display(Name = "Time: ")]
        [Required(ErrorMessage="Time is required!")]
        public DateTime EventTime { get; set; } =  DateTime.Parse(DateTime.Now.ToString("HH:mmtt"));

        [Display(Name = "Duration: ")]
        [Required(ErrorMessage="Duration is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "Duration must be a positive value greater than 0.")]
        public int Duration { get; set; }

        public string DurationLength { get; set; }

        public int CreatedById { get; set; }

        public User CreatedBy { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<UserActivity> JoinedUsers { get; set; }
 
        public Activities()
        {
            JoinedUsers = new List<UserActivity>();
        }

        public bool HasJoined(int userId){

            bool joined = false;

            foreach(UserActivity user in JoinedUsers){
                // if(user.JoinedUser != null){
                    if(user.JoinedUser.UserId == userId){
                        joined = true;
                    }
                // }
            }

            return joined;
        }

        public bool CanJoin(User user){

            bool canJoin = true;

            DateTime eventEndDate = getEndDate(EventDate, Duration, DurationLength);
            DateTime eventStartDate = EventDate;

            foreach(UserActivity joinedActivity in user.JoinedActivities){
                
                DateTime joinedEndDate = getEndDate(joinedActivity.ActivityInfo.EventDate, joinedActivity.ActivityInfo.Duration, joinedActivity.ActivityInfo.DurationLength);
                DateTime joinedStartDate = joinedActivity.ActivityInfo.EventDate;

                if((eventStartDate > joinedStartDate && eventStartDate < joinedEndDate) || (eventEndDate > joinedStartDate && eventEndDate < joinedEndDate) || (eventStartDate < joinedStartDate && eventEndDate > joinedEndDate)){
                    canJoin = false;
                    break;
                }
                
            }

            return canJoin;
        }

        public DateTime getEndDate(DateTime activityEventDate, int activityDuration, string length){
            DateTime endDate = new DateTime();

            if(length == "Days"){
                endDate = activityEventDate.AddDays(activityDuration);
            }else if(length == "Hours"){
                endDate = activityEventDate.AddHours(activityDuration);
            }else if(length == "Minutes"){
                endDate = activityEventDate.AddMinutes(activityDuration);
            }

            return endDate;
        }
    }

    public class CurrentDateAttribute : ValidationAttribute
    {
        public CurrentDateAttribute()
        {
        }

        public override bool IsValid(object value)
        {
            var dt = (DateTime)value;
            if(dt > DateTime.Now)
            {
                return true;
            }
            return false;
        }
    }
}