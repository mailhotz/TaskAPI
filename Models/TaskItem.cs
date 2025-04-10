using System.Text.Json.Serialization;

namespace TaskApi.Models;

public class TaskItem
{
    public int Id { get; set; }
    [JsonRequired]
    public string Task { get; set; }
    public string? Details { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? CompletedOn { get; set; }
    public string? AssignedToName { get; set; }
    [JsonRequired]
    public string AssignedToEmail { get; set; }
    public bool IsDeleted { get; set; }
}