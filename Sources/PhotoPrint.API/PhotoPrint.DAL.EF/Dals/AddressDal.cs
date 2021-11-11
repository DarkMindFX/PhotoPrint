using PPT.DAL.EF.Models;
using PPT.Interfaces;
using PPT.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPT.DAL.EF.Dals
{
    class AddressDalInitParams : InitParamsImpl
    {
    }

    [Export("EF", typeof(IAddressDal))]
    public class AddressDal : IAddressDal
    {
        PhotoPrintContext dbContext;

        public IInitParams CreateInitParams()
        {
            return new AddressDalInitParams();
        }

        public bool Delete(long? ID)
        {
            var entity = dbContext.Addresses.Find(ID);
            if (entity != null)
            {
                entity.IsDeleted = true;
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Erase(long? ID)
        {
            var entity = dbContext.Addresses.Where( e => e.ID == ID).FirstOrDefault();
            if (entity != null)
            {
                var entityEntry = dbContext.Remove<Models.Address>(entity);
                dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public PPT.Interfaces.Entities.Address Get(long? ID)
        {
            PPT.Interfaces.Entities.Address result = null;
            var entity = dbContext.Addresses.Where(e => e.ID == ID).FirstOrDefault();
            if (entity != null)
            {
                result = Convertors.AddressConvertor.FromEFEntity(entity);
            }
            return result;
        }

        public IList<PPT.Interfaces.Entities.Address> GetAll()
        {
            var entities = dbContext.Addresses.ToList();

            IList<PPT.Interfaces.Entities.Address> result = ToList(entities);
            
            return result;
        }

        public IList<PPT.Interfaces.Entities.Address> GetByAddressTypeID(long AddressTypeID)
        {
            var entities = dbContext.Addresses.Where(e => e.AddressTypeID == AddressTypeID).ToList();

            IList<PPT.Interfaces.Entities.Address> result = ToList(entities);

            return result;
        }

        public IList<PPT.Interfaces.Entities.Address> GetByCityID(long CityID)
        {
            var entities = dbContext.Addresses.Where(e => e.CityID == CityID).ToList();

            IList<PPT.Interfaces.Entities.Address> result = ToList(entities);

            return result;
        }

        public IList<PPT.Interfaces.Entities.Address> GetByCreatedByID(long CreatedByID)
        {
            var entities = dbContext.Addresses.Where(e => e.CreatedByID == CreatedByID).ToList();

            IList<PPT.Interfaces.Entities.Address> result = ToList(entities);

            return result;
        }

        public IList<PPT.Interfaces.Entities.Address> GetByModifiedByID(long? ModifiedByID)
        {
            var entities = dbContext.Addresses.Where(e => e.ModifiedByID == ModifiedByID).ToList();

            IList<PPT.Interfaces.Entities.Address> result = ToList(entities);

            return result;
        }

        public void Init(IInitParams initParams)
        {
            dbContext = new PhotoPrintContext(initParams.Parameters["ConnectionString"]);
        }

        public PPT.Interfaces.Entities.Address Insert(PPT.Interfaces.Entities.Address entity)
        {
            PPT.Interfaces.Entities.Address result = null;
            var efEntity = Convertors.AddressConvertor.ToEFEntity(entity);
            var efEntityEntry = dbContext.Add<PPT.DAL.EF.Models.Address>(efEntity);
            dbContext.SaveChanges();

            result = Convertors.AddressConvertor.FromEFEntity(efEntityEntry.Entity);

            return result;
        }

        public PPT.Interfaces.Entities.Address Update(PPT.Interfaces.Entities.Address entity)
        {
            PPT.Interfaces.Entities.Address result = null;
            var efEntity = dbContext.Addresses.Where(e => e.ID == entity.ID).FirstOrDefault();
            if (efEntity != null)
            {
                efEntity.AddressTypeID = entity.AddressTypeID;

                efEntity.Title = entity.Title;

                efEntity.CityID = entity.CityID;

                efEntity.Street = entity.Street;

                efEntity.BuildingNo = entity.BuildingNo;

                efEntity.ApartmentNo = entity.ApartmentNo;

                efEntity.Comment = entity.Comment;

                efEntity.CreatedByID = entity.CreatedByID;

                efEntity.CreatedDate = entity.CreatedDate;

                efEntity.ModifiedByID = entity.ModifiedByID;

                efEntity.ModifiedDate = entity.ModifiedDate;

                efEntity.IsDeleted = entity.IsDeleted;

                dbContext.SaveChanges();

                efEntity = dbContext.Addresses.Where(e => e.ID == entity.ID).FirstOrDefault();
                result = Convertors.AddressConvertor.FromEFEntity(efEntity);
            }
            return result;
        }

        #region Support methods
        IList<PPT.Interfaces.Entities.Address> ToList(IList<PPT.DAL.EF.Models.Address> entities)
        {
            IList<PPT.Interfaces.Entities.Address> result = new List<PPT.Interfaces.Entities.Address>();
            if (entities != null)
            {
                foreach (var e in entities)
                {
                    result.Add(Convertors.AddressConvertor.FromEFEntity(e));
                }
            }
            return result;
        }
        
        #endregion
    }
}
