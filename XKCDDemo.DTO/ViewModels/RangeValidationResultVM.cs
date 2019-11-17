using System;
using System.Collections.Generic;
using System.Text;

namespace XKCDDemo.DTO.ViewModels
{
    public class RangeValidationResultVM
    {
        public bool IsValid { get; set; }
        public int? FirstComicId { get; set; }
        public int? LastComicId { get; set; }
    }
}
