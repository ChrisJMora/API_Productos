﻿using APIProductos.Data;
using APIProductos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIProductos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        private readonly ApplicationDBContext _db;
        
        public ProductoController(ApplicationDBContext db)
        {
            _db = db;
        }

        // GET: api/<ProductoController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Producto> productos = await _db.Producto.ToListAsync();
            return Ok(productos);
        }

        // GET api/<ProductoController>/5
        [HttpGet("{idProducto}")]
        public async Task<IActionResult> Get(int idProducto)
        {
            Producto producto = await _db.Producto.FirstOrDefaultAsync(x => x.IdProducto == idProducto);
            if (producto == null)
            {
                return BadRequest();
            }
            return Ok(producto);
        }

        // POST api/<ProductoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Producto producto)
        {
            Producto producto2 = await _db.Producto.FirstOrDefaultAsync(x => x.IdProducto == producto.IdProducto);
            if (producto2 == null && producto != null)
            {
                await _db.Producto.AddAsync(producto);
                await _db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest("El objeto ya existe");
        }

        // PUT api/<ProductoController>/5
        [HttpPut("{idProducto}")]
        public async Task<IActionResult> Put(int idProducto, [FromBody] Producto producto)
        {
            Producto producto2 =await _db.Producto.FirstOrDefaultAsync(x => x.IdProducto == idProducto);
            if (producto2!= null)
            {
                producto2.Nombre = producto.Nombre != null ? producto.Nombre : producto2.Nombre;
                producto2.Descripcion = producto.Descripcion != null ? producto.Descripcion : producto2.Descripcion;
                producto2.cantidad = producto.Descripcion != null ? producto.cantidad : producto2.cantidad;
                _db.Producto.Update(producto2);
                await _db.SaveChangesAsync();
                return Ok(producto2);
            }
            return BadRequest("El producto no existe");
        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{idProducto}")]
        public async Task<IActionResult> Delete(int idProducto)
        {
            Producto producto = await _db.Producto.FirstOrDefaultAsync(x => x.IdProducto == idProducto);
            if (producto != null)
            {
                _db.Producto.Remove(producto);
                await _db.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }
    }
}
