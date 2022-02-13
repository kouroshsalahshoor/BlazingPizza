using BlazingPizza.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace BlazingPizza.Server.Controllers
{
    [Route("pizzas")]
    [ApiController]
    public class PizzasController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PizzasController(ApplicationDbContext db)
        {
            _db = db;
        }
    }
}
