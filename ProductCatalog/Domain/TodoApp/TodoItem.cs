using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.TodoApp;


public enum Priority
{
    Low,
    Medium,
    High
}

public class TodoItem
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsCompleted { get; set; } = false;

    public Priority PriorityLevel { get; set; } 
    public DateTime? DueDate { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
