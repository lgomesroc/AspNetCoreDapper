using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreDapper.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreDapper.Repositories
{
    public class ProductController : Controller
    {
        private readonly ProductRepository productRepository;

        public ProductController(IConfiguration configuration){
            productRepository = new ProductRepository(configuration);
        }

        // GET: Products
        public ActionResult Index()
        {
            return View(productRepository.FindAll().ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            Product product = productRepository.FindByID(id.Value);
            if (product == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Name,Quantity,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.Add(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            Product product = productRepository.FindByID(id.Value);
            if (product == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,Name,Quantity,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.Update(product);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            Product product = productRepository.FindByID(id.Value);
            if (product == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            productRepository.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
