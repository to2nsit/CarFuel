using CarFuel.Models;
using CarFuel.Services;
using CarFuel.Services.Bases;
using CarFuel.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarFuel.Web.Controllers
{
  public class CarsController : AppControllerBase
  {
    private readonly ICarService _carService;

    public CarsController(ICarService carService, IUserService userService) 
            : base(userService) { // call parent contructor
      _carService = carService;
    }


        // GET: Cars
        public ActionResult Index() {
            if (User.Identity.IsAuthenticated) {
                var cars = _carService.All();

                ViewBag.AppUser = _userService.CurrentUser;

                return View("IndexForMember", cars);
            }
            else {
                return View("IndexForAnonymous");
            }
        }

        public ActionResult Add() {
      return View();
    }

    [HttpPost]
    public ActionResult Add(Car c) {

      ModelState.Remove("Owner");
      if (ModelState.IsValid) {

        User u = _userService.Find(new Guid(User.Identity.GetUserId()));
        c.Owner = u;

        _carService.Add(c);
        _carService.SaveChanges();

        return RedirectToAction("Index");
      }
      return View(c);
    }

    public ActionResult AddFillUp(Guid id) {
      //var car = db.Cars.Find(id);
      //if(car == null) {
      //  return HttpNotFound();
      //}
      var q = _carService.Find(id).Name;
      //var name = q.SingleOrDefault();

      ViewBag.CarName = q;
      return View();
    }

    [HttpPost]
    public ActionResult AddFillUp(Guid id, FillUp item) {

      ModelState.Remove("Id");

      if (ModelState.IsValid) {

        var car = _carService.Find(id);
        //db.Entry(car).Collection(x => x.FillUps).Load();  // Manual load,  if don't have virtual(FillUps) 
        // FillUp  = >   db.Entry(car).Reference(x => x.NextFillUp).Load();
        car.AddFillUp(item.Odometer, item.Liters);

        _carService.SaveChanges();

        return RedirectToAction("Index");
      }
      return View();
    }


  }
}