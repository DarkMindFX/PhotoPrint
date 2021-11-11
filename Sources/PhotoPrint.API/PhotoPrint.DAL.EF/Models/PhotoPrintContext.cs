using Microsoft.EntityFrameworkCore;

#nullable disable

namespace PPT.DAL.EF.Models
{
    public partial class PhotoPrintContext : DbContext
    {
        public PhotoPrintContext()
        {
        }

        public PhotoPrintContext(DbContextOptions<PhotoPrintContext> options)
            : base(options)
        {
        }

        public PhotoPrintContext(string connectionString) : base(GetOptions(connectionString))
        {
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }


        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressType> AddressTypes { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<DeliveryService> DeliveryServices { get; set; }
        public virtual DbSet<DeliveryServiceCity> DeliveryServiceCities { get; set; }
        public virtual DbSet<FrameType> FrameTypes { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<ImageCategory> ImageCategories { get; set; }
        public virtual DbSet<ImageRelated> ImageRelateds { get; set; }
        public virtual DbSet<ImageThumbnail> ImageThumbnails { get; set; }
        public virtual DbSet<Mat> Mats { get; set; }
        public virtual DbSet<MaterialType> MaterialTypes { get; set; }
        public virtual DbSet<MountingType> MountingTypes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<OrderPaymentDetail> OrderPaymentDetails { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<OrderStatusFlow> OrderStatusFlows { get; set; }
        public virtual DbSet<OrderTracking> OrderTrackings { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<PrintingHouse> PrintingHouses { get; set; }
        public virtual DbSet<PrintingHouseAddress> PrintingHouseAddresses { get; set; }
        public virtual DbSet<PrintingHouseContact> PrintingHouseContacts { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAddress> UserAddresses { get; set; }
        public virtual DbSet<UserConfirmation> UserConfirmations { get; set; }
        public virtual DbSet<UserContact> UserContacts { get; set; }
        public virtual DbSet<UserStatus> UserStatuses { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<VOrder> VOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=PhotoPrint;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("Address");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.AddressTypeID).HasColumnName("AddressTypeID");

                entity.Property(e => e.ApartmentNo).HasMaxLength(50);

                entity.Property(e => e.BuildingNo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CityID).HasColumnName("CityID");

                entity.Property(e => e.Comment).HasMaxLength(1000);

                entity.Property(e => e.CreatedByID).HasColumnName("CreatedByID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.AddressType)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.AddressTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Address_AddressType");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.CityID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Address_City");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.AddressCreatedBies)
                    .HasForeignKey(d => d.CreatedByID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Address_UserCreated");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.AddressModifiedBies)
                    .HasForeignKey(d => d.ModifiedByID)
                    .HasConstraintName("FK_Address_UserModified");
            });

            modelBuilder.Entity<AddressType>(entity =>
            {
                entity.ToTable("AddressType");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.AddressTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedByID).HasColumnName("CreatedByID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.CategoryCreatedBies)
                    .HasForeignKey(d => d.CreatedByID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Category_CreatedByUser");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.CategoryModifiedBies)
                    .HasForeignKey(d => d.ModifiedByID)
                    .HasConstraintName("FK_Category_ModifiedByUser");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_ImageCategory_ImageCategory");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.RegionID).HasColumnName("RegionID");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.RegionID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_Region");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Comment).HasMaxLength(250);

                entity.Property(e => e.ContactTypeID).HasColumnName("ContactTypeID");

                entity.Property(e => e.CreatedByID).HasColumnName("CreatedByID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.ContactType)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ContactTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contact_ContactType");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.ContactCreatedBies)
                    .HasForeignKey(d => d.CreatedByID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contact_UserCreated");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.ContactModifiedBies)
                    .HasForeignKey(d => d.ModifiedByID)
                    .HasConstraintName("FK_Contact_UserModified");
            });

            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.ToTable("ContactType");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.ContactTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ISO)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("ISO");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CurrencyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ISO)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("ISO");
            });

