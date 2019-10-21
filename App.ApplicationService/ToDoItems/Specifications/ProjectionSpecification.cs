using App.ApplicationService.Shaared;
using App.ApplicationService.ToDoItems.Dtos;
using App.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace App.ApplicationService.ToDoItems.Specifications
{
    public class ProjectionSpecification : Specification<ToDoItem, ToDoItemQueryDto>
    {
        public override Expression<Func<ToDoItem, ToDoItemQueryDto>> ToExpression()
        {
            return ToDoItem => new ToDoItemQueryDto
            {
                Id = ToDoItem.Id,
                Description = ToDoItem.Description
            };
        }
    }
}
