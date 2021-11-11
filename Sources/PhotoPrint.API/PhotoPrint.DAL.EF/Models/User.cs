using System;
using System.Collections.Generic;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class User
    {
        public User()
        {
            AddressCreatedBies = new HashSet<Address>();
            AddressModifiedBies = new HashSet<Address>();
            CategoryCreatedBies = new HashSet<Category>();
            CategoryModifiedBies = new HashSet<Category>();
            ContactCreatedBies = new HashSet<Contact>();
            ContactModifiedBies = new HashSet<Contact>();
            DeliveryServiceCreatedBies = new HashSet<DeliveryService>();
            DeliveryServiceModifiedBies = new HashSet<DeliveryService>();
            FrameTypeCreatedBies = new HashSet<FrameType>();
            FrameTypeModifiedBies = new HashSet<FrameType>();
            ImageCreatedBies = new HashSet<Image>();
            ImageModifiedBies = new HashSet<Image>();
            InverseModifiedBy = new HashSet<User>();
            MatCreatedBies = new HashSet<Mat>();
            MatModifiedBies = new HashSet<Mat>();
            MaterialTypeCreatedBies = new HashSet<MaterialType>();
            MaterialTypeModifiedBies = new HashSet<MaterialType>();
            OrderCreatedBies = new HashSet<Order>();
            OrderItemCreatedBies = new HashSet<OrderItem>();
            OrderItemModifiedBies = new HashSet<OrderItem>();
            OrderManagers = new HashSet<Order>();
            OrderModifiedBies = new HashSet<Order>();
            OrderPaymentDetailCreatedBies = new HashSet<OrderPaymentDetail>();
            OrderPaymentDetailModifiedBies = new HashSet<OrderPaymentDetail>();
            OrderTrackings = new HashSet<OrderTracking>();
            OrderUsers = new HashSet<Order>();
            PrintingHouseCreatedBies = new HashSet<PrintingHouse>();
            PrintingHouseModifiedBies = new HashSet<PrintingHouse>();
            SizeCreatedBies = new HashSet<Size>();
            SizeModifiedBies = new HashSet<Size>();
            UserAddresses = new HashSet<UserAddress>();
            UserConfirmations = new HashSet<UserConfirmation>();
            UserContacts = new HashSet<UserContact>();
        }

        public long ID { get; set; }
        public string Login { get; set; }
        public string PwdHash { get; set; }
        public string Salt { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string FriendlyName { get; set; }
        public long UserStatusID { get; set; }
        public long UserTypeID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedByID { get; set; }

        public virtual User ModifiedBy { get; set; }
        public virtual UserStatus UserStatus { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<Address> AddressCreatedBies { get; set; }
        public virtual ICollection<Address> AddressModifiedBies { get; set; }
        public virtual ICollection<Category> CategoryCreatedBies { get; set; }
        public virtual ICollection<Category> CategoryModifiedBies { get; set; }
        public virtual ICollection<Contact> ContactCreatedBies { get; set; }
        public virtual ICollection<Contact> ContactModifiedBies { get; set; }
        public virtual ICollection<DeliveryService> DeliveryServiceCreatedBies { get; set; }
        public virtual ICollection<DeliveryService> DeliveryServiceModifiedBies { get; set; }
        public virtual ICollection<FrameType> FrameTypeCreatedBies { get; set; }
        public virtual ICollection<FrameType> FrameTypeModifiedBies { get; set; }
        public virtual ICollection<Image> ImageCreatedBies { get; set; }
        public virtual ICollection<Image> ImageModifiedBies { get; set; }
        public virtual ICollection<User> InverseModifiedBy { get; set; }
        public virtual ICollection<Mat> MatCreatedBies { get; set; }
        public virtual ICollection<Mat> MatModifiedBies { get; set; }
        public virtual ICollection<MaterialType> MaterialTypeCreatedBies { get; set; }
        public virtual ICollection<MaterialType> MaterialTypeModifiedBies { get; set; }
        public virtual ICollection<Order> OrderCreatedBies { get; set; }
        public virtual ICollection<OrderItem> OrderItemCreatedBies { get; set; }
        public virtual ICollection<OrderItem> OrderItemModifiedBies { get; set; }
        public virtual ICollection<Order> OrderManagers { get; set; }
        public virtual ICollection<Order> OrderModifiedBies { get; set; }
        public virtual ICollection<OrderPaymentDetail> OrderPaymentDetailCreatedBies { get; set; }
        public virtual ICollection<OrderPaymentDetail> OrderPaymentDetailModifiedBies { get; set; }
        public virtual ICollection<OrderTracking> OrderTrackings { get; set; }
        public virtual ICollection<Order> OrderUsers { get; set; }
        public virtual ICollection<PrintingHouse> PrintingHouseCreatedBies { get; set; }
        public virtual ICollection<PrintingHouse> PrintingHouseModifiedBies { get; set; }
        public virtual ICollection<Size> SizeCreatedBies { get; set; }
        public virtual ICollection<Size> SizeModifiedBies { get; set; }
        public virtual ICollection<UserAddress> UserAddresses { get; set; }
        public virtual ICollection<UserConfirmation> UserConfirmations { get; set; }
        public virtual ICollection<UserContact> UserContacts { get; set; }
    }
}
