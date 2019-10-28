using System.ComponentModel.DataAnnotations;

namespace App.Bootstraper.Resources.Shared
{
    public class ToDoItemResource
    {
        [Required(ErrorMessage = " پر کردن فیلد  Description ضروری است ")]
        public string Description { get; set; }
    }
}
