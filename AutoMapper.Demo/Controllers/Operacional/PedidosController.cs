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
    public class PedidosController : Controller
    {
        private ContextoDB db = new ContextoDB();

        // GET: Pedidos
        public ActionResult Index()
        {

            //var pedidos = db.Pedidos.Include("LinhaPedido").ToList();



            // Exemplo - Automapeamento de Campos Nomes diferentes.
            // Versão 3.11
            //Mapper.CreateMap<Pedido, PedidoDTO>()
            //    .ForMember(d => d.NomeCliente, o => o.MapFrom(p=>p.Cliente.PegaNome()))
            //    .ForMember(d => d.Total, o => o.MapFrom(p => p.TotalPedido()))   
            //    .ForMember(d=>d.Entregar, o=>o.ResolveUsing<ResolvedorBooleano>())
            //    //.ForMember(d => d.LinhaPedido, o => o.MapFrom(p => p.LinhaPedido))
            //    .ForMember(d => d.NrPedido, o => o.MapFrom(p=>p.NumeroPedido));

            // Mapper.CreateMap<ItensPedido, ItensPedidoDTO>();

            //var model = Mapper.Map<IEnumerable<Pedido>, IEnumerable<PedidoDTO>>(pedidos);

            // Versão 5.1.1
            


            var pedidos = db.Pedidos.ToList();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Pedido, PedidoDTO>()
                .ForMember(d => d.NomeCliente, o => o.MapFrom(p => p.Cliente.PegaNome()))                
                .ForMember(d => d.Total, o => o.MapFrom(p => p.TotalPedido()))
                .ForMember(d => d.Entregar, o => o.ResolveUsing<ResolvedorBooleano>())
                .ForMember(d => d.NrPedido, o => o.MapFrom(p => p.NumeroPedido));

                cfg.CreateMap<ItensPedido, ItensPedidoDTO>();
            });

           
            var model = config.CreateMapper().Map<IList<Pedido>, IList<PedidoDTO>>(pedidos);
           
            return View(model);

            //return View(db.Pedidos.ToList());
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PedidoId,NumeroPedido,DataCompra,Entregar,InternalId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Pedidos.Add(pedido);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PedidoId,NumeroPedido,DataCompra,Entregar,InternalId")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedido);
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
