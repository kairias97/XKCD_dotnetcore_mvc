using System;
using System.Collections.Generic;
using System.Text;

namespace XKCDDemo.DTO.ViewModels
{
    public class DisplayedComicVM
    {
        public int? PreviousId { get; set; }
        public ComicDetailVM Comic { get; set; }
        public int? NextId { get; set; }
    }
}
