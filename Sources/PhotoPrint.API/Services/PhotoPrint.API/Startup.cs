


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

            Console.WriteLine("Starting service with parameters:");
            Console.WriteLine($"StorageType: {serviceConfig.StorageType}");
            Console.WriteLine("StorageInitParams");
            foreach(var k in serviceConfig.StorageInitParams.Keys)
            {
                Console.WriteLine($"{k}: {serviceConfig.StorageInitParams[k]}");
            }
            Console.WriteLine($"DALType: {serviceConfig.DALType}");
            foreach (var k in serviceConfig.DALInitParams.Keys)
            {
                Console.WriteLine($"{k}: {serviceConfig.DALInitParams[k]}");
            }

            PrepareComposition();

            services.AddCors();
            services.AddControllers();

            services.AddSwaggerGen( c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "PhotoPrint.API", Version = "v1" });
            });

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
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name:"PhotoPrint API v1");
            }); 

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
            services.AddSingleton<PPT.Services.Dal.IAddressDal, PPT.Services.Dal.AddressDal>();

            var dalAddressTypeDal = InitDal<IAddressTypeDal>(serviceCfg);
            services.AddSingleton<IAddressTypeDal>(dalAddressTypeDal);
            services.AddSingleton<PPT.Services.Dal.IAddressTypeDal, PPT.Services.Dal.AddressTypeDal>();

            var dalCategoryDal = InitDal<ICategoryDal>(serviceCfg);
            services.AddSingleton<ICategoryDal>(dalCategoryDal);
            services.AddSingleton<PPT.Services.Dal.ICategoryDal, PPT.Services.Dal.CategoryDal>();

            var dalCityDal = InitDal<ICityDal>(serviceCfg);
            services.AddSingleton<ICityDal>(dalCityDal);
            services.AddSingleton<PPT.Services.Dal.ICityDal, PPT.Services.Dal.CityDal>();

            var dalContactDal = InitDal<IContactDal>(serviceCfg);
            services.AddSingleton<IContactDal>(dalContactDal);
            services.AddSingleton<PPT.Services.Dal.IContactDal, PPT.Services.Dal.ContactDal>();

            var dalContactTypeDal = InitDal<IContactTypeDal>(serviceCfg);
            services.AddSingleton<IContactTypeDal>(dalContactTypeDal);
            services.AddSingleton<PPT.Services.Dal.IContactTypeDal, PPT.Services.Dal.ContactTypeDal>();

            var dalCountryDal = InitDal<ICountryDal>(serviceCfg);
            services.AddSingleton<ICountryDal>(dalCountryDal);
            services.AddSingleton<PPT.Services.Dal.ICountryDal, PPT.Services.Dal.CountryDal>();

            var dalCurrencyDal = InitDal<ICurrencyDal>(serviceCfg);
            services.AddSingleton<ICurrencyDal>(dalCurrencyDal);
            services.AddSingleton<PPT.Services.Dal.ICurrencyDal, PPT.Services.Dal.CurrencyDal>();

            var dalDeliveryServiceDal = InitDal<IDeliveryServiceDal>(serviceCfg);
            services.AddSingleton<IDeliveryServiceDal>(dalDeliveryServiceDal);
            services.AddSingleton<PPT.Services.Dal.IDeliveryServiceDal, PPT.Services.Dal.DeliveryServiceDal>();

            var dalDeliveryServiceCityDal = InitDal<IDeliveryServiceCityDal>(serviceCfg);
            services.AddSingleton<IDeliveryServiceCityDal>(dalDeliveryServiceCityDal);
            services.AddSingleton<PPT.Services.Dal.IDeliveryServiceCityDal, PPT.Services.Dal.DeliveryServiceCityDal>();

            var dalFrameTypeDal = InitDal<IFrameTypeDal>(serviceCfg);
            services.AddSingleton<IFrameTypeDal>(dalFrameTypeDal);
            services.AddSingleton<PPT.Services.Dal.IFrameTypeDal, PPT.Services.Dal.FrameTypeDal>();

            var dalImageDal = InitDal<IImageDal>(serviceCfg);
            services.AddSingleton<IImageDal>(dalImageDal);
            services.AddSingleton<PPT.Services.Dal.IImageDal, PPT.Services.Dal.ImageDal>();

            var dalImageCategoryDal = InitDal<IImageCategoryDal>(serviceCfg);
            services.AddSingleton<IImageCategoryDal>(dalImageCategoryDal);
            services.AddSingleton<PPT.Services.Dal.IImageCategoryDal, PPT.Services.Dal.ImageCategoryDal>();

            var dalImageRelatedDal = InitDal<IImageRelatedDal>(serviceCfg);
            services.AddSingleton<IImageRelatedDal>(dalImageRelatedDal);
            services.AddSingleton<PPT.Services.Dal.IImageRelatedDal, PPT.Services.Dal.ImageRelatedDal>();

            var dalImageThumbnailDal = InitDal<IImageThumbnailDal>(serviceCfg);
            services.AddSingleton<IImageThumbnailDal>(dalImageThumbnailDal);
            services.AddSingleton<PPT.Services.Dal.IImageThumbnailDal, PPT.Services.Dal.ImageThumbnailDal>();

            var dalMatDal = InitDal<IMatDal>(serviceCfg);
            services.AddSingleton<IMatDal>(dalMatDal);
            services.AddSingleton<PPT.Services.Dal.IMatDal, PPT.Services.Dal.MatDal>();

            var dalMaterialTypeDal = InitDal<IMaterialTypeDal>(serviceCfg);
            services.AddSingleton<IMaterialTypeDal>(dalMaterialTypeDal);
            services.AddSingleton<PPT.Services.Dal.IMaterialTypeDal, PPT.Services.Dal.MaterialTypeDal>();

            var dalMountingTypeDal = InitDal<IMountingTypeDal>(serviceCfg);
            services.AddSingleton<IMountingTypeDal>(dalMountingTypeDal);
            services.AddSingleton<PPT.Services.Dal.IMountingTypeDal, PPT.Services.Dal.MountingTypeDal>();

            var dalOrderDal = InitDal<IOrderDal>(serviceCfg);
            services.AddSingleton<IOrderDal>(dalOrderDal);
            services.AddSingleton<PPT.Services.Dal.IOrderDal, PPT.Services.Dal.OrderDal>();

            var dalOrderItemDal = InitDal<IOrderItemDal>(serviceCfg);
            services.AddSingleton<IOrderItemDal>(dalOrderItemDal);
            services.AddSingleton<PPT.Services.Dal.IOrderItemDal, PPT.Services.Dal.OrderItemDal>();

            var dalOrderPaymentDetailsDal = InitDal<IOrderPaymentDetailsDal>(serviceCfg);
            services.AddSingleton<IOrderPaymentDetailsDal>(dalOrderPaymentDetailsDal);
            services.AddSingleton<PPT.Services.Dal.IOrderPaymentDetailsDal, PPT.Services.Dal.OrderPaymentDetailsDal>();

            var dalOrderStatusDal = InitDal<IOrderStatusDal>(serviceCfg);
            services.AddSingleton<IOrderStatusDal>(dalOrderStatusDal);
            services.AddSingleton<PPT.Services.Dal.IOrderStatusDal, PPT.Services.Dal.OrderStatusDal>();

            var dalOrderStatusFlowDal = InitDal<IOrderStatusFlowDal>(serviceCfg);
            services.AddSingleton<IOrderStatusFlowDal>(dalOrderStatusFlowDal);
            services.AddSingleton<PPT.Services.Dal.IOrderStatusFlowDal, PPT.Services.Dal.OrderStatusFlowDal>();

            var dalOrderTrackingDal = InitDal<IOrderTrackingDal>(serviceCfg);
            services.AddSingleton<IOrderTrackingDal>(dalOrderTrackingDal);
            services.AddSingleton<PPT.Services.Dal.IOrderTrackingDal, PPT.Services.Dal.OrderTrackingDal>();

            var dalPaymentMethodDal = InitDal<IPaymentMethodDal>(serviceCfg);
            services.AddSingleton<IPaymentMethodDal>(dalPaymentMethodDal);
            services.AddSingleton<PPT.Services.Dal.IPaymentMethodDal, PPT.Services.Dal.PaymentMethodDal>();

            var dalPrintingHouseDal = InitDal<IPrintingHouseDal>(serviceCfg);
            services.AddSingleton<IPrintingHouseDal>(dalPrintingHouseDal);
            services.AddSingleton<PPT.Services.Dal.IPrintingHouseDal, PPT.Services.Dal.PrintingHouseDal>();

            var dalPrintingHouseAddressDal = InitDal<IPrintingHouseAddressDal>(serviceCfg);
            services.AddSingleton<IPrintingHouseAddressDal>(dalPrintingHouseAddressDal);
            services.AddSingleton<PPT.Services.Dal.IPrintingHouseAddressDal, PPT.Services.Dal.PrintingHouseAddressDal>();

            var dalPrintingHouseContactDal = InitDal<IPrintingHouseContactDal>(serviceCfg);
            services.AddSingleton<IPrintingHouseContactDal>(dalPrintingHouseContactDal);
            services.AddSingleton<PPT.Services.Dal.IPrintingHouseContactDal, PPT.Services.Dal.PrintingHouseContactDal>();

            var dalRegionDal = InitDal<IRegionDal>(serviceCfg);
            services.AddSingleton<IRegionDal>(dalRegionDal);
            services.AddSingleton<PPT.Services.Dal.IRegionDal, PPT.Services.Dal.RegionDal>();

            var dalSizeDal = InitDal<ISizeDal>(serviceCfg);
            services.AddSingleton<ISizeDal>(dalSizeDal);
            services.AddSingleton<PPT.Services.Dal.ISizeDal, PPT.Services.Dal.SizeDal>();

            var dalUnitDal = InitDal<IUnitDal>(serviceCfg);
            services.AddSingleton<IUnitDal>(dalUnitDal);
            services.AddSingleton<PPT.Services.Dal.IUnitDal, PPT.Services.Dal.UnitDal>();

            var dalUserDal = InitDal<IUserDal>(serviceCfg);
            services.AddSingleton<IUserDal>(dalUserDal);
            services.AddSingleton<PPT.Services.Dal.IUserDal, PPT.Services.Dal.UserDal>();

            var dalUserConfirmationDal = InitDal<IUserConfirmationDal>(serviceCfg);
            services.AddSingleton<IUserConfirmationDal>(dalUserConfirmationDal);
            services.AddSingleton<PPT.Services.Dal.IUserConfirmationDal, PPT.Services.Dal.UserConfirmationDal>();

            var dalUserAddressDal = InitDal<IUserAddressDal>(serviceCfg);
            services.AddSingleton<IUserAddressDal>(dalUserAddressDal);
            services.AddSingleton<PPT.Services.Dal.IUserAddressDal, PPT.Services.Dal.UserAddressDal>();

            var dalUserContactDal = InitDal<IUserContactDal>(serviceCfg);
            services.AddSingleton<IUserContactDal>(dalUserContactDal);
            services.AddSingleton<PPT.Services.Dal.IUserContactDal, PPT.Services.Dal.UserContactDal>();

            var dalUserStatusDal = InitDal<IUserStatusDal>(serviceCfg);
            services.AddSingleton<IUserStatusDal>(dalUserStatusDal);
            services.AddSingleton<PPT.Services.Dal.IUserStatusDal, PPT.Services.Dal.UserStatusDal>();

            var dalUserTypeDal = InitDal<IUserTypeDal>(serviceCfg);
            services.AddSingleton<IUserTypeDal>(dalUserTypeDal);
            services.AddSingleton<PPT.Services.Dal.IUserTypeDal, PPT.Services.Dal.UserTypeDal>();

            /** Connection Tester for health endpoint **/
            var dalConnTest = InitDal<IConnectionTestDal>(serviceCfg);
            services.AddSingleton<IConnectionTestDal>(dalConnTest);

            var storage = InitBinaryStorage(serviceCfg);
            services.AddSingleton<IBinaryStorage>(storage);
        }

        private TDal InitDal<TDal>(ServiceConfig serviceCfg) where TDal : IInitializable
        {
            var dal = Container.GetExportedValue<TDal>(serviceCfg.DALType);
            var dalInitParams = dal.CreateInitParams();

            dalInitParams.Parameters = serviceCfg.DALInitParams;
            dal.Init(dalInitParams);

            return dal;
        }

        private IBinaryStorage InitBinaryStorage(ServiceConfig serviceCfg)
        {
            var s = Container.GetExportedValue<IBinaryStorage>(serviceCfg.StorageType);

            var initParams = s.CreateInitParams();
            foreach(var k in initParams.Parameters.Keys)
            {
                initParams.Parameters[k] = serviceCfg.StorageInitParams[k];
            }
            s.Init(initParams);

            return s;

        }
    }
}
