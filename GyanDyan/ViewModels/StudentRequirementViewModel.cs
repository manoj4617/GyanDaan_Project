using System;

namespace GyanDyan.ViewModels
{
    public class StudentRequirementViewModel
    {
        public int StudentProfileId { get; set; }
        public string StartDay { get; set; }
        public string EndDay { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Topic { get; set; }
        public DateTime TimeOfStart { get; set; }
        public string TypeOfClass { get; set; }
    }
}
