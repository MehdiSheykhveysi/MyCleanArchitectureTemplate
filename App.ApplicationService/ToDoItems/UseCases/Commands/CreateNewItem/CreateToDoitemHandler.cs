using App.ApplicationService.ToDoItems.Dtos;
using App.ApplicationService.ToDoItems.Events;
using App.Domain.Contracts;
using App.Domain.Entities;
using App.Domain.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.ApplicationService.ToDoItems.UseCases.Commands.CreateNewItem
{
    class CreateToDoitemHandler : IRequestHandler<CreateToDoItemCommand, CreateIItemDto>
    {

        private readonly IRepository<ToDoItem,int> _repository;
        private readonly IMapperFacade _mapper;
        private readonly IMediator _mediator;

        public CreateToDoitemHandler(IRepository<ToDoItem,int> repository, IMapperFacade mapper, IMediator mediator)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<CreateIItemDto> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
        {
            ToDoItem mappedItem = _mapper.Map<ToDoItem, CreateToDoItemCommand>(request);

            await _repository.InsertAsync(mappedItem, cancellationToken).ConfigureAwait(false);

            await _mediator.Publish(new CreateToDoItemEvent($"Object Description value feild is {request.Description}"), cancellationToken);

            return _mapper.Map<CreateIItemDto, ToDoItem>(mappedItem);
        }
    }
}
