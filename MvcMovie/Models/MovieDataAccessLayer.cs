using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace MvcMovie.Models
{
    public class MovieDataAccessLayer
    {
        string connectionString = @"Server=tcp:abcddb.database.windows.net,1433;Initial Catalog=MvcMovie_db;Persist Security Info=False;User ID=Svet;Password=SoftLinkersDemo!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        //To View all Movies details    
        public IEnumerable<Movie> GetAllMovies()
        {
            List<Movie> lstMovie = new List<Movie>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Movie", con);


                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Movie Movie = new Movie();

                    Movie.Id = Convert.ToInt32(rdr["Id"]);
                    Movie.Title = rdr["Title"].ToString();
                    Movie.Genre = rdr["Genre"].ToString();
                    Movie.ReleaseDate = Convert.ToDateTime(rdr["ReleaseDate"]);
                    Movie.Price = Convert.ToDecimal(rdr["Price"]);


                    lstMovie.Add(Movie);
                }
                con.Close();
            }
            return lstMovie;
        }
        public Movie GetMovie(int id)
        {
            Movie mov = new Movie();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand($"SELECT * FROM Movie WHERE Id = {id}", con);


                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    

                    mov.Id = Convert.ToInt32(rdr["Id"]);
                    mov.Title = rdr["Title"].ToString();
                    mov.Genre = rdr["Genre"].ToString();
                    mov.ReleaseDate = Convert.ToDateTime(rdr["ReleaseDate"]);
                    mov.Price = Convert.ToDecimal(rdr["Price"]);



                }
                con.Close();
            }
            return mov;
        }
        public void CreateMovie(Movie movie)
        {
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@$"INSERT INTO Movie (Title, ReleaseDate, Genre, Price)
                    VALUES(@Title, @ReleaseDate, @Genre, @Price); ", con);


                con.Open();
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar, -1).Value = movie.Title;
                cmd.Parameters.Add("@ReleaseDate", SqlDbType.DateTime2,7).Value = movie.ReleaseDate;
                cmd.Parameters.Add("@Genre", SqlDbType.NVarChar, -1).Value = movie.Genre;
                //decimal
                SqlParameter param = new SqlParameter("@Price", SqlDbType.Decimal);
                param.Value = movie.Price;
                param.Scale = 2;
                param.Precision = 18;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            
        }
        public void EditMovie(Movie movie)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@$"UPDATE Movie SET Title = @Title, ReleaseDate = @ReleaseDate,
                    Genre = @Genre, Price = @Price WHERE Id = @Id", con);


                con.Open();
                cmd.Parameters.Add("@Id", SqlDbType.Int, -1).Value = movie.Id;
                cmd.Parameters.Add("@Title", SqlDbType.NVarChar, -1).Value = movie.Title;
                cmd.Parameters.Add("@ReleaseDate", SqlDbType.DateTime2, 7).Value = movie.ReleaseDate;
                cmd.Parameters.Add("@Genre", SqlDbType.NVarChar, -1).Value = movie.Genre;
                //decimal
                SqlParameter param = new SqlParameter("@Price", SqlDbType.Decimal);
                param.Value = movie.Price;
                param.Scale = 2;
                param.Precision = 18;
                cmd.Parameters.Add(param);
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
        public void DeleteMovie(int id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(@$"DELETE FROM Movie WHERE Id = @Id", con);


                con.Open();
                cmd.Parameters.Add("@Id", SqlDbType.Int, -1).Value = id;
                
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }

    }
}
