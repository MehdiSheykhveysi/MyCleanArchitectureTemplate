using App.Domain.Entities;
using App.Domain.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.ApplicationService.ToDoItems.UseCases.Commands.CreateNewItem
{
    public class DeleteToDoitemHandler : IRequestHandler<DeleteToDoItemCommand>
    {
        private readonly IRepository<ToDoItem, int> _repository;

        public DeleteToDoitemHandler(IRepository<ToDoItem, int> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(new ToDoItem(request.Id), cancellationToken);
            return Unit.Value;
        }
    }
}
