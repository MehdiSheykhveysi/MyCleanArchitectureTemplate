using App.ApplicationService.ToDoItems.Dtos;
using App.ApplicationService.ToDoItems.UseCases.Commands.CreateNewItem;
using App.Domain.Entities;
using AutoMapper;

namespace App.Infrastructure.AutomapperConfigurationAndMapping
{
    public class CommonMapping : Profile
    {
        public CommonMapping()
        {
            CreateMap<CreateIItemDto, ToDoItem>();

            CreateMap<ToDoItem, ToDoItemQueryDto>();

            CreateMap<ToDoItem, CreateIItemDto>();

            CreateMap<CreateToDoItemCommand, ToDoItem>();

            CreateMap<EditToDoItem, ToDoItem>().ReverseMap();
        }
    }
}
