using Facturacion.Application.Dtos.Producto;
using Facturacion.Application.Extensions;
using Facturacion.Application.Interfaces;
using Facturacion.Domain.Interfaces;

namespace Facturacion.Application.UseCases
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;
        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<List<ProductoDto>> GetAllAsync()
        {
            var productos = await _productoRepository.GetAllAsync();
            return productos.Select(p => p.ToDto()).ToList();
        }

        public async Task<ProductoDto> GetByIdAsync(int id)
        {
            var producto = await _productoRepository.GetByIdAsync(id);
            return producto.ToDto();
        }

        public async Task<List<ProductoDto>> SearchAsync(string nombre)
        {
            var productos = await _productoRepository.SearchAsync(nombre);
            return productos.Select(p => p.ToDto()).ToList();
        }

        public async Task<ProductoDto> CreateAsync(CreateProductoDto productoDto)
        {
            var producto = await _productoRepository.CreateAsync(productoDto.ToEntity());
            return producto.ToDto();
        }

        public async Task<ProductoDto> UpdateAsync(int id, UpdateProductoDto productoDto)
        {
            var producto = await _productoRepository.UpdateAsync(id, productoDto.ToEntity());
            return producto.ToDto();
        }

        public async Task DeleteAsync(int id)
        {
            await _productoRepository.DeleteAsync(id);
        }
    }
}
