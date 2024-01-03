

using DocumentValidator;

namespace DesafioCurso.Domain.Commons
{
    public static class Utils
    {
        public static bool ContainsWhitespace(string value)
        {
            return value?.Contains(" ") == true;
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
