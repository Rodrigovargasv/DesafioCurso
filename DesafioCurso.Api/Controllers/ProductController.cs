using DesafioCurso.Application.Commands.Request.Person;
using DesafioCurso.Application.Commands.Request.Product;
using DesafioCurso.Application.Commands.Response.Product;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DesafioCurso.Api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "administrator, commonUser, manager, seller")]
        [HttpGet("GetAllProduct")]
        public async Task<IEnumerable<GetAllProductResponse>> GetAllPerson(int value)
        {
            var command = new GetAllProductRequest();
            command.Quantity = value;

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, commonUser, manager, seller")]
        [HttpGet("GetAllProductSeleable")]
        public async Task<IEnumerable<GetAllProductSeleableResponse>> GetAllProductSeleable(int value)
        {
            var command = new GetAllProductSeleableRequest();
            command.Quantity = value;

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, commonUser, manager, seller")]
        [HttpGet("GetProductById/{id:Guid}")]
        public async Task<GetProductByIdResponse> GetProductById(Guid id)
        {
            var command = new GetProductByIdRequest();
            command.Id = id;

            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager, seller")]
        [HttpPost("CreateProduct")]
        public async Task<CreateProductResponse> CreateProduct([FromBody] CreateProductRequest command)
        {
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager")]
        [HttpPut("UpdateProduct/{id:Guid}")]
        public async Task<UpdateProductResponse> UpdateProduct([FromBody] UpdateProductRequest command, Guid id)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [Authorize(Roles = "administrator, manager")]
        [HttpDelete("DeleteProduct/{id:Guid}")]
        public async Task<DeleteProductResponse> DeleteProduct(Guid id)
        {
            var command = new DeleteProductRequest();
            command.Id = id;

            return await _mediator.Send(command);
        }
    }
}