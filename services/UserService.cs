using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterviewExample.models;

// Instead of using concatenation, create SQLiteParameter objects and add them to the command,
// then execute the command using these.

// SQL Injection could destroy the DB and opens up vulerabilities.
// i.e. select * from users where UserId = " + txtUserId; 
// If we let txtUserId = 105 OR 1=1, a hacker could get all rows, potentially including all usernames and passwords


namespace InterviewExample.services
{
    class UserService : BaseService
    {
		//reinitialize user table
        public void CreateTable()
        {
			using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
			{
				conn.Open();
				using (SQLiteCommand cmd = new SQLiteCommand(conn))
				{
					cmd.CommandText = "drop table if exists user";
					cmd.ExecuteNonQuery();

					cmd.CommandText = "create table user ("
							+ "id integer, "
							+ "username string, "
							+ "email string, "
							+ "created date)";
					cmd.ExecuteNonQuery();
				}
			}
		}

		//create table and users with random data
		public void Setup(int userCount)
		{
			CreateTable();
			for(int i = 0; i < userCount; i++)
			{
				Put(new User()
				{
					id = i,
					userName = RandomString(10),
					email = RandomString(8) + "@" + RandomString(5) + "." + RandomString(3)
				});
			}
		}

		//store a new user
		public void Put(User u)
		{
			using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
			{
				conn.Open();
				using (SQLiteCommand cmd = new SQLiteCommand("insert into user VALUES (" + u.id + ", '" + u.userName + "', '" + u.email + "', datetime('now'))", conn))
				{
					cmd.ExecuteNonQuery();
				}
			}
		}

		//retrieve an existing user
		public User Get(String id)
		{
			using (SQLiteConnection conn = new SQLiteConnection(ConnectionString))
			{
				conn.Open();
				using (SQLiteCommand cmd = new SQLiteCommand("select * from user where id = " + id, conn))
				{
					using (SQLiteDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							return new User()
							{
								id = reader.GetInt32(reader.GetOrdinal("id")),
								userName = reader.GetString(reader.GetOrdinal("username")),
								email = reader.GetString(reader.GetOrdinal("email")),
								created = reader.GetDateTime(reader.GetOrdinal("created"))
							};
						}
					}
				}
			}
			return null;
		}
	}
}
