using TaskManagementServices.Enums;

namespace TaskManagementServices.DTOs
{
    public class TaskUpsertModel
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public StatusEnum Status { get; set; }
    }
}
