using TaskManagementServices.Enums;

namespace TaskManagementServices.Domain
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public int StatusId { get; set; }
        public StatusEnum Status
        {
            get => (StatusEnum)StatusId;
            set => StatusId = (int)value;
        }
        public int UserId { get; set; }
        public User User { get; set; } 
    }

}
