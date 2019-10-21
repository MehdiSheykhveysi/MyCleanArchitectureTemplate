using App.Domain.Shared;

namespace App.Domain.Entities
{
    public class ToDoItem : IntEntity
    {
        public string Description { get; set; }
    }
}
