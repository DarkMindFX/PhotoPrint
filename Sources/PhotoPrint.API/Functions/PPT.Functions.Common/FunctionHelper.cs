using PPT.Interfaces;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace PPT.Functions.Common
{
    public class FunctionHelper
    {
        public FunctionHelper()
        {
            PrepareComposition();
        }
        public CompositionContainer Container
        {
            get;
            set;
        }          

        public TDal CreateDal<TDal>() where TDal : IInitializable
        {
            var dal = Container.GetExportedValue<TDal>(System.Environment.GetEnvironmentVariable(Constants.ENV_DAL_TYPE));
            var dalInitParams = dal.CreateInitParams();

            dalInitParams.Parameters["ConnectionString"] = System.Environment.GetEnvironmentVariable(Constants.ENV_SQL_CONNECTION_STRING);

            dalInitParams.Parameters = dalInitParams.Parameters;
            dal.Init(dalInitParams);

            return dal;
        }

        public void SetCreatedModifiedProperties(object obj, string propNameDate, string propNameID, PPT.Interfaces.Entities.User currentUser = null)
        {
            if (propNameDate != null && obj.GetType().GetProperty(propNameDate) != null)
            {
                obj.GetType().GetProperty(propNameDate,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.Instance).SetValue(obj, DateTime.UtcNow, null);
            }
            if (propNameID != null && obj.GetType().GetProperty(propNameID) != null && currentUser != null)
            {
                obj.GetType().GetProperty(propNameID,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.Instance).SetValue(obj, currentUser.ID, null);
            }
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
                string codeBase = Assembly.GetExecutingAssembly().Location;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        
        public string ToJosn(object obj)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(obj, options);

            return jsonString;
        }
    }
}
