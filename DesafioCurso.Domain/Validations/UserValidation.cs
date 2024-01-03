using DesafioCurso.Domain.Entities;
using DesafioCurso.Domain.Interfaces;
using DocumentValidator;
using FluentValidation;

namespace DesafioCurso.Domain.Validations
{
    public class UserValidation : AbstractValidator<User>
    {

     
        public UserValidation() 
        {
            RuleFor(x => x.FullName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .NotEmpty()
                .Matches(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@#$!%^&*])[A-Za-z\d@#$!%^&*]{8,}$")
                .WithMessage("A senha deve conter no mínimo 1 letra maiúscula, 1 letra minúsculas, 1 caracter especial e 8 caracteres.");


            RuleFor(p => p.Cpf_Cnpj)
               .Must(ValidationCpfAndCnpj)
               .WithMessage("CPF ou CNPJ inválido");
        }

        private bool ValidationCpfAndCnpj(string document)
        {
            if (string.IsNullOrEmpty(document))
                return true;

            if (CpfValidation.Validate(document))
                return true;

            if (CnpjValidation.Validate(document))
                return true;

            return false;

        }
    }
}
