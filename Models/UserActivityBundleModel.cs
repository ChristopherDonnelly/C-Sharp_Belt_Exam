using System.Collections.Generic;

namespace C_Sharp_Belt.Models
{
    public class UserActivityBundleModel : BaseEntity
    {
        public User User  { get; set; }
        public List<Activities> Activities { get; set; }

        public Activities Activity { get; set; }
    }
}