﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using SuperMarket.API.Domain.Models;
using SuperMarket.API.Domain.Repositories;
using SuperMarket.API.Persistence.Contexts;

namespace SuperMarket.API.Persistence.Repositories
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> ListAsync()
        {
            return await _context.Products.Include(p=>p.Category)
                .ToListAsync();
        }


        public async Task AddAsync(Product product)
        {
           var result = await _context.Products.AddAsync(product);
        }

        public async Task<Product> FindByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
        public void Remove(Product product)
        {
            _context.Products.Remove(product);
        }
    }
}
