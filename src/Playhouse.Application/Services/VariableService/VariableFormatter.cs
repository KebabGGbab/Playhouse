using System.Text.RegularExpressions;
using Playhouse.Domain;

namespace Playhouse.Application.Services.VariableService
{
    internal static partial class VariableFormatter
    {
        public static string Format(string format, IEnumerable<Variable> variables)
        {
            ArgumentNullException.ThrowIfNull(format);
            ArgumentNullException.ThrowIfNull(variables);

            return VariableRegex().Replace(format, (match) =>
            {
                Variable? variable = variables.FirstOrDefault(v => v.Name == match.Groups["Name"].Value);

                return variable == null ? match.Value : variable.Value;
            });
        }

        [GeneratedRegex(@"\${(?<Name>[^{}]+)}")]
        private static partial Regex VariableRegex();
    }
}
