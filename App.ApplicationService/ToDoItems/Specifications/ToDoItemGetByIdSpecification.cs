using App.ApplicationService.Shaared;
using App.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace App.ApplicationService.ToDoItems.Specifications
{
    public class ToDoItemGetByIdSpecification : Specification<ToDoItem, bool>
    {
        private readonly int Id;

        public ToDoItemGetByIdSpecification(int id)
        {
            Id = id;
        }
        public override Expression<Func<ToDoItem, bool>> ToExpression()
        {
            return item => item.Id == Id;
        }
    }
}
