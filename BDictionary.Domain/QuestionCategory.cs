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
    
    public partial class QuestionCategory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QuestionCategory()
        {
            this.Questions = new HashSet<Question>();
            this.QuestionAnswers = new HashSet<QuestionAnswer>();
            this.QuestionCategory1 = new HashSet<QuestionCategory>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int AnswerType { get; set; }
        public Nullable<int> ParentID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionCategory> QuestionCategory1 { get; set; }
        public virtual QuestionCategory QuestionCategory2 { get; set; }
    }
}
