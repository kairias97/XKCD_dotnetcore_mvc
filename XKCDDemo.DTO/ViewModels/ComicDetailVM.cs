using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XKCDDemo.DTO.ViewModels
{
    public class ComicDetailVM
    {
        public string Month { get; set; }
        public int Num { get; set; }
        public string Link { get; set; }
        public string Year { get; set; }
        public string News { get; set; }
        [JsonProperty(PropertyName = "safe_title")]
        public string SafeTitle { get; set; }
        public string Transcript { get; set; }
        public string Alt { get; set; }
        public string Img { get; set; }
        public string Title { get; set; }
        public string Day { get; set; }
    }
}
