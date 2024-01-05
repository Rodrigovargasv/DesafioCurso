using DesafioCurso.Domain.Commons;
using DesafioCurso.Domain.Entities;
using FluentValidation;

namespace DesafioCurso.Domain.Validations
{
    public class PersonValidation : AbstractValidator<Person>
    {
        public PersonValidation()
        {
            RuleFor(p => p.FullName)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo descrição completa não pode conter espaço em branco.")
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(p => p.Document)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo documento não pode conter espaço em branco.")
                .Must(value => UtilsValidations.ValidationCpfAndCnpj(value))
                .WithMessage("CPF ou CNPJ inválido");

            RuleFor(p => p.City)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo cidade não pode conter espaço em branco.")
                .NotEmpty()
                .NotNull()
                .MaximumLength(30);

            RuleFor(p => p.Observation)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo obeservação não pode conter espaço em branco.")
                .MaximumLength(250);

            RuleFor(p => p.AlternativeCode)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo codigo alternativo não pode conter espaço em branco.")
                .MaximumLength(50);
        }

        //// Implementando metados de validação de documento usuando a biblioteca DocumentValidator;
        //private bool ValidationCpfAndCnpj(string document)
        //{
        //    if (string.IsNullOrEmpty(document))
        //        return true;

        //    if (CpfValidation.Validate(document))
        //        return true;

        //    if (CnpjValidation.Validate(document))
        //        return true;

        //    return false;

        //}
    }
}