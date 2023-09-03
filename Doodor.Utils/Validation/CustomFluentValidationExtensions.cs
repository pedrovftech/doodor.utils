using FluentValidation;

namespace Doodor.Utils.Utilities.Validation
{
    public static class CustomFluentValidation
    {
        public static IRuleBuilderOptions<T, string> ValidarDocumento<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .Must((x => (x ?? string.Empty).Length.Equals(11) ?
                    IsCpf(x) :
                    IsCnpj(x)));

            return options;
        }


        public static IRuleBuilderOptions<T, string> IsNumeric<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .Matches(x => RegexIsNumeric());

            return options;
        }

        public static IRuleBuilderOptions<T, string> IsAlphaNumeric<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
              .Matches(x => RegexIsAlphaNumeric());

            return options;
        }

        public static IRuleBuilderOptions<T, string> IsTelefone<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
              .Matches(x => RegexIsTelefone());

            return options;
        }

        private static string RegexIsTelefone() => @"^([0-9]{2})([9]{1})?([0-9]{4})([0-9]{4})$";
        private static string RegexIsNumeric() => @"^[0-9]*$";
        private static string RegexIsAlphaNumeric() => @"^[a-zA-Z0-9 à-úÀ-Ú-_'.!?,:;\n/()%]*$";

        private static bool IsCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;

            for (int j = 0; j < 10; j++)
                if (j.ToString().PadLeft(11, char.Parse(j.ToString())) == cpf)
                    return false;

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto;

            return cpf.EndsWith(digito);
        }

        private static bool IsCnpj(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
                return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim().Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto;

            return cnpj.EndsWith(digito);
        }
    }
}