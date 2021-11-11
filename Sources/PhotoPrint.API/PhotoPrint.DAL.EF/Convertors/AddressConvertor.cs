using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPT.DAL.EF.Convertors
{
    class AddressConvertor
    {
        public static PPT.Interfaces.Entities.Address FromEFEntity(PPT.DAL.EF.Models.Address efEntity)
        {
			Interfaces.Entities.Address result = new Interfaces.Entities.Address()
			{
				ID = efEntity.ID,

				AddressTypeID = efEntity.AddressTypeID,

				Title = efEntity.Title,

				CityID = efEntity.CityID,

				Street = efEntity.Street,

				BuildingNo = efEntity.BuildingNo,

				ApartmentNo = efEntity.ApartmentNo,

				Comment = efEntity.Comment,

				CreatedByID = efEntity.CreatedByID,

				CreatedDate = efEntity.CreatedDate,

				ModifiedByID = efEntity.ModifiedByID,

				ModifiedDate = efEntity.ModifiedDate,

				IsDeleted = efEntity.IsDeleted
			};

            return result;
        }

		public static PPT.DAL.EF.Models.Address ToEFEntity(PPT.Interfaces.Entities.Address entity)
		{
			PPT.DAL.EF.Models.Address result = new PPT.DAL.EF.Models.Address()
			{
				AddressTypeID = entity.AddressTypeID,

				Title = entity.Title,

				CityID = entity.CityID,

				Street = entity.Street,

				BuildingNo = entity.BuildingNo,

				ApartmentNo = entity.ApartmentNo,

				Comment = entity.Comment,

				CreatedByID = entity.CreatedByID,

				CreatedDate = entity.CreatedDate,

				ModifiedByID = entity.ModifiedByID,

				ModifiedDate = entity.ModifiedDate,

				IsDeleted = entity.IsDeleted
			};

			if(entity.ID.HasValue)
            {
				result.ID = (long)entity.ID;
			}

			return result;
		}
	}
}
