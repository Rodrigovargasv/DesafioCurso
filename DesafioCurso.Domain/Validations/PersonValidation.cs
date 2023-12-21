using DesafioCurso.Domain.Entities;
using FluentValidation;
using DocumentValidator;


namespace DesafioCurso.Domain.Validations
{
    public class PersonValidation : AbstractValidator<Person>
    {

        public PersonValidation()
        {
            RuleFor(p => p.FullName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(100);


            RuleFor(p => p.Document)
                .Must(ValidationCpfAndCnpj)
                .WithMessage("CPF ou CNPJ inválido");

            RuleFor(p => p.City)
                .NotEmpty()
                .NotNull()
                .MaximumLength(30);


            RuleFor(p => p.Observation)
                .MaximumLength(250);

            RuleFor(p => p.AlternativeCode)
                .MaximumLength(50);

        }

        // Implementando metados de validação de documento usuando a biblioteca DocumentValidator;
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


