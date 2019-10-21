namespace App.ApplicationService.ToDoItems.Dtos
{
    public class CreateIItemDto
    {
        public CreateIItemDto(int id) => Id = id;

        public int Id { get;}
    }
}
