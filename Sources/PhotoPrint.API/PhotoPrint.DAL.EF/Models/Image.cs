using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class Image
    {
        public Image()
        {
            ImageCategories = new HashSet<ImageCategory>();
            ImageRelatedImages = new HashSet<ImageRelated>();
            ImageRelatedRelatedImages = new HashSet<ImageRelated>();
            ImageThumbnails = new HashSet<ImageThumbnail>();
            OrderItems = new HashSet<OrderItem>();
        }

        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string OriginUrl { get; set; }
        public int? MaxWidth { get; set; }
        public int? MaxHeight { get; set; }
        public decimal? PriceAmount { get; set; }
        public long? PriceCurrencyId { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedByID { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedByID { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual User CreatedBy { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual Currency PriceCurrency { get; set; }
        public virtual ICollection<ImageCategory> ImageCategories { get; set; }
        public virtual ICollection<ImageRelated> ImageRelatedImages { get; set; }
        public virtual ICollection<ImageRelated> ImageRelatedRelatedImages { get; set; }
        public virtual ICollection<ImageThumbnail> ImageThumbnails { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
