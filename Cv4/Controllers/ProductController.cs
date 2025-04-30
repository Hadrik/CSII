using Cv4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Cv4.Controllers;

public class ProductController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ViewBag.CartController = HttpContext.Session.GetString("cart");
        base.OnActionExecuting(context);
    }

    public IActionResult Index(ProductService productService)
    {
        ViewBag.Products = productService.List();
        return View();
    }

    public IActionResult Detail(int id, ProductService productService)
    {
        var p = productService.GetProduct(id);
        if (p == null)
        {
            return NotFound();
        }
        
        ViewBag.Product = p;
        return View();
    }

    [HttpPost]
    public IActionResult AddToCart(int id)
    {
        var cart = HttpContext.Session.GetString("cart");

        var ids = cart is null ? [] : cart.Split(',').Select(int.Parse).ToList();
        ids.Add(id);
        HttpContext.Session.SetString("cart", string.Join(',', ids));
        
        return RedirectToAction("Detail", new {id = id});
    }

    public IActionResult Order()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Order(OrderFormViewModel form)
    {
        return View();
    }
}