using System;
using System.ComponentModel.DataAnnotations;

namespace GyanDyan.ViewModels
{
    public class DateTimeViewModel
    {
        [Required]
        public string Subject { get; set; }

        public string Topic { get; set; }
        [Required]
        public string StartDay { get; set; }
        [Required]
        public string EndDay { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string EndTime { get; set; }
        [Required]
        public string TypeOfClass { get; set; }
    }
}
