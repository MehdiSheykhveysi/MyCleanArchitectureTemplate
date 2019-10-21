using App.ApplicationService.Shaared;
using App.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace App.ApplicationService.ToDoItems.Specifications
{
    public class OrderByIdSpecification : Specification<ToDoItem, int>
    {
        public override Expression<Func<ToDoItem, int>> ToExpression()
        {
            return ToDoitem => ToDoitem.Id;
        }
    }
}
