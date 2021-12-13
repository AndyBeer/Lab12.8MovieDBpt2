using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PracticeLab12._8.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace PracticeLab12._8.Controllers
{
    public class HomeController : Controller
    {
        public MoviedbDAL MovieDB = new MoviedbDAL(); //this is creating an instance of that class, to call those DAL layer functions/methods/actions
        public List<Movie> MovieResults = new List<Movie>();//Will this help w scope between methods?


        public IActionResult Index()
        {
            List<Movie> movies = MovieDB.GetMovies();
            // calling the method from the DAL layer to create/read the movies list
            //We passed in an IEnumerable into the Index here because that will allow ANY type of collection (it is parent data type of any collection)
            return View(movies);
        }
        public IActionResult Details(int ID)
        {
            Movie m = MovieDB.GetMovie(ID);
            return View(m);
        }
        public IActionResult Delete(int ID)
        {
            //This will show full details of what we intend to delete
            //We can ask the user if they are sure do remove before we do that
            Movie m = MovieDB.GetMovie(ID);
            return View(m);
        }

        public IActionResult DeleteFromDb(int ID)
        {
            MovieDB.DeleteMovie(ID);
            return RedirectToAction("Index", "Home");
        }

        //We are copying the code from the Details action - we will display the view as well as pass the movie to the form

        public IActionResult Update(int ID)
        {
            Movie m = MovieDB.GetMovie(ID);
            return View(m);
        }
        [HttpPost]
        public IActionResult Update(Movie m)
        {
            if (ModelState.IsValid)
            {
                MovieDB.UpdateMovie(m);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(m);
            }
        }

        //we dont have an existing movie object to work off of so we just need to display a view
        public IActionResult Create()
        {
            return View();
        }
        //This is also similar to the Update methods - since we will use a form here as well, we can reuse a lot.
        //Forms will typically use an HTTPPOST
        [HttpPost]
        public IActionResult Create(Movie m) //ModelState.IsValid checks the model against the Data Annotations. Bool - returns true if all rules met.
            //This is a way to validate and only allow valid objects to be created (or edited, etc.)
            //If the model is good, it goes to DB in this situation and jump back to index.
            //If the model is bad, it jumps back to the same page as before.
        {
            if (ModelState.IsValid)
            {
                MovieDB.CreateMovie(m);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(m);
            }
        }

        public IActionResult Search()
        {

            return View();
        }
     [HttpPost]
        public IActionResult SearchTitle(string title)
        {
            List<Movie> searchResults = MovieDB.GetMoviesByTitle(title);
            if (ModelState.IsValid)
            {
                MovieList.Movies = searchResults;
                return RedirectToAction("SearchResults", "Home", MovieList.Movies); ;
            }
            else
            {
                //How do I show that the search returned no results?
                return View();
            }
        }
        [HttpPost]
        public IActionResult SearchByGenre(string searchGenre)
        {
            List<Movie> searchResults = MovieDB.GetMoviesByGenre(searchGenre);
            if (ModelState.IsValid)
            {
                MovieList.Movies = searchResults;
                return RedirectToAction("SearchResults", "Home", MovieList.Movies); ;
            }
            else
            {
                //How do I show that the search returned no results?
                return View();
            }
        }
        [HttpPost]
        public IActionResult SearchByID(int searchID)
        {
            return RedirectToAction("Details", "Home", searchID);
        }
        public IActionResult SearchResults()
        {
            return View(MovieList.Movies);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
