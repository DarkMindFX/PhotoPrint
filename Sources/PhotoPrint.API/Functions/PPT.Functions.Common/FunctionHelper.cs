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
