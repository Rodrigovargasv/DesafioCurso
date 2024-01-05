using DocumentValidator;
using System.Text.RegularExpressions;

namespace DesafioCurso.Domain.Commons
{
    public static class UtilsValidations
    {
        public static bool ContainsWhitespace(string value)
        {
            if (value == null) return false;

            return Regex.IsMatch(value, @"^\s|\s$") ? true : false;
        }

        public static bool ValidationCpfAndCnpj(string document)
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