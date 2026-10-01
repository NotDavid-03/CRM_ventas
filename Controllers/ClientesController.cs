using System;
using System.Web.Mvc;
using CRM_Nexus_Retail.Models;
using CRM_Nexus_Retail.DataAccess;

namespace CRM_Nexus_Retail.Controllers
{
    public class ClientesController : Controller
    {
        private readonly Nexus_ClienteDAL _accesoClientes = new Nexus_ClienteDAL();

        // ── GET: /Clientes ──
        public ActionResult Index()
        {
            var listaClientes = _accesoClientes.ListarTodos();
            return View(listaClientes);
        }

        // ── GET: /Clientes/Create ──
        public ActionResult Create()
        {
            return View(new Omni_Cliente());
        }

        // ── POST: /Clientes/Create ──
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Omni_Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _accesoClientes.Registrar(cliente);
                    TempData["Exito"] = "Cliente registrado correctamente.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al registrar: " + ex.Message);
                }
            }

            return View(cliente);
        }

        // ── GET: /Clientes/Edit/5 ──
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var cliente = _accesoClientes.ConsultarPorId(id.Value);

            if (cliente == null)
            {
                return HttpNotFound();
            }

            return View(cliente);
        }

        // ── POST: /Clientes/Edit ──
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Omni_Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _accesoClientes.Modificar(cliente);
                    TempData["Exito"] = "Cliente actualizado correctamente.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error al actualizar: " + ex.Message);
                }
            }

            return View(cliente);
        }

        // ── GET: /Clientes/Delete/5 ──
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            try
            {
                _accesoClientes.DarDeBaja(id.Value);
                TempData["Exito"] = "Cliente dado de baja correctamente.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al dar de baja: " + ex.Message;
            }

            return RedirectToAction("Index");
        }

        // ── GET: /Clientes/Details/5 ──
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var cliente = _accesoClientes.ConsultarPorId(id.Value);

            if (cliente == null)
            {
                return HttpNotFound();
            }

            return View(cliente);
        }
    }
}