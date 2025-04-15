using Microsoft.AspNetCore.Mvc;
using PruebasUnitarias.Models;
using System.Collections.Generic;
using System.Linq;

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
            if (product.Description != null && product.Description.Length > 500)
            {
                ModelState.AddModelError("Description", "La descripci�n no puede tener m�s de 500 caracteres.");
            }

            if (ModelState.IsValid)
            {
                product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
                // Asignar un ID �nico al producto
                products.Add(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // Acci�n GET para cargar la vista de actualizaci�n de un producto
        [HttpGet]
        public IActionResult Update(int id)
        {
            // Buscar el producto existente por su ID
            var existingProduct = products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                // Si no se encuentra el producto, devolver un error 404
                return NotFound($"El producto con ID {id} no existe.");
            }

            // Pasar el producto encontrado a la vista
            return View(existingProduct);
        }

        // Acci�n POST para actualizar un producto existente
        [HttpPost]
        public IActionResult Update(Product updatedProduct)
        {
            // Buscar el producto existente por su ID
            var existingProduct = products.FirstOrDefault(p => p.Id == updatedProduct.Id);

            if (existingProduct == null)
            {
                // Si no se encuentra el producto, devolver un error 404
                return NotFound($"El producto con ID {updatedProduct.Id} no existe.");
            }

            // Validar el modelo
            if (updatedProduct.Description != null && updatedProduct.Description.Length > 500)
            {
                ModelState.AddModelError("Description", "La descripci�n no puede tener m�s de 500 caracteres.");
            }

            if (!ModelState.IsValid)
            {
                return View(updatedProduct);
            }

            // Actualizar las propiedades del producto existente
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;

            // Redirigir al �ndice despu�s de la actualizaci�n
            return RedirectToAction("Index");
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
                TempData["SuccessMessage"] = "El formulario de contacto se envi� correctamente.";
                //return RedirectToAction("Index");
            }
            return View(contactMessage);
        }
    }
}
