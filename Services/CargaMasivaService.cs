using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Seguros.Interfaces;
using Seguros.Migrations;
using Seguros.Modelo;
using System;

namespace Seguros.Services
{
    public class CargaMasivaService : ICargaMasivaService
    {
        private readonly ApplicationDbContext _context;

        public CargaMasivaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asegurado>> CargarAseguradosDesdeArchivoAsync(IFormFile archivo)
        {
            var asegurados = new List<Asegurado>();

            using (var stream = new MemoryStream())
            {
                await archivo.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        var asegurado = new Asegurado
                        {
                            Cedula = worksheet.Cells[row, 1].Value?.ToString(),
                            NombreCliente = worksheet.Cells[row, 2].Value?.ToString(),
                            Telefono = worksheet.Cells[row, 3].Value?.ToString(),
                            Edad = int.Parse(worksheet.Cells[row, 4].Value?.ToString())
                        };

                        asegurados.Add(asegurado);
                    }
                }
            }

            // Ahora puedes guardar los asegurados en la base de datos si es necesario
            // _context.Asegurados.AddRange(asegurados);
            // await _context.SaveChangesAsync();

            return asegurados;
        }
    }
}