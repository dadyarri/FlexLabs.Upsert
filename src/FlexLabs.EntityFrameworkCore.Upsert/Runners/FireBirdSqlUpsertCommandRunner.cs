using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlexLabs.EntityFrameworkCore.Upsert.Internal;

namespace FlexLabs.EntityFrameworkCore.Upsert.Runners
{
    /// <summary>
    /// Upsert command runner for the Npgsql.EntityFrameworkCore.PostgreSQL provider
    /// </summary>
    public class FireBirdSqlUpsertCommandRunner : RelationalUpsertCommandRunner
    {
        /// <inheritdoc/>
        public override bool Supports(string providerName) => providerName == "FirebirdSql.EntityFrameworkCore.Firebird";
        /// <inheritdoc/>
        protected override string EscapeName(string name) => "\"" + name + "\"";
        /// <inheritdoc/>
        protected override string? SourcePrefix => "s.";
        /// <inheritdoc/>
        protected override string? TargetPrefix => "t.";
        /// <inheritdoc/>
        protected override int? MaxQueryParams => 1000;

        /// <inheritdoc/>
        public override string GenerateCommand(string tableName, ICollection<ICollection<(string ColumnName, ConstantValue Value, string DefaultSql, bool AllowInserts)>> entities,
            ICollection<(string ColumnName, bool IsNullable)> joinColumns, ICollection<(string ColumnName, IKnownValue Value)>? updateExpressions,
            KnownExpression? updateCondition)
        {
            var result = new StringBuilder();
            result.Append($"UPDATE OR INSERT INTO {tableName}");
            result.Append(" (");
            result.Append(string.Join(", ", entities.First().Select(e => EscapeName(e.ColumnName))));
            result.Append(") VALUES (");
            result.Append(string.Join("", entities.Select(ec => string.Join(", ", ec.Select(e => Parameter(e.Value.ArgumentIndex))))));
            result.Append(") MATCHING (");
            result.Append(string.Join(", ", joinColumns.Select(c => EscapeName(c.ColumnName))));
            result.Append(");");
            // if (updateExpressions != null)
            // {
            //     result.Append("UPDATE SET ");
            //     result.Append(string.Join(", ", updateExpressions.Select((e, i) => $"{EscapeName(e.ColumnName)} = {ExpandValue(e.Value)}")));
            //     if (updateCondition != null)
            //         result.Append($" WHERE {ExpandExpression(updateCondition)}");
            // }
            // else
            // {
            //     result.Append("NOTHING");
            // }
            return result.ToString();
        }
    }
}
