// -----------------------------------------------------------------------
// <copyright file="CsvGenerator.cs" company="Carlos Huchim Ahumada">
// Este código se libera bajo los términos de licencia especificados.
// </copyright>
// -----------------------------------------------------------------------
namespace Jaguar.Reporting.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Jaguar.Reporting;
    using Jaguar.Reporting.Common;

    /// <summary>
    /// Genera la información en formato CSV.
    /// </summary>
    public class CsvGenerator : IGeneratorEngine
    {
        /// <inheritdoc/>
        public Guid Id => new Guid("d6eb3d50-5d8e-42c4-bdb1-32f593d62bc4");

        /// <inheritdoc/>
        public string Name => "Archivo separado por comas CSV";

        /// <inheritdoc/>
        public string MimeType => "text/csv";

        /// <inheritdoc/>
        public string FileExtension => ".csv";

        /// <inheritdoc/>
        public bool IsEmbed => false;

        /// <inheritdoc/>
        public string GetString(ReportHandler report, List<DataTable> data, Dictionary<string, object> variables)
        {
            var sb = new StringBuilder();
            var data1 = data[0];

            if (data1.Columns.Count != 0)
            {
                var columns = new List<string>();

                foreach (var c in data1.Columns)
                {
                    columns.Add(c.Name);
                }

                sb.AppendLine(string.Join(",", columns.ToArray()));
            }

            if (data1.Rows.Count != 0)
            {
                foreach (var c in data1.Rows)
                {
                    var row = new List<object>();

                    foreach (var m in c.Columns)
                    {
                        var v = m.Value;

                        if (v is string)
                        {
                            row.Add(v.ToString().Replace("\n", string.Empty).Replace(",", string.Empty));
                        }
                        else
                        {
                            row.Add(m.Value);
                        }
                    }

                    sb.AppendLine(string.Join(",", row.ToArray()));
                }
            }

            return sb.ToString();
        }

        /// <inheritdoc/>
        public byte[] GetAllBytes(ReportHandler report, List<DataTable> data, Dictionary<string, object> variables)
        {
            return Encoding.UTF8.GetBytes(this.GetString(report, data, variables));
        }
    }
}