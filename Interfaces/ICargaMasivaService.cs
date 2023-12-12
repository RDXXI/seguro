using Seguros.Modelo;

namespace Seguros.Interfaces
{
    public interface ICargaMasivaService
    {
        Task<IEnumerable<Asegurado>> CargarAseguradosDesdeArchivoAsync(IFormFile archivo);
    }
}