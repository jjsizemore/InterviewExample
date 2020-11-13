using InterviewExample.models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Instead of using concatenation, create SQLiteParameter objects and add them to the command,
// then execute the command using these.

// SQL Injection could destroy the DB and opens up vulerabilities.
// i.e. select * from users where UserId = " + txtUserId; 
// If we let txtUserId = 105 OR 1=1, a hacker could get all rows, potentially including all usernames and passwords

// May also be of value to set id as primary key
// i.e. id INTEGER NOT NULL PRIMARY KEY
// Could also just use rowid and not have an id column. 
// When a table is created with a PK, this column is the alias of the rowid column

namespace InterviewExample.services
{
    class PostService : BaseService
    {
		//Initialize post table
		public void CreateTable()
		{
			using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
			{
				conn.Open();
				using (SQLiteCommand cmd = new SQLiteCommand(conn))
				{
					cmd.CommandText = "drop table if exists post";
					cmd.ExecuteNonQuery();

					cmd.CommandText = "create table post ("
							+ "id integer, "
							+ "user_id integer, "
							+ "subject string, "
							+ "posted date, "
							+ "updated date)";
					cmd.ExecuteNonQuery();
				}
			}
		}

		//Create table and initialize the appropriate number of posts per user
		public void Setup(int userCount, int postsPerUserCount)
		{
			CreateTable();
			int i = 0;
			while (i < userCount)
            {
				Put(new Post()
				{
					id = i,
					userId = i,
					subject = RandomString(10)
				});
				i++;
            }
			while (i < userCount * postsPerUserCount)
			{
				Put(new Post()
				{
					id = i,
					userId = random.Next(userCount),
					subject = RandomString(10)
				});
				i++;
			}
		}

		//store a new post
		public void Put(Post p)
		{
			using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
			{
				conn.Open();
				using (SQLiteCommand cmd = new SQLiteCommand("insert into post VALUES (" +
					p.id + ", " +
					p.userId + ", '" +
					p.subject + "', datetime('now'),datetime('now'))", conn))
				{
					cmd.ExecuteNonQuery();
				}
			}
		}

		//retrieve an existing post by id
		public Post Get(String id)
		{
			using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
			{
				conn.Open();
				using (SQLiteCommand cmd = new SQLiteCommand("select * from post where id = " + id, conn))
				{
					using (SQLiteDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							return new Post()
							{
								id = reader.GetInt32(reader.GetOrdinal("id")),
								subject = reader.GetString(reader.GetOrdinal("subject")),
								userId = reader.GetInt32(reader.GetOrdinal("user_id")),
								posted = reader.GetDateTime(reader.GetOrdinal("posted")),
								updated = reader.GetDateTime(reader.GetOrdinal("updated"))
							};
						}
					}
				}
			}
			return null;
		}

		//return a users total number of posts
		public int getPostCount(String userId)
		{
			return getByUserId(userId).Count();
		}

		//return all of a users posts
		public List<Post> getByUserId(String userId)
		{
			List<Post> result = new List<Post>();
			
			try
			{
				using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
				{
					conn.Open();
					using (SQLiteCommand cmd = new SQLiteCommand("select * from post where user_id = " + userId, conn))
					{
						using (SQLiteDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								result.Add(new Post()
								{
									id = reader.GetInt32(reader.GetOrdinal("id")),
									subject = reader.GetString(reader.GetOrdinal("subject")),
									userId = reader.GetInt32(reader.GetOrdinal("user_id")),
									posted = reader.GetDateTime(reader.GetOrdinal("posted")),
									updated = reader.GetDateTime(reader.GetOrdinal("updated"))
								});
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.StackTrace);
			}
			return result;
		}
	}
}