            modelBuilder.Entity<DeliveryService>(entity =>
            {
                entity.ToTable("DeliveryService");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CreatedByID).HasColumnName("CreatedByID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveryServiceName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.DeliveryServiceCreatedBies)
                    .HasForeignKey(d => d.CreatedByID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DeliveryService_CreatedByUser");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.DeliveryServiceModifiedBies)
                    .HasForeignKey(d => d.ModifiedByID)
                    .HasConstraintName("FK_DeliveryService_ModifiedByUser");
            });

            modelBuilder.Entity<DeliveryServiceCity>(entity =>
            {
                entity.HasKey(e => new { e.DeliveryServiceID, e.CityID })
                    .HasName("PK_DeliveryServiceCity_1");

                entity.ToTable("DeliveryServiceCity");

                entity.Property(e => e.DeliveryServiceID).HasColumnName("DeliveryServiceID");

                entity.Property(e => e.CityID).HasColumnName("CityID");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.DeliveryServiceCities)
                    .HasForeignKey(d => d.CityID)
                    .HasConstraintName("FK_DeliveryServiceCity_City");

                entity.HasOne(d => d.DeliveryService)
                    .WithMany(p => p.DeliveryServiceCities)
                    .HasForeignKey(d => d.DeliveryServiceID)
                    .HasConstraintName("FK_DeliveryServiceCity_DeliveryService");
            });

            modelBuilder.Entity<FrameType>(entity =>
            {
                entity.ToTable("FrameType");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CreatedByID).HasColumnName("CreatedByID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.FrameTypeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ThumbnailUrl)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.FrameTypeCreatedBies)
                    .HasForeignKey(d => d.CreatedByID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FrameType_CreatedByUser");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.FrameTypeModifiedBies)
                    .HasForeignKey(d => d.ModifiedByID)
                    .HasConstraintName("FK_FrameType_ModifiedByUser");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CreatedByID).HasColumnName("CreatedByID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OriginUrl)
                    .IsRequired()
                    .HasMaxLength(3000);

                entity.Property(e => e.PriceAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PriceCurrencyId).HasColumnName("PriceCurrencyID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.ImageCreatedBies)
                    .HasForeignKey(d => d.CreatedByID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Image_UserCreated");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.ImageModifiedBies)
                    .HasForeignKey(d => d.ModifiedByID)
                    .HasConstraintName("FK_Image_UserModified");

                entity.HasOne(d => d.PriceCurrency)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.PriceCurrencyId)
                    .HasConstraintName("FK_Image_Currency");
            });

            modelBuilder.Entity<ImageCategory>(entity =>
            {
                entity.HasKey(e => new { e.ImageID, e.CategoryID })
                    .HasName("PK_ImageCategory_1");

                entity.ToTable("ImageCategory");

                entity.Property(e => e.ImageID).HasColumnName("ImageID");

                entity.Property(e => e.CategoryID).HasColumnName("CategoryID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.ImageCategories)
                    .HasForeignKey(d => d.CategoryID)
                    .HasConstraintName("FK_ImageCategory_Category");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.ImageCategories)
                    .HasForeignKey(d => d.ImageID)
                    .HasConstraintName("FK_ImageCategory_Image");
            });

            modelBuilder.Entity<ImageRelated>(entity =>
            {
                entity.HasKey(e => new { e.ImageID, e.RelatedImageID })
                    .HasName("PK_ImageRelated_1");

                entity.ToTable("ImageRelated");

                entity.Property(e => e.ImageID).HasColumnName("ImageID");

                entity.Property(e => e.RelatedImageID).HasColumnName("RelatedImageID");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.ImageRelatedImages)
                    .HasForeignKey(d => d.ImageID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImageRelated_RootImage");

                entity.HasOne(d => d.RelatedImage)
                    .WithMany(p => p.ImageRelatedRelatedImages)
                    .HasForeignKey(d => d.RelatedImageID)
                    .HasConstraintName("FK_ImageRelated_RelatedImage");
            });

            modelBuilder.Entity<ImageThumbnail>(entity =>
            {
                entity.ToTable("ImageThumbnail");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.ImageID).HasColumnName("ImageID");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.ImageThumbnails)
                    .HasForeignKey(d => d.ImageID)
                    .HasConstraintName("FK_ImageThumbnail_Image");
            });

