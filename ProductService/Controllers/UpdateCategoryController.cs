using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Database;
using ProductService.Database.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.Controllers
{
    [Route("api/product/updateCategory")]
    [ApiController]
    public class UpdateCategoryController : ControllerBase
    {
        DatabaseContext db;
        public UpdateCategoryController()
        {
            db = new DatabaseContext();
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return db.Products.ToList();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]Product model)
        {
            try
            {
                var myProduct = db.Products.FirstOrDefault(e => e.ProductID == id);
                myProduct.CategoryId = model.CategoryId;
                myProduct.CategoryName = model.CategoryName;
                db.Products.Update(myProduct);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status202Accepted);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Product with Id = " + id.ToString() + " not found to update");
            }
        }

    }
}
