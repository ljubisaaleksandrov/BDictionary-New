//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BDictionary.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuestionCreator
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public int QuestionID { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Question Question { get; set; }
    }
}
