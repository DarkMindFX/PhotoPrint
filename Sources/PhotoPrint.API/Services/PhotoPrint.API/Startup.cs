


using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Text;
using PPT.PhotoPrint.API.Helpers;
using PPT.PhotoPrint.API.MiddleWare;
using PPT.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace PPT.PhotoPrint.API
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceConfig = Configuration.GetSection("ServiceConfig").Get<ServiceConfig>();
            PrepareComposition();

            services.AddCors();
            services.AddControllers();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            AddInjections(services, serviceConfig);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void PrepareComposition()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            DirectoryCatalog directoryCatalog = new DirectoryCatalog(AssemblyDirectory);
            catalog.Catalogs.Add(directoryCatalog);
            Container = new CompositionContainer(catalog);
            Container.ComposeParts(this);
        }

        private string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private CompositionContainer Container
        {
            get;
            set;
        }

        private void AddInjections(IServiceCollection services, ServiceConfig serviceCfg)
        {
            var dalAddressDal = InitDal<IAddressDal>(serviceCfg);
            services.AddSingleton<IAddressDal>(dalAddressDal);
            services.AddSingleton<Dal.IAddressDal, Dal.AddressDal>();

            var dalAddressTypeDal = InitDal<IAddressTypeDal>(serviceCfg);
            services.AddSingleton<IAddressTypeDal>(dalAddressTypeDal);
            services.AddSingleton<Dal.IAddressTypeDal, Dal.AddressTypeDal>();

            var dalCategoryDal = InitDal<ICategoryDal>(serviceCfg);
            services.AddSingleton<ICategoryDal>(dalCategoryDal);
            services.AddSingleton<Dal.ICategoryDal, Dal.CategoryDal>();

            var dalCityDal = InitDal<ICityDal>(serviceCfg);
            services.AddSingleton<ICityDal>(dalCityDal);
            services.AddSingleton<Dal.ICityDal, Dal.CityDal>();

            var dalContactDal = InitDal<IContactDal>(serviceCfg);
            services.AddSingleton<IContactDal>(dalContactDal);
            services.AddSingleton<Dal.IContactDal, Dal.ContactDal>();

            var dalContactTypeDal = InitDal<IContactTypeDal>(serviceCfg);
            services.AddSingleton<IContactTypeDal>(dalContactTypeDal);
            services.AddSingleton<Dal.IContactTypeDal, Dal.ContactTypeDal>();

            var dalCountryDal = InitDal<ICountryDal>(serviceCfg);
            services.AddSingleton<ICountryDal>(dalCountryDal);
            services.AddSingleton<Dal.ICountryDal, Dal.CountryDal>();

            var dalCurrencyDal = InitDal<ICurrencyDal>(serviceCfg);
            services.AddSingleton<ICurrencyDal>(dalCurrencyDal);
            services.AddSingleton<Dal.ICurrencyDal, Dal.CurrencyDal>();

            var dalDeliveryServiceDal = InitDal<IDeliveryServiceDal>(serviceCfg);
            services.AddSingleton<IDeliveryServiceDal>(dalDeliveryServiceDal);
            services.AddSingleton<Dal.IDeliveryServiceDal, Dal.DeliveryServiceDal>();

            var dalDeliveryServiceCityDal = InitDal<IDeliveryServiceCityDal>(serviceCfg);
            services.AddSingleton<IDeliveryServiceCityDal>(dalDeliveryServiceCityDal);
            services.AddSingleton<Dal.IDeliveryServiceCityDal, Dal.DeliveryServiceCityDal>();

            var dalFrameTypeDal = InitDal<IFrameTypeDal>(serviceCfg);
            services.AddSingleton<IFrameTypeDal>(dalFrameTypeDal);
            services.AddSingleton<Dal.IFrameTypeDal, Dal.FrameTypeDal>();

            var dalImageDal = InitDal<IImageDal>(serviceCfg);
            services.AddSingleton<IImageDal>(dalImageDal);
            services.AddSingleton<Dal.IImageDal, Dal.ImageDal>();

            var dalImageCategoryDal = InitDal<IImageCategoryDal>(serviceCfg);
            services.AddSingleton<IImageCategoryDal>(dalImageCategoryDal);
            services.AddSingleton<Dal.IImageCategoryDal, Dal.ImageCategoryDal>();

            var dalImageRelatedDal = InitDal<IImageRelatedDal>(serviceCfg);
            services.AddSingleton<IImageRelatedDal>(dalImageRelatedDal);
            services.AddSingleton<Dal.IImageRelatedDal, Dal.ImageRelatedDal>();

            var dalImageThumbnailDal = InitDal<IImageThumbnailDal>(serviceCfg);
            services.AddSingleton<IImageThumbnailDal>(dalImageThumbnailDal);
            services.AddSingleton<Dal.IImageThumbnailDal, Dal.ImageThumbnailDal>();

            var dalMatDal = InitDal<IMatDal>(serviceCfg);
            services.AddSingleton<IMatDal>(dalMatDal);
            services.AddSingleton<Dal.IMatDal, Dal.MatDal>();

            var dalMaterialTypeDal = InitDal<IMaterialTypeDal>(serviceCfg);
            services.AddSingleton<IMaterialTypeDal>(dalMaterialTypeDal);
            services.AddSingleton<Dal.IMaterialTypeDal, Dal.MaterialTypeDal>();

            var dalMountingTypeDal = InitDal<IMountingTypeDal>(serviceCfg);
            services.AddSingleton<IMountingTypeDal>(dalMountingTypeDal);
            services.AddSingleton<Dal.IMountingTypeDal, Dal.MountingTypeDal>();

            var dalOrderDal = InitDal<IOrderDal>(serviceCfg);
            services.AddSingleton<IOrderDal>(dalOrderDal);
            services.AddSingleton<Dal.IOrderDal, Dal.OrderDal>();

            var dalOrderItemDal = InitDal<IOrderItemDal>(serviceCfg);
            services.AddSingleton<IOrderItemDal>(dalOrderItemDal);
            services.AddSingleton<Dal.IOrderItemDal, Dal.OrderItemDal>();

            var dalOrderPaymentDetailsDal = InitDal<IOrderPaymentDetailsDal>(serviceCfg);
            services.AddSingleton<IOrderPaymentDetailsDal>(dalOrderPaymentDetailsDal);
            services.AddSingleton<Dal.IOrderPaymentDetailsDal, Dal.OrderPaymentDetailsDal>();

            var dalOrderStatusDal = InitDal<IOrderStatusDal>(serviceCfg);
            services.AddSingleton<IOrderStatusDal>(dalOrderStatusDal);
            services.AddSingleton<Dal.IOrderStatusDal, Dal.OrderStatusDal>();

            var dalOrderStatusFlowDal = InitDal<IOrderStatusFlowDal>(serviceCfg);
            services.AddSingleton<IOrderStatusFlowDal>(dalOrderStatusFlowDal);
            services.AddSingleton<Dal.IOrderStatusFlowDal, Dal.OrderStatusFlowDal>();

            var dalOrderTrackingDal = InitDal<IOrderTrackingDal>(serviceCfg);
            services.AddSingleton<IOrderTrackingDal>(dalOrderTrackingDal);
            services.AddSingleton<Dal.IOrderTrackingDal, Dal.OrderTrackingDal>();

            var dalPaymentMethodDal = InitDal<IPaymentMethodDal>(serviceCfg);
            services.AddSingleton<IPaymentMethodDal>(dalPaymentMethodDal);
            services.AddSingleton<Dal.IPaymentMethodDal, Dal.PaymentMethodDal>();

            var dalPrintingHouseDal = InitDal<IPrintingHouseDal>(serviceCfg);
            services.AddSingleton<IPrintingHouseDal>(dalPrintingHouseDal);
            services.AddSingleton<Dal.IPrintingHouseDal, Dal.PrintingHouseDal>();

            var dalPrintingHouseAddressDal = InitDal<IPrintingHouseAddressDal>(serviceCfg);
            services.AddSingleton<IPrintingHouseAddressDal>(dalPrintingHouseAddressDal);
            services.AddSingleton<Dal.IPrintingHouseAddressDal, Dal.PrintingHouseAddressDal>();

            var dalPrintingHouseContactDal = InitDal<IPrintingHouseContactDal>(serviceCfg);
            services.AddSingleton<IPrintingHouseContactDal>(dalPrintingHouseContactDal);
            services.AddSingleton<Dal.IPrintingHouseContactDal, Dal.PrintingHouseContactDal>();

            var dalRegionDal = InitDal<IRegionDal>(serviceCfg);
            services.AddSingleton<IRegionDal>(dalRegionDal);
            services.AddSingleton<Dal.IRegionDal, Dal.RegionDal>();

            var dalSizeDal = InitDal<ISizeDal>(serviceCfg);
            services.AddSingleton<ISizeDal>(dalSizeDal);
            services.AddSingleton<Dal.ISizeDal, Dal.SizeDal>();

            var dalUnitDal = InitDal<IUnitDal>(serviceCfg);
            services.AddSingleton<IUnitDal>(dalUnitDal);
            services.AddSingleton<Dal.IUnitDal, Dal.UnitDal>();

            var dalUserDal = InitDal<IUserDal>(serviceCfg);
            services.AddSingleton<IUserDal>(dalUserDal);
            services.AddSingleton<Dal.IUserDal, Dal.UserDal>();

            var dalUserAddressDal = InitDal<IUserAddressDal>(serviceCfg);
            services.AddSingleton<IUserAddressDal>(dalUserAddressDal);
            services.AddSingleton<Dal.IUserAddressDal, Dal.UserAddressDal>();

            var dalUserContactDal = InitDal<IUserContactDal>(serviceCfg);
            services.AddSingleton<IUserContactDal>(dalUserContactDal);
            services.AddSingleton<Dal.IUserContactDal, Dal.UserContactDal>();

            var dalUserStatusDal = InitDal<IUserStatusDal>(serviceCfg);
            services.AddSingleton<IUserStatusDal>(dalUserStatusDal);
            services.AddSingleton<Dal.IUserStatusDal, Dal.UserStatusDal>();

            var dalUserTypeDal = InitDal<IUserTypeDal>(serviceCfg);
            services.AddSingleton<IUserTypeDal>(dalUserTypeDal);
            services.AddSingleton<Dal.IUserTypeDal, Dal.UserTypeDal>();

            /** Connection Tester for health endpoint **/
            var dalConnTest = InitDal<IConnectionTestDal>(serviceCfg);
            services.AddSingleton<IConnectionTestDal>(dalConnTest);

        }

        private TDal InitDal<TDal>(ServiceConfig serviceCfg) where TDal : IInitializable
        {
            var dal = Container.GetExportedValue<TDal>(serviceCfg.DALType);
            var dalInitParams = dal.CreateInitParams();

            dalInitParams.Parameters = serviceCfg.DALInitParams;
            dal.Init(dalInitParams);

            return dal;

        }
    }
}
