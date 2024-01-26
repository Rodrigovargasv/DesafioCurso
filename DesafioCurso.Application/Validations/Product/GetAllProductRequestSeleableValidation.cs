using DesafioCurso.Application.Commands.Request.Product;
using FluentValidation;

namespace DesafioCurso.Application.Validations.Product
{
    public class GetAllProductRequestSeleableValidation : AbstractValidator<GetAllProductSeleableRequest>
    {
        public GetAllProductRequestSeleableValidation()
        {
            RuleFor(request => request.Page).GreaterThan(0).WithMessage("A página deve ser maior que zero.");
            RuleFor(request => request.PageSize).GreaterThan(0).WithMessage("O tamanho da página deve ser maior que zero.");
        }
    }
}