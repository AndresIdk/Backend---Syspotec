using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using Syspotec.Core.DTOs;
using Syspotec.Core.entities;
using Syspotec.Core.Interfaces;
using Syspotec.Infrastructure.Data;

namespace Syspotec.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        DB db = new DB();
        public async Task<List<UserDTO>> Get()
        {
            var users = new List<UserDTO>();
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("showUsers", sql))
                    {

                        await sql.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var user = new UserDTO();
                                user.NIT = (int) reader["nit"];
                                user.First_name = ((string)reader["first_name"]).Trim();
                                user.Last_name = ((string)reader["last_name"]).Trim();
                                users.Add(user);
                            }
                        }
                    }
                }
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return users;
            }
        }

        public async Task<User> GetByID(int nit)
        {
            var user = new User();
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("showUserById", sql))
                    {
                        await sql.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", nit);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                user.NIT = (int)reader["nit"];
                                user.First_name = ((string)reader["first_name"]).Trim();
                                user.Last_name = ((string)reader["last_name"]).Trim();
                                user.Email = ((string)reader["email"]).Trim();
                                user.Password = ((string)reader["password"]).Trim();
                            }
                        }
                    }
                }
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return user;
            }
        }

        public async Task<bool> Post(User user)
        {
            var hashAlgorithm = SHA256.Create();
            var hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(user.Password));
            var pwd_hash = Convert.ToBase64String(hash);

            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("insertUser", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("nit", user.NIT);
                        cmd.Parameters.AddWithValue("Fname", user.First_name);
                        cmd.Parameters.AddWithValue("Lname", user.Last_name);
                        cmd.Parameters.AddWithValue("email", user.Email);
                        cmd.Parameters.AddWithValue("pwd", pwd_hash);
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public async Task<bool> Delete(int nit)
        {
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("deleteUser", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", nit);
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Update(int nit, User user)
        {
            try
            {
                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("updateTicket", sql))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("id", nit);
                        cmd.Parameters.AddWithValue("Fname", user.First_name);
                        cmd.Parameters.AddWithValue("Lname", user.Last_name);
                        cmd.Parameters.AddWithValue("email", user.Email);
                        cmd.Parameters.AddWithValue("pwd", user.Password);
                        await sql.OpenAsync();
                        await cmd.ExecuteNonQueryAsync();
                    }
                }
                return true;

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<string> GetPWD(int nit)
        {
            string pwd = null;
            try
            {

                using (var sql = new SqlConnection(db.DBConnection()))
                {
                    using (var cmd = new SqlCommand("getPWD", sql))
                    {
                        await sql.OpenAsync();
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("nit", nit);
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                pwd = ((string)reader["password"]).Trim();
                            }
                        }
                    }
                }
                return pwd;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return pwd;
            }
        }
    }
}
