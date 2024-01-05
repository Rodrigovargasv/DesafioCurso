using DesafioCurso.Application.Commands.Response.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCurso.Application.Commands.Request.Product
{
    public class GetAllProductSeleableRequest :IRequest<IEnumerable<GetAllProductSeleableResponse>>
    {
        public int Quantity { get; set; }
    }
}
