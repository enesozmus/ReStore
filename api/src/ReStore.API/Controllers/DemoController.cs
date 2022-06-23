using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReStore.API.Extensions;
using ReStore.API.RequestHelpers;
using ReStore.Application.CQRS;
using ReStore.Application.Extensions;

namespace ReStore.API.Controllers
{
        public class DemoController : BaseController
        {
                private readonly IMediator _mediator;

                public DemoController(IMediator mediator)
                {
                        _mediator = mediator;
                }
                
                [HttpGet]
                public async Task<IActionResult> Get([FromQuery] ProductParams productParams)
                {
                        var products = await _mediator.Send(new GetProductsQueryRequest()
                        {
                                Brands = productParams.Brands,
                                Colors = productParams.Colors,
                                PageNumber = productParams.PageNumber,
                                PageSize = productParams.PageSize,
                                OrderBy = productParams.OrderBy,
                                SearchTerm = productParams.SearchTerm
                        });


                        Response.AddPaginationHeader(products.MetaData);
                        return Ok(await _mediator.Send(new GetProductsQueryRequest()));
                }
                        
        }
}
