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
    public class VendasController : Controller
    {
        private ContextoDB db = new ContextoDB();

        // GET: Vendas
        public ActionResult Index()
        {
            var vendas = db.ItensPedidos.ToList();

            var config = new MapperConfiguration(cfg => cfg.CreateMap<ItensPedido, ItensPedidoDTO>()
                .ForMember(d => d.Cliente, o => o.MapFrom(p => p.Pedido.Cliente.PegaNome()))
                .ForMember(d => d.Total, o => o.MapFrom(p => p.PrecoTotal()))
            );

            //Mapper.CreateMap<ItensPedido, ItensPedidoDTO>()
            //    .ForMember(d => d.Cliente, o => o.MapFrom(p => p.Pedido.Cliente.PegaNome()))
            //    .ForMember(d => d.Total, o => o.MapFrom(p => p.PrecoTotal()));

            var model = Mapper.Map<IEnumerable<ItensPedido>, IEnumerable<ItensPedidoDTO>>(vendas);

            return View(model);
        }

        // GET: Vendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItensPedido itensPedido = db.ItensPedidos.Find(id);
            if (itensPedido == null)
            {
                return HttpNotFound();
            }
            return View(itensPedido);
        }

        // GET: Vendas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vendas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemId,Preco,Produto,Quantidade")] ItensPedido itensPedido)
        {
            if (ModelState.IsValid)
            {
                db.ItensPedidos.Add(itensPedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(itensPedido);
        }

        // GET: Vendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItensPedido itensPedido = db.ItensPedidos.Find(id);
            if (itensPedido == null)
            {
                return HttpNotFound();
            }
            return View(itensPedido);
        }

        // POST: Vendas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemId,Preco,Produto,Quantidade")] ItensPedido itensPedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itensPedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(itensPedido);
        }

        // GET: Vendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItensPedido itensPedido = db.ItensPedidos.Find(id);
            if (itensPedido == null)
            {
                return HttpNotFound();
            }
            return View(itensPedido);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItensPedido itensPedido = db.ItensPedidos.Find(id);
            db.ItensPedidos.Remove(itensPedido);
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
