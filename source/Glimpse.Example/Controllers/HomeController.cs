﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Glimpse.Net.Extensibility;
using MvcMusicStore.Models;

namespace MvcMusicStore.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        MusicStoreEntities storeDB = new MusicStoreEntities();

        public ActionResult Index()
        {
            // Get most popular albums
            var albums = GetTopSellingAlbums(5);

            Trace.Write("Got top 5 albums");

            using (GlimpseTrace.Time("It takes {t:g} to trace (which is {t:ss} seconds)"))
            {

                GlimpseTrace.Info("This is info from Glimpse");
                GlimpseTrace.Warn("This is warn from Glimpse at {0}", DateTime.Now);
                GlimpseTrace.Error("This is error from {0}", GetType());
                GlimpseTrace.Fail("This is Fail from Glimpse");

            }


            TempData["Test"] = "A bit of temp";
            
            return View(albums.ToList());
        }

        private List<Album> GetTopSellingAlbums(int count)
        {
            // Group the order details by album and return
            // the albums with the highest count

            return storeDB.Albums
                .OrderByDescending(a => a.OrderDetails.Count())
                .Take(count)
                .ToList();
        }

        public ActionResult News()
        {
            var views = new[]{"One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight"};

            var randomIndex = new Random().Next(0, views.Count());

            Trace.Write("Randomly selected story number " + randomIndex);

            return PartialView(views[randomIndex]);
        }
    }
}