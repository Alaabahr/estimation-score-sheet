using EstimasionSS.DataContext;
using EstimasionSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Table;

namespace EstimasionSS.Controllers
{
    [Authorize]
    public class PlayersController : Controller
    {
        // GET: Players
        public ActionResult Index()
        {
            PlayerRepository repository = new PlayerRepository();

            try
            {
                // Create a new customer entity.
                string userName;

                if (User.Identity.IsAuthenticated)
                {
                    userName = User.Identity.Name;
                }
                else
                {
                    userName = "AnonymousUser";
                }
                AuditModel audit = new AuditModel(userName);
                audit.ActionTaken = "User entered Player>Index";

                AuditHelper.AddAudit(audit);

                var userId = User.Identity.GetUserId();
                var players = AutoMapper.Mapper.Map<List<PlayerModel>>(repository.GetByUserId(userId));
                return View(players);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // GET: Players/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Players/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Players/Create
        [HttpPost]
        public ActionResult Create(PlayerModel model)
        {
            try
            {
                // TODO: Add insert logic here
                PlayerRepository repository = new PlayerRepository();
                model.UserId = User.Identity.GetUserId();
                repository.Add(model);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Trace.TraceError(ex.Message);
                throw;
            }
        }

        // GET: Players/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Players/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Players/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Players/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
