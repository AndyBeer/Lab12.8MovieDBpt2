using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace PracticeLab12._8.Models
{
    public class MoviedbDAL
    {
        //created this Data Access Layer as a standalone connection place, instead of putting this in the controller(s).
        //Contains ALL of our code for talking to the DB, so this can be reused anywhere within the App
        //Will house all our CRUD functions

        public List<Movie> GetMovies() //using the Dapper guide here to know what to do - need using() statement here
            //This is our R (read all)  in CRUD
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                var sql = "select * from movies";
                connect.Open();
                List<Movie> movies = connect.Query<Movie>(sql).ToList();
                connect.Close();
                return movies;
            }
        }
        public List<Movie> GetMoviesByTitle(string title)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"select * from movies where title like '{title}%';"; //this is how we can pass in a variable to use for our SQL query.
                connect.Open();
                List<Movie> searchResults = connect.Query<Movie>(sql).ToList(); //This query returns a list, but
                                                                                //we will tell it to only return the first on the list
                                                                                //(even though this search will always return exactly one object)
                connect.Close();

                return searchResults;
            }
        }
        public List<Movie> GetMoviesByGenre(string genre)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"select * from movies where genre like '{genre}%';"; //this is how we can pass in a variable to use for our SQL query.
                connect.Open();
                List<Movie> searchResults = connect.Query<Movie>(sql).ToList(); //This query returns a list, but
                                                                                //we will tell it to only return the first on the list
                                                                                //(even though this search will always return exactly one object)
                connect.Close();

                return searchResults;
            }
        }
        //Creating our Read single method/action now
        public Movie GetMovie(int ID)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = "select * from movies where id="+ ID; //this is how we can pass in a variable to use for our SQL query.
                connect.Open();
                Movie m = connect.Query<Movie>(sql).First(); //This query returns a list, but
                                                             //we will tell it to only return the first on the list
                                                             //(even though this search will always return exactly one object)
                connect.Close();

                return m;
            }
        }

        //Delete method - CAREFUL - deleting is permanent
        public void DeleteMovie(int ID) //void because we dont need anything to return
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = "delete from movies where id=" + ID; //this is how we can pass in a variable to use for our SQL query.
                connect.Open();
                connect.Query<Movie>(sql); //This query returns a list, but
                                                             //we will tell it to only return the first on the list
                                                             //(even though this search will always return exactly one object)
                //This looks very similar to the getMovies method but returns nothing (we dont care to display it) and we dont need a new object.
                connect.Close();

            }
        }
        public void UpdateMovie(Movie m)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = "update movies " +
                    $"set title='{m.Title}', genre='{m.Genre}', `year`={m.Year}, runtime={m.RunTime} " + //CONFIRM spaces, syntax, single quotes, etc.!
                    $"where id={m.ID}";
                connect.Open();
                connect.Query<Movie>(sql);
                connect.Close();
            }
        }

        public void CreateMovie(Movie m)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = "insert into movies " +
                    $"VALUES(0, '{m.Title}', '{m.Genre}', {m.Year}, {m.RunTime});";                    
                connect.Open();
                connect.Query<Movie>(sql);
                connect.Close();
            }
        }
        
    }
}
