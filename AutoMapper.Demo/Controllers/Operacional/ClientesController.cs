using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper.Demo.Models.Contexto;
using AutoMapper.Demo.Models.Dominio;
using AutoMapper.Demo.ViewModel;

namespace AutoMapper.Demo.Controllers.Operacional
{
    public class ClientesController : Controller
    {
        private ContextoDB db = new ContextoDB();

        // GET: Clientes
        public ActionResult Index()
        {
           // var clientes = db.Clientes.Where(c=>c.Nome.StartsWith("S")).ToList();
             var clientes = db.Clientes.ToList();

            //AutoMapper.Mapper.CreateMap<Cliente, ClienteDTO>();
            //var model = AutoMapper.Mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteDTO>>(clientes);


            // Exemplo - Automapeamento de Campos nulos para valor padrão.

            // Versão 3.1.1
            //AutoMapper.Mapper.CreateMap<Cliente, ClienteDTO>()
            //    .ForMember(d => d.Bio, o => o.NullSubstitute("Não foi informado."))
            //    .ForMember(d => d.NomeCompleto, o => o.MapFrom(p => p.PegaNome()));

            // Versão 5.1.1

            var config = new MapperConfiguration(
                cfg => cfg.CreateMap<Cliente, ClienteDTO>()
                .ForMember(d => d.Bio, o => o.NullSubstitute("Não foi informado."))
                .ForMember(d => d.NomeCompleto, o => o.MapFrom(p => p.PegaNome()))
                );

            var model = config.CreateMapper().Map<IList<Cliente>, IList<ClienteDTO>>(clientes);
          


            return View(model);
        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClienteId,Bio,Nome,Sobrenome")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClienteId,Bio,Nome,Sobrenome")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
