using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XKCDDemo.DTO.ViewModels.Configuration
{
    [JsonObject("xkcdApi")]
    public class ApiConfiguration
    {
        public string BaseUrl { get; set; }
    }
}
