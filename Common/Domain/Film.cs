using Repository.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain
{
    [Serializable]
    public class Film : IEntity
    {
        [IncludeInParameters("Delete,Update")]
        public int FilmId { get; set; }
        [IncludeInParameters("Insert")]
        public string Title { get; set; }
        [IncludeInParameters("Insert, Update")]
        public int Quantity { get; set; }
        public Genre Genre { get; set; }
        [IncludeInParameters("Insert")]
        [Browsable(false)]
        public int GenreId => Genre.GenreId;
        public List<Actor> Actors { get; set; }
        [IncludeInParameters("Insert")]
        public string ImageUrl { get; set; }
        [IncludeInParameters("Insert")]
        public double PricePerDay { get; set; }


        [Browsable(false)]
        public string TableName => "Film";
        [Browsable(false)]
        public string IDName => "FilmId";
        [Browsable(false)]
        public string TableAlias => "f";
        [Browsable(false)]
        public string InsertValues => "@Title, @Quantity, @GenreId, @ImageUrl, @PricePerDay ";
        [Browsable(false)]
        public string SelectValues => "f.*, fr.actorId, fr.roleName, g.genreName, a.Name, a.Gender";
        [Browsable(false)]
        public string UpdateValues => "Quantity=@Quantity ";
        [Browsable(false)]
        public string JoinTable => " LEFT JOIN FilmRole fr ON f.filmId = fr.filmId JOIN Genre g ON (f.genreId = g.genreId) JOIN Actor a ON (a.ActorId = fr.ActorId) ";
        [Browsable(false)]
        public string JoinCondition => "";
        [Browsable(false)]
        public string UpdateCondition => " FilmId=@FilmId ";
        [Browsable(false)]
        public string DeleteQuery => " FilmId=@FilmId";
        [Browsable(false)]
        public string OrderBy => "";
        [Browsable(false)]
        [IncludeInParameters("Select")]
        public string SearchFilter {get; set;}
        public List<IEntity> GetReaderList(SqlDataReader reader)
        {
            Dictionary<int, IEntity> filmDictionary = new Dictionary<int, IEntity>();
            try
            {
                while (reader.Read())
                {
                    int filmId = (int)reader["filmId"];
                    if (!filmDictionary.ContainsKey(filmId))
                    {
                        Film film = new Film()
                        {
                            FilmId = filmId,
                            Genre = new Genre()
                            {
                                GenreId = (int)reader["genreId"],
                                GenreName = (string)reader["genreName"]
                            },
                            Title = (string)reader["title"],
                            ImageUrl = (string)reader["imageUrl"],
                            Quantity = (int)reader["quantity"],
                            Actors = new List<Actor>(),
                            PricePerDay = (double)reader["PricePerDay"]
                        };
                        filmDictionary.Add(filmId, film);
                    }
                    int actorId = reader["actorId"] as int? ?? -1;
                    if (actorId != -1)
                    {
                        Actor actor = new Actor()
                        {
                            ActorId = actorId,
                            Gender = (Gender)Enum.Parse(typeof(Gender), (string)reader["Gender"]),
                            Name = (string)reader["name"],
                            RoleName = (string)reader["roleName"]
                        };
                        ((Film)filmDictionary[filmId]).Actors.Add(actor);
                    }
                }
            }
            finally { reader.Close(); }
            return filmDictionary.Select(kvp => kvp.Value).ToList();
        }
    }
}
