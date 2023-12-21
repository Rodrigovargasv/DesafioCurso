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
                .Length(11, 14)
                .Must(ValidationCpfAndCnpj)
                .WithMessage("CPF ou CNPJ inválido");


            RuleFor(p => p.Observation)
                .MaximumLength(250);

            RuleFor(p => p.AlternativeCode)
                .MaximumLength(50);
        }

        // Implementando metados de validação de documento usuando a biblioteca DocumentValidator;
        private bool ValidationCpfAndCnpj(string document) 
        {
            if (CpfValidation.Validate(document))
                return true;
           
            if (CnpjValidation.Validate(document))
                return true;

            return false;

        }

     




    }


}