            modelBuilder.Entity<Mat>(entity =>
            {
                entity.ToTable("Mat");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CreatedByID).HasColumnName("CreatedByID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.MatName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ThumbnailUrl)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.MatCreatedBies)
                    .HasForeignKey(d => d.CreatedByID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mat_CreatedByUser");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.MatModifiedBies)
                    .HasForeignKey(d => d.ModifiedByID)
                    .HasConstraintName("FK_Mat_ModifiedByUser");
            });

            modelBuilder.Entity<MaterialType>(entity =>
            {
                entity.ToTable("MaterialType");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CreatedByID).HasColumnName("CreatedByID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.MaterialTypeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ThumbnailUrl)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.MaterialTypeCreatedBies)
                    .HasForeignKey(d => d.CreatedByID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaterialType_CreatedByUser");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.MaterialTypeModifiedBies)
                    .HasForeignKey(d => d.ModifiedByID)
                    .HasConstraintName("FK_MaterialType_ModifiedByUser");
            });

            modelBuilder.Entity<MountingType>(entity =>
            {
                entity.ToTable("MountingType");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CreatedByID).HasColumnName("CreatedByID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MountingTypeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ThumbnailUrl)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Comments).HasMaxLength(1000);

                entity.Property(e => e.ContactID).HasColumnName("ContactID");

                entity.Property(e => e.CreatedByID).HasColumnName("CreatedByID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveryAddressID).HasColumnName("DeliveryAddressID");

                entity.Property(e => e.DeliveryServiceID).HasColumnName("DeliveryServiceID");

                entity.Property(e => e.ManagerID).HasColumnName("ManagerID");

                entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UserID).HasColumnName("UserID");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ContactID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Contact");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.OrderCreatedBies)
                    .HasForeignKey(d => d.CreatedByID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_UserCreated");

                entity.HasOne(d => d.DeliveryAddress)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DeliveryAddressID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_AddressDelivery");

                entity.HasOne(d => d.DeliveryService)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DeliveryServiceID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_DeliveryService");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.OrderManagers)
                    .HasForeignKey(d => d.ManagerID)
                    .HasConstraintName("FK_Order_ManagerUser");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.OrderModifiedBies)
                    .HasForeignKey(d => d.ModifiedByID)
                    .HasConstraintName("FK_Order_UserModified");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrderUsers)
                    .HasForeignKey(d => d.UserID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Comments)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.CreatedByID).HasColumnName("CreatedByID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FrameSizeID).HasColumnName("FrameSizeID");

                entity.Property(e => e.FrameTypeID).HasColumnName("FrameTypeID");

                entity.Property(e => e.ImageID).HasColumnName("ImageID");

                entity.Property(e => e.MatID).HasColumnName("MatID");

                entity.Property(e => e.MaterialTypeID).HasColumnName("MaterialTypeID");

                entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MountingTypeID).HasColumnName("MountingTypeID");

                entity.Property(e => e.OrderID).HasColumnName("OrderID");

                entity.Property(e => e.PriceAmountPerItem).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PriceCurrencyID).HasColumnName("PriceCurrencyID");

                entity.Property(e => e.PrintingHouseID).HasColumnName("PrintingHouseID");

                entity.Property(e => e.SizeID).HasColumnName("SizeID");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.OrderItemCreatedBies)
                    .HasForeignKey(d => d.CreatedByID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_CreatedByUser");

                entity.HasOne(d => d.FrameSize)
                    .WithMany(p => p.OrderItemFrameSizes)
                    .HasForeignKey(d => d.FrameSizeID)
                    .HasConstraintName("FK_OrderItem_FrameSize");

                entity.HasOne(d => d.FrameType)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.FrameTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_FrameType");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ImageID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_Image");

                entity.HasOne(d => d.Mat)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.MatID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_Mat");

                entity.HasOne(d => d.MaterialType)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.MaterialTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_MaterialType");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.OrderItemModifiedBies)
                    .HasForeignKey(d => d.ModifiedByID)
                    .HasConstraintName("FK_OrderItem_ModifiedByUser");

                entity.HasOne(d => d.MountingType)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.MountingTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_MountingType");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderID)
                    .HasConstraintName("FK_OrderItem_Order");

                entity.HasOne(d => d.PriceCurrency)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.PriceCurrencyID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderItem_Currency");

