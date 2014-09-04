using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace iBizProduct.Models.Templates
{
    /// <summary>
    /// This provides a base class for creating a Product Wiki. A particular Revision should be
    /// displayed for a corresponding page, with administrators being able to view all Revisions for
    /// a particular Page. This gives you a 1 - * relationship between Pages and Revisions, and allows
    /// for a Git like Head/Revision mapping.
    /// </summary>
    /// <typeparam name="T">Should be Guid or Int depending on the standard of your Product DbContext</typeparam>
    public class RevisionBase<T>
    {
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public T RevisionsId { get; set; }

        public T PageId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        [DatabaseGenerated( DatabaseGeneratedOption.Computed )]
        public DateTime Created { get; set; }

        public T Creator { get; set; }

        //public abstract PageBase<T> Page { get; set; }
    }
}
