//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace student_enquiry.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class enquiry
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public string EMAIL { get; set; }
        public string NUMBER { get; set; }
        public string REFERENCE { get; set; }
        public string GENDER { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<int> COURSE_ENQUIRY { get; set; }
        public string IN_TIME { get; set; }
        public string OUT_TIME { get; set; }
        public Nullable<System.DateTime> JOINING_DATE { get; set; }
        public Nullable<System.DateTime> END_DATE { get; set; }
        public string TESTIMONIAL { get; set; }
        public string FEES { get; set; }
        public string REMAINING_FEES { get; set; }
        public Nullable<bool> IS_DELETED { get; set; }
    
        public virtual cours cours { get; set; }
    }
}
