using Microsoft.AspNetCore.Mvc;
using PruebasUnitarias.Models;
using System.Collections.Generic;

namespace PruebasUnitarias.Controllers
{
    public class ProductController : Controller
    {
        // Lista estática de mensajes de contacto
        private static List<ContactMessage> contactMessages = new List<ContactMessage>
        {
            new ContactMessage { Name = "Juan", Email = "juan@ejemplo.com", Message = "Hola" }
        };

        // Lista estática de productos
        private static List<Product> products = new List<Product>
        {
            new Product { Name = "Producto 1", Description = "Descripción1", Price = 10 },
            new Product { Name = "Producto 2", Description = "Descripción2", Price = 15 }
        };

        // Acción GET para mostrar productos y mensajes de contacto
        public IActionResult Index()
        {
            // Pasamos ambos, productos y mensajes, a la vista
            var viewModel = new IndexViewModel
            {
                Products = products,
                ContactMessages = contactMessages
            };

            return View(viewModel);
        }

        // Acción GET para mostrar el formulario de creación de producto
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Acción POST para guardar el producto
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                products.Add(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // Acción GET para mostrar el formulario de contacto
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        // Acción POST para guardar el mensaje de contacto
        [HttpPost]
        public IActionResult Contact(ContactMessage contactMessage)
        {
            if (ModelState.IsValid)
            {
                contactMessages.Add(contactMessage);
                return RedirectToAction("Index");
            }
            return View(contactMessage);
        }
    }
}
