using App.ApplicationService.Shaared;
using App.Domain.Entities;
using System;
using System.Linq.Expressions;

namespace App.ApplicationService.ToDoItems.Specifications
{
    public class ToDoAllItemSpecification : Specification<ToDoItem, bool>
    {
        public override Expression<Func<ToDoItem, bool>> ToExpression()
        {
            return ToDoItem => true;
        }
    }
}