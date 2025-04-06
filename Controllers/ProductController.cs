using Microsoft.AspNetCore.Mvc;
using PruebasUnitarias.Models;
using System.Collections.Generic;

namespace PruebasUnitarias.Controllers
{
    public class ProductController : Controller
    {
        // Lista est�tica de mensajes de contacto
        private static List<ContactMessage> contactMessages = new List<ContactMessage>
        {
            new ContactMessage { Name = "Juan", Email = "juan@ejemplo.com", Message = "Hola" }
        };

        // Lista est�tica de productos
        public static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Producto 1", Description = "Descripci�n1", Price = 10 },
            new Product { Id = 2, Name = "Producto 2", Description = "Descripci�n2", Price = 15 }
        };

        // Acci�n GET para mostrar productos y mensajes de contacto
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

        // Acci�n GET para mostrar el formulario de creaci�n de producto
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Acci�n POST para guardar el producto
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = products.Count > 0 ? products[^1].Id + 1 : 1;
                // Asignar un ID �nico al producto
                products.Add(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // Acci�n GET para mostrar el formulario de contacto
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        // Acci�n POST para guardar el mensaje de contacto
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
