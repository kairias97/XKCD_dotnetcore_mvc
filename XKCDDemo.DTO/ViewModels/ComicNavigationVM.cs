using System;
using System.Collections.Generic;
using System.Text;

namespace XKCDDemo.DTO.ViewModels
{
    public class ComicNavigationVM
    {
        public int? FirstId { get; set; }
        public int? PreviousId { get; set; }
        public int? NextId { get; set; }
        public int? LastId { get; set; }
    }
}
