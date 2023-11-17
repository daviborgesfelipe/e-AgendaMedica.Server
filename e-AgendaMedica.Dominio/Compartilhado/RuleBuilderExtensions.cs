using e_AgendaMedica.Dominio.ModuloAtividade;
using FluentValidation;

namespace e_AgendaMedica.Dominio.Compartilhado
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T, string> CRM<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                .Matches(@"(\(?\d{2}\)?\s)?(\d{4,5}\-(AC|AL|AP|AM|BA|CE|DF|ES|GO|MA|MT|MS|MG|PA|PB|PR|PE|PI|RJ|RN|RS|RO|RR|SC|SP|SE|TO))");

            return options;
        }
    }
}
