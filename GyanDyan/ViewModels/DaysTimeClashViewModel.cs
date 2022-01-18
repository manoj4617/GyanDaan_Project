using static GyanDyan.Models.Domain;

namespace GyanDyan.ViewModels
{
    public class DaysTimeClashViewModel
    {
        public int Id { get; set; }
        public Days StartDay { get; set; }
        public Days EndDay { get; set; }
        public string Topic { get; set; }
        public string Subject { get; set; }
    }
}
