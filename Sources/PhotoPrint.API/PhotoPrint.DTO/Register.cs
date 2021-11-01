using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PPT.DTO
{
    public class RegisterRequest
    {
        [JsonPropertyName("User")]
        public User User
        {
            get; set;
        }

        [JsonPropertyName("Contact")]
        public Contact Contact
        {
            get; set;
        }
    }

    public class RegisterResponse
    {
        [JsonPropertyName("User")]
        public User User
        {
            get; set;
        }

        [JsonPropertyName("Contact")]
        public Contact Contact
        {
            get; set;
        }
    }
}