                entity.HasOne(d => d.PrintingHouse)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.PrintingHouseID)
                    .HasConstraintName("FK_OrderItem_PrintingHouse");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.OrderItemSizes)
                    .HasForeignKey(d => d.SizeID)
                    .HasConstraintName("FK_OrderItem_Size");
            });

            modelBuilder.Entity<OrderPaymentDetail>(entity =>
            {
                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CreatedByID).HasColumnName("CreatedByID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PaymentDateTime).HasColumnType("datetime");

                entity.Property(e => e.PaymentMethodID).HasColumnName("PaymentMethodID");

                entity.Property(e => e.PaymentTransUID)
                    .HasMaxLength(250)
                    .HasColumnName("PaymentTransUID");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.OrderPaymentDetailCreatedBies)
                    .HasForeignKey(d => d.CreatedByID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderPaymentDetails_CreatedByUser");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.OrderPaymentDetailModifiedBies)
                    .HasForeignKey(d => d.ModifiedByID)
                    .HasConstraintName("FK_OrderPaymentDetails_ModifiedByUser");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderPaymentDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderPaymentDetails_Order");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.OrderPaymentDetails)
                    .HasForeignKey(d => d.PaymentMethodID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderPaymentDetails_PaymentMethod");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.OrderStatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OrderStatusFlow>(entity =>
            {
                entity.HasKey(e => new { e.FromStatusID, e.ToStatusID });

                entity.ToTable("OrderStatusFlow");

                entity.Property(e => e.FromStatusID).HasColumnName("FromStatusID");

                entity.Property(e => e.ToStatusID).HasColumnName("ToStatusID");

                entity.HasOne(d => d.FromStatus)
                    .WithMany(p => p.OrderStatusFlowFromStatuses)
                    .HasForeignKey(d => d.FromStatusID)
                    .HasConstraintName("FK_OrderStatusFlow_StatusFrom");

                entity.HasOne(d => d.ToStatus)
                    .WithMany(p => p.OrderStatusFlowToStatuses)
                    .HasForeignKey(d => d.ToStatusID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderStatusFlow_StatusTo");
            });

            modelBuilder.Entity<OrderTracking>(entity =>
            {
                entity.ToTable("OrderTracking");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Comment).HasMaxLength(1000);

                entity.Property(e => e.OrderID).HasColumnName("OrderID");

                entity.Property(e => e.OrderStatusID).HasColumnName("OrderStatusID");

                entity.Property(e => e.SetByID).HasColumnName("SetByID");

                entity.Property(e => e.SetDate).HasColumnType("datetime");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderTrackings)
                    .HasForeignKey(d => d.OrderID)
                    .HasConstraintName("FK_OrderTracking_Order");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.OrderTrackings)
                    .HasForeignKey(d => d.OrderStatusID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderTracking_OrderStatus");

                entity.HasOne(d => d.SetBy)
                    .WithMany(p => p.OrderTrackings)
                    .HasForeignKey(d => d.SetByID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderTracking_User");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.ToTable("PaymentMethod");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PrintingHouse>(entity =>
            {
                entity.ToTable("PrintingHouse");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CreatedByID).HasColumnName("CreatedByID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.PrintingHouseCreatedBies)
                    .HasForeignKey(d => d.CreatedByID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrintingHouse_CreatedByUser");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.PrintingHouseModifiedBies)
                    .HasForeignKey(d => d.ModifiedByID)
                    .HasConstraintName("FK_PrintingHouse_ModifiedByUser");
            });

            modelBuilder.Entity<PrintingHouseAddress>(entity =>
            {
                entity.HasKey(e => new { e.PrintingHouseID, e.AddressId })
                    .HasName("PK_PrintingHouseAddress_1");

                entity.ToTable("PrintingHouseAddress");

                entity.Property(e => e.PrintingHouseID).HasColumnName("PrintingHouseID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.PrintingHouseAddresses)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrintingHouseAddress_Address");

                entity.HasOne(d => d.PrintingHouse)
                    .WithMany(p => p.PrintingHouseAddresses)
                    .HasForeignKey(d => d.PrintingHouseID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrintingHouseAddress_PrintingHouse");
            });

            modelBuilder.Entity<PrintingHouseContact>(entity =>
            {
                entity.HasKey(e => new { e.PrintingHouseID, e.ContactId })
                    .HasName("PK_PrintingHouseContact_1");

                entity.ToTable("PrintingHouseContact");

                entity.Property(e => e.PrintingHouseID).HasColumnName("PrintingHouseID");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.PrintingHouseContacts)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PrintingHouseContact_Contact");

                entity.HasOne(d => d.PrintingHouse)
                    .WithMany(p => p.PrintingHouseContacts)
                    .HasForeignKey(d => d.PrintingHouseID)
                    .HasConstraintName("FK_PrintingHouseContact_PrintingHouseContact");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CountryID).HasColumnName("CountryID");

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Regions)
                    .HasForeignKey(d => d.CountryID)
                    .HasConstraintName("FK_Region_Country");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CreatedByID).HasColumnName("CreatedByID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SizeName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.SizeCreatedBies)
                    .HasForeignKey(d => d.CreatedByID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Size_CreatedByUser");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.SizeModifiedBies)
                    .HasForeignKey(d => d.ModifiedByID)
                    .HasConstraintName("FK_Size_ModifiedByUser");
            });

            modelBuilder.Entity<Unit>(entity =>
            {
                entity.ToTable("Unit");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.UnitAbbr)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UnitName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FriendlyName).HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PwdHash)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserStatusID).HasColumnName("UserStatusID");

                entity.Property(e => e.UserTypeID).HasColumnName("UserTypeID");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.InverseModifiedBy)
                    .HasForeignKey(d => d.ModifiedByID)
                    .HasConstraintName("FK_User_User");

                entity.HasOne(d => d.UserStatus)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserStatusID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserStatus");

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserTypeID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_UserType");
            });

            modelBuilder.Entity<UserAddress>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.AddressID });

                entity.ToTable("UserAddress");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.AddressID).HasColumnName("AddressID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.AddressID)
                    .HasConstraintName("FK_UserAddress_Address");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserAddresses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserAddress_User");
            });

            modelBuilder.Entity<UserConfirmation>(entity =>
            {
                entity.ToTable("UserConfirmation");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.ConfirmationCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ConfirmationDate).HasColumnType("datetime");

                entity.Property(e => e.ExpiresDate).HasColumnType("datetime");

                entity.Property(e => e.UserID).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserConfirmations)
                    .HasForeignKey(d => d.UserID)
                    .HasConstraintName("FK_UserConfirmation_User");
            });

            modelBuilder.Entity<UserContact>(entity =>
            {
                entity.HasKey(e => new { e.UserID, e.ContactID });

                entity.ToTable("UserContact");

                entity.Property(e => e.UserID).HasColumnName("UserID");

                entity.Property(e => e.ContactID).HasColumnName("ContactID");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.UserContacts)
                    .HasForeignKey(d => d.ContactID)
                    .HasConstraintName("FK_UserContact_Contact");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserContacts)
                    .HasForeignKey(d => d.UserID)
                    .HasConstraintName("FK_UserContact_User");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.ToTable("UserStatus");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserType>(entity =>
            {
                entity.ToTable("UserType");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.UserTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<VOrder>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("v_Orders");

                entity.Property(e => e.Client)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Comments).HasMaxLength(1000);

                entity.Property(e => e.ContactID).HasColumnName("ContactID");

                entity.Property(e => e.CreatedByID).HasColumnName("CreatedByID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DeliveryAddressId).HasColumnName("DeliveryAddressID");

                entity.Property(e => e.DeliveryService)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Delivery Service");

                entity.Property(e => e.DeliveryServiceId).HasColumnName("DeliveryServiceID");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Manager).HasMaxLength(250);

                entity.Property(e => e.ManagerId).HasColumnName("ManagerID");

                entity.Property(e => e.ModifiedByID).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OrderID).HasColumnName("OrderID");

                entity.Property(e => e.OrderStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Order Status");

                entity.Property(e => e.PaymentMethod)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Payment Method");

                entity.Property(e => e.TransactionId)
                    .HasMaxLength(250)
                    .HasColumnName("Transaction ID");

                entity.Property(e => e.UserID).HasColumnName("UserID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
