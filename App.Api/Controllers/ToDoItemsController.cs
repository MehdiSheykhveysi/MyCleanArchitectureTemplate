using System.Threading;
using System.Threading.Tasks;
using App.ApplicationService.ToDoItems.Dtos;
using App.ApplicationService.ToDoItems.Specifications;
using App.ApplicationService.ToDoItems.UseCases.Commands.CreateNewItem;
using App.ApplicationService.ToDoItems.UseCases.Queries;
using App.Bootstraper.Resources.Shared;
using App.Bootstraper.Resources.ToDoItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private const int PageSize = 3;
        private readonly IMediator _mediator;

        public ToDoItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/ToDoitems
        [HttpGet]
        public async Task<OkObjectResult> GetPaged([FromBody]PagingInfo pagingInfo, CancellationToken cancellationToken)
        {
            PaginatedList<ToDoItemQueryDto> result = await _mediator.Send(new ToDoItemPagedQuery(new ToDoAllItemSpecification(), new ProjectionSpecification(), new OrderByIdSpecification(), PageSize, pagingInfo.TakeItem, pagingInfo.CurrentPageNumber), cancellationToken);

            return Ok(result);
        }

        [Route("/Home/Post")]
        [HttpPost]
        public OkResult Post(string firstName)
        {
            return Ok();
        }

        // GET api/values/5
        [HttpGet("{id:int}")]
        public async Task<OkObjectResult> Get([FromRoute]int id, CancellationToken cancellationToken)
        {
            CreateIItemDto result = await _mediator.Send(new CreateToDoItemCommand(null), cancellationToken);
           // ToDoItemQueryDto result = await _mediator.Send(new ToDoItemGetByIdQuery(new ToDoItemGetByIdSpecification(id)), cancellationToken);
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<CreateIItemDto>> Post(ToDoItemResource toDoItem, CancellationToken cancellationToken)
        {
            CreateIItemDto result = await _mediator.Send(new CreateToDoItemCommand(toDoItem.Description), cancellationToken);
            return Ok(result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<OkObjectResult> Put(ToDoEditItemResource toDoEditItem, CancellationToken cancellationToken)
        {
            EditToDoItem finalResult = await _mediator.Send(new EditToDoItemCommand(new EditToDoItem(toDoEditItem.Id, toDoEditItem.Description)));

            return Ok(finalResult);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<OkObjectResult> Delete([FromRoute]int id, CancellationToken cancellationToken)
        {
            Unit result = await _mediator.Send(new DeleteToDoItemCommand(id), cancellationToken);

            return Ok("item Successfully deleted!!");
        }
    }
}
