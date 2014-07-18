using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace iBizProduct.DataContracts.Templates
{
    /// <summary>
    /// This allows for the the implementation of a Product Wiki. Pages Maintain a Git like 1 - * relationship
    /// with Revisions. This points to the Head/Revision that should be displayed. Virtual references must be
    /// implemented by your Model.
    /// </summary>
    /// <typeparam name="T">Should be Guid or Int depending on the standard of your Product DbContext</typeparam>
    public abstract class PageBase<T>
    {
        /// <summary>
        /// Id of the Pages class 
        /// </summary>
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public T PagesId { get; set; }

        /// <summary>
        /// By convention this should be the value /iBiz/Product/{ProductName}/Wiki/{Uri}
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// This is a Page Recursive value that points to the Wiki Page to redirect to. 
        /// </summary>
        public T RedirectId { get; set; }

        /// <summary>
        /// The Creator Id refers to an unspecified modelset that you can implement
        /// </summary>
        public T Creator { get; set; }

        /// <summary>
        /// The Editor Id refers to an unspecified modelset that you can implement.
        /// </summary>
        public T Editor { get; set; }

        /// <summary>
        /// When the Page was created.
        /// </summary>
        [DatabaseGenerated( DatabaseGeneratedOption.Computed )]
        public DateTime Created { get; set; }

        /// <summary>
        /// When the Page was last updated.
        /// </summary>
        public DateTime Updated { get; set; }
    }
}
