﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IHFF.Models;
using IHFF.Repositories;
using IHFF.Interfaces;

namespace IHFF.Controllers
{
    public class RestaurantController : Controller
    {
        private IRestaurantRepository restaurantsRepository = new RestaurantsRepository();
        private IWishlistRepository wishlistRepository = new WishlistRepository();

        public ActionResult Index()
        {
           return View(restaurantsRepository.GetAllRestaurants());
        }

        [HttpPost]
        public ActionResult Index(int Datum, int Tijd, int Amount, int RestaurantId)
        {
            Restaurant restaurant = restaurantsRepository.GetRestaurant(RestaurantId);

            restaurantsRepository.CreateReservation(restaurant, Amount, Tijd, Datum);
            return RedirectToAction("Index", "Wishlist");
        }

        public ActionResult RestaurantInfo(int EventId)
        {
            return View(restaurantsRepository.GetRestaurant(EventId));
        }
    }
}