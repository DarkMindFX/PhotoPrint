




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class PrintingHouseAddressConvertor
    {
        public static DTO.PrintingHouseAddress Convert(Interfaces.Entities.PrintingHouseAddress entity, IUrlHelper url)
        {
            var dto = new DTO.PrintingHouseAddress()
            {
        		        PrintingHouseID = entity.PrintingHouseID,

				        AddressID = entity.AddressID,

				        IsPrimary = entity.IsPrimary,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetPrintingHouseAddress", "printinghouseaddresses", new { printinghouseid = dto.PrintingHouseID, addressid = dto.AddressID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeletePrintingHouseAddress", "printinghouseaddresses", new { printinghouseid = dto.PrintingHouseID, addressid = dto.AddressID  }), "delete_printinghouseaddress", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertPrintingHouseAddress", "printinghouseaddresses"), "insert_printinghouseaddress", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdatePrintingHouseAddress", "printinghouseaddresses"), "update_printinghouseaddress", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.PrintingHouseAddress Convert(DTO.PrintingHouseAddress dto)
        {
            var entity = new Interfaces.Entities.PrintingHouseAddress()
            {
                
        		        PrintingHouseID = dto.PrintingHouseID,

				        AddressID = dto.AddressID,

				        IsPrimary = dto.IsPrimary,

				
     
            };

            return entity;
        }
    }
}
