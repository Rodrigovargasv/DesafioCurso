using DesafioCurso.Domain.Commons;
using DesafioCurso.Domain.Entities;
using FluentValidation;

namespace DesafioCurso.Domain.Validations
{
    public class UserValidation : AbstractValidator<User>
    {

     
        public UserValidation() 
        {
            RuleFor(x => x.FullName)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo nome não pode conter espaço em branco.")
                .NotNull()
                .MaximumLength(100);

            RuleFor(x => x.Nickname)
                 .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo apelido não pode conter espaço em branco.");



            RuleFor(x => x.Email)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo email não pode conter espaço em branco.")
                .EmailAddress();

            RuleFor(x => x.Password)
                .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo senha não pode conter espaço em branco.")
                .NotNull()
                .NotEmpty()
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$!%^&*])[A-Za-z\d@#$!%^&*]{8,}$")
                .WithMessage("A senha deve conter no mínimo 1 letra maiúscula, 1 letra minúsculas, 1 caracter especial e 8 caracteres.");


            RuleFor(p => p.Cpf_Cnpj)
               .Must(value => !UtilsValidations.ContainsWhitespace(value)).WithMessage("O campo CPF/CNPJ não pode conter espaço em branco.")
               .Must(value => UtilsValidations.ValidationCpfAndCnpj(value))
               .WithMessage("CPF ou CNPJ inválido");
        }

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
