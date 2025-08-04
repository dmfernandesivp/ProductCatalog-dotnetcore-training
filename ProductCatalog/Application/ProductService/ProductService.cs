using Application.Dtos;
using Domain;
using Persistence.CategoryRepository;
using Persistence.ProductRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProductService;


    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            if (category == null)
                throw new ArgumentException($"Category with ID {product.CategoryId} not found");

            product.CreatedAt = DateTime.UtcNow;
            product.IsActive = true;

            var createdProduct = await _productRepository.CreateAsync(product);
            createdProduct.Category = category.Name;
            return createdProduct;
        }

        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
                throw new ArgumentException($"Product with ID {id} not found");

            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);
            if (category == null)
                throw new ArgumentException($"Category with ID {product.CategoryId} not found");

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.IsActive = product.IsActive;
            existingProduct.UpdatedAt = DateTime.UtcNow;

            var updatedProduct = await _productRepository.UpdateAsync(existingProduct);
            updatedProduct.Category = category.Name;
            return updatedProduct;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _productRepository.GetByCategoryAsync(categoryId);
        }
    }

