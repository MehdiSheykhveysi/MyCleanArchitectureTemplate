using App.ApplicationService.ToDoItems.Dtos;
using App.Domain.Entities;
using AutoMapper;

namespace App.Infrastructure.AutomapperConfigurationAndMapping
{
    public class CommonMapping : Profile
    {
        public CommonMapping()
        {
            CreateMap<CreateIItemDto, ToDoItem>();
        }
    }
}
