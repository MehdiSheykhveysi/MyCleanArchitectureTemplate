using App.ApplicationService.ToDoItems.Dtos;
using App.Domain.Contracts;
using App.Domain.Entities;
using App.Domain.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.ApplicationService.ToDoItems.UseCases.Queries
{
    public class ToDoitemGetByIdQueryHandler : IRequestHandler<ToDoItemGetByIdQuery, ToDoItemQueryDto>
    {
        private readonly IRepository<ToDoItem, int> _repository;
        private readonly IMapperFacade _mapper;
        public ToDoitemGetByIdQueryHandler(IRepository<ToDoItem, int> repository, IMapperFacade mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ToDoItemQueryDto> Handle(ToDoItemGetByIdQuery request, CancellationToken cancellationToken)
        {
            ToDoItem toDoItem = await _repository.FirstItemAsync(request.ToDoItemGetByIdSpecification.ToExpression(), cancellationToken);

            return _mapper.Map<ToDoItemQueryDto, ToDoItem>(toDoItem);
        }
    }
}
