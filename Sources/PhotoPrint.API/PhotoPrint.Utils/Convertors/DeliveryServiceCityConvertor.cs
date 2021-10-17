




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class DeliveryServiceCityConvertor
    {
        public static DTO.DeliveryServiceCity Convert(Interfaces.Entities.DeliveryServiceCity entity, IUrlHelper url)
        {
            var dto = new DTO.DeliveryServiceCity()
            {
        		        DeliveryServiceID = entity.DeliveryServiceID,

				        CityID = entity.CityID,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetDeliveryServiceCity", "deliveryservicecities", new { deliveryserviceid = dto.DeliveryServiceID, cityid = dto.CityID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeleteDeliveryServiceCity", "deliveryservicecities", new { deliveryserviceid = dto.DeliveryServiceID, cityid = dto.CityID  }), "delete_deliveryservicecity", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertDeliveryServiceCity", "deliveryservicecities"), "insert_deliveryservicecity", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdateDeliveryServiceCity", "deliveryservicecities"), "update_deliveryservicecity", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.DeliveryServiceCity Convert(DTO.DeliveryServiceCity dto)
        {
            var entity = new Interfaces.Entities.DeliveryServiceCity()
            {
                
        		        DeliveryServiceID = dto.DeliveryServiceID,

				        CityID = dto.CityID,

				
     
            };

            return entity;
        }
    }
}
