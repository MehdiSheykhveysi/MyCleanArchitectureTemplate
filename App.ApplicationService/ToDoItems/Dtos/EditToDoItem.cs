namespace App.ApplicationService.ToDoItems.Dtos
{
    public class EditToDoItem
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public EditToDoItem(int id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
