using App.ApplicationService.ToDoItems.Dtos;
using App.Domain.Contracts;
using App.Domain.Entities;
using App.Domain.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.ApplicationService.ToDoItems.UseCases.Commands.CreateNewItem
{
    public class EditToDoItemHandler : IRequestHandler<EditToDoItemCommand, EditToDoItem>
    {
        private readonly IRepository<ToDoItem, int> _repository;
        private readonly IMapperFacade _mapper;

        public EditToDoItemHandler(IRepository<ToDoItem, int> repository, IMapperFacade mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EditToDoItem> Handle(EditToDoItemCommand request, CancellationToken cancellationToken)
        {
            ToDoItem mappedItem = _mapper.Map<ToDoItem, EditToDoItem>(request.EditToDoItem);

            await _repository.UpdateAsync(mappedItem, cancellationToken);

            return _mapper.Map<EditToDoItem, ToDoItem>(mappedItem);
        }
    }
}