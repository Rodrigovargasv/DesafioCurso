using DocumentValidator;
using System.Text.RegularExpressions;

namespace DesafioCurso.Domain.Commons
{
    public static class UtilsValidations
    {
        public static bool ContainsWhitespace(string value)
        {
            // verificar se o valor passado contém espaço em branco no começo ou no final da informação
            if (value == null) return false;

            return Regex.IsMatch(value, @"^\s|\s$") ? true : false;
        }

        // Faz a validação de CPF e CNPJ
        public static bool ValidationCpfAndCnpj(string document)
        {
            if (CpfValidation.Validate(document.Replace(".", "").Replace("-", "").Replace("/", "")))
                return true;

            if (CnpjValidation.Validate(document.Replace(".", "").Replace("-", "").Replace("/", "")))
                return true;

            return false;
        }
    }
}