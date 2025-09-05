using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using InmobiliariaMVC.Models;

namespace InmobiliariaMVC.Controllers
{
    public class InquilinosController : Controller
    {
        private readonly RepositorioInquilino _repo;

        public InquilinosController(RepositorioInquilino repo)
        {
            _repo = repo;
        }

        // GET: Inquilinos
        public async Task<IActionResult> Index()
        {
            var lista = _repo.ObtenerTodos();
            return View(lista);

        }

        // GET: Inquilinos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inquilino = _repo.ObtenerPorId(id.Value);
            if (inquilino == null)
            {
                return NotFound();
            }

            return View(inquilino);
        }

        // GET: Inquilinos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inquilinos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInquilino,Nombre,Apellido,Dni,Telefono,Email")] Inquilino inquilino)
        {
            if (ModelState.IsValid)
            {
                _repo.Alta(inquilino);
                return RedirectToAction(nameof(Index));
            }
            return View(inquilino);
        }

        // GET: Inquilinos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inquilino = _repo.ObtenerPorId(id.Value);

            if (inquilino == null)
            {
                return NotFound();
            }
            return View(inquilino);
        }

        // POST: Inquilinos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInquilino,Nombre,Apellido,Dni,Telefono,Email")] Inquilino inquilino)
        {
            if (id != inquilino.IdInquilino)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool editado = _repo.Editar(inquilino); // que devuelva true si se editó correctamente

                if (!editado)
                {
                    return NotFound(); // o mensaje de error personalizado
                }

                return RedirectToAction(nameof(Index));
            }

            return View(inquilino);
        }

        // GET: Inquilinos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inquilino = _repo.ObtenerPorId(id.Value);
            if (inquilino == null)
            {
                return NotFound();
            }

            return View(inquilino);
        }

        // POST: Inquilinos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _repo.Eliminar(id);
            return RedirectToAction(nameof(Index));

        }

        private bool InquilinoExists(int id)
        {
            return _repo.ObtenerPorId(id) != null;
        }
    }
}
