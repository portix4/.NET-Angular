using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using WebAPIProductos.Models;

using Microsoft.AspNetCore.Cors;

namespace WebAPIProductos.Controllers
{
    [EnableCors("Cors_Rules")]
    [Route("api")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public readonly DBAPIContext dbAPIContext;

        public ProductoController(DBAPIContext context)
        {
            dbAPIContext = context;
        }

        [HttpGet]
        [Route("GetAll")]

        public IActionResult GetAll()
        {
            List<Producto> lista = new List<Producto>();
            try
            {
                lista = dbAPIContext.Productos.Include(e => e.objCategoria).ToList();
                return StatusCode(StatusCodes.Status200OK, new {mensaje = "Request Success", response = lista});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });
            }
        }

        [HttpGet]
        [Route("GetOne/{productId:int}")]
        public IActionResult GetOne(int productId)
        {
            Producto objProducto = dbAPIContext.Productos.Find(productId);

            if (objProducto == null) 
                return BadRequest("Product Not found");
            try
            {
                objProducto = dbAPIContext.Productos.Include(e => e.objCategoria).Where(p => p.IdProducto==productId).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Request Success", response = objProducto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = objProducto });
            }
        }

        [HttpPost]
        [Route("Save")]
        public IActionResult SaveOne([FromBody] Producto obj)
        {
            try
            {
                dbAPIContext.Productos.Add(obj);
                dbAPIContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Data saved" });
            } 
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { ex.Message });
            }
        }

        [HttpPut]
        [Route("EditOne")]
        public IActionResult EditOne([FromBody] Producto obj)
        {
            Producto objProducto = dbAPIContext.Productos.Find(obj.IdProducto);

            if (objProducto == null)
                return BadRequest("Product Not found");
            try
            {
                objProducto.CodigoBarra = obj.CodigoBarra is null ? objProducto.CodigoBarra : obj.CodigoBarra;
                objProducto.Descripcion = obj.Descripcion is null ? objProducto.Descripcion : obj.Descripcion;
                objProducto.Marca = obj.Marca is null ? objProducto.Marca : obj.Marca;
                objProducto.IdCategoria = obj.IdCategoria is null ? objProducto.IdCategoria : obj.IdCategoria;
                objProducto.Precio = obj.Precio is null ? objProducto.Precio : obj.Precio;


                dbAPIContext.Productos.Update(objProducto);
                dbAPIContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Data saved" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { ex.Message });
            }
        }
        [HttpDelete]
        [Route("DeleteOne/{productId:int}")]
        public IActionResult DeleteOne(int productId)
        {
            Producto objProducto = dbAPIContext.Productos.Find(productId);

            if (objProducto == null)
                return BadRequest("Product Not found");
            try
            {
                dbAPIContext.Productos.Remove(objProducto);
                dbAPIContext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Data delete" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status200OK, new { ex.Message });
            }
        }
    }
}
