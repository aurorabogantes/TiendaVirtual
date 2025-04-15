using Microsoft.AspNetCore.Mvc;
using PruebasUnitarias.Models;
using System.Collections.Generic;
using System.Linq;

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
        public static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Producto 1", Description = "Descripción1", Price = 10 },
            new Product { Id = 2, Name = "Producto 2", Description = "Descripción2", Price = 15 }
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
            if (product.Description != null && product.Description.Length > 500)
            {
                ModelState.AddModelError("Description", "La descripción no puede tener más de 500 caracteres.");
            }

            if (ModelState.IsValid)
            {
                product.Id = products.Any() ? products.Max(p => p.Id) + 1 : 1;
                // Asignar un ID único al producto
                products.Add(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // Acción GET para cargar la vista de actualización de un producto
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

        // Acción POST para actualizar un producto existente
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
                ModelState.AddModelError("Description", "La descripción no puede tener más de 500 caracteres.");
            }

            if (!ModelState.IsValid)
            {
                return View(updatedProduct);
            }

            // Actualizar las propiedades del producto existente
            existingProduct.Name = updatedProduct.Name;
            existingProduct.Description = updatedProduct.Description;
            existingProduct.Price = updatedProduct.Price;

            // Redirigir al índice después de la actualización
            return RedirectToAction("Index");
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
                TempData["SuccessMessage"] = "El formulario de contacto se envió correctamente.";
                //return RedirectToAction("Index");
            }
            return View(contactMessage);
        }
    }
}
