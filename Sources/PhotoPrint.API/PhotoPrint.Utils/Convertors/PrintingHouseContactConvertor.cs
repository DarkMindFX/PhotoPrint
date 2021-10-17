




using Microsoft.AspNetCore.Mvc;
using System;

namespace PPT.Utils.Convertors
{
    public class PrintingHouseContactConvertor
    {
        public static DTO.PrintingHouseContact Convert(Interfaces.Entities.PrintingHouseContact entity, IUrlHelper url)
        {
            var dto = new DTO.PrintingHouseContact()
            {
        		        PrintingHouseID = entity.PrintingHouseID,

				        ContactID = entity.ContactID,

				        IsPrimary = entity.IsPrimary,

				
            };

                        if(url != null)
            {
                dto.Links.Add(new DTO.Link(url.Action("GetPrintingHouseContact", "printinghousecontacts", new { printinghouseid = dto.PrintingHouseID, contactid = dto.ContactID  }), "self", "GET"));
                dto.Links.Add(new DTO.Link(url.Action("DeletePrintingHouseContact", "printinghousecontacts", new { printinghouseid = dto.PrintingHouseID, contactid = dto.ContactID  }), "delete_printinghousecontact", "DELETE"));
                dto.Links.Add(new DTO.Link(url.Action("InsertPrintingHouseContact", "printinghousecontacts"), "insert_printinghousecontact", "POST"));
                dto.Links.Add(new DTO.Link(url.Action("UpdatePrintingHouseContact", "printinghousecontacts"), "update_printinghousecontact", "PUT"));
            }
            return dto;

        }

        public static Interfaces.Entities.PrintingHouseContact Convert(DTO.PrintingHouseContact dto)
        {
            var entity = new Interfaces.Entities.PrintingHouseContact()
            {
                
        		        PrintingHouseID = dto.PrintingHouseID,

				        ContactID = dto.ContactID,

				        IsPrimary = dto.IsPrimary,

				
     
            };

            return entity;
        }
    }
}
