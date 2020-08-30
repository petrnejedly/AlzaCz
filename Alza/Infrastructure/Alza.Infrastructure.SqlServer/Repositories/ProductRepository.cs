using Alza.Domain.Abstractions.Repositories;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Alza.Infrastructure.SqlServer.Repositories
{
    /// <summary>
    /// Product repository.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private static string ConnectionString = string.Empty;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="mapper">An instance of the Automapper class.</param>
        public ProductRepository(IConfiguration configuration, IMapper mapper)
        {
            this.configuration = configuration;
            this.mapper = mapper;
            ConnectionString = configuration.GetConnectionString("ConnectionStringAlzaWeb");
        }

        /// <inheritdoc/>
        public Domain.Entities.Product GetProduct(int id)
        {
            Entities.Product product = new Entities.Product();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand myCmd = connection.CreateCommand())
                {
                    myCmd.CommandType = CommandType.StoredProcedure;
                    myCmd.CommandText = "[dbo].[Alza_Products_SelectOneById]";
                    myCmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = id;
                    try
                    {
                        myCmd.Connection.Open();
                        using (SqlDataReader sqlReader = myCmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (sqlReader.HasRows)
                            {
                                sqlReader.Read();
                                product = new Entities.Product()
                                {
                                    Id = sqlReader.IsDBNull(0) ? 0 : sqlReader.GetInt32(0),
                                    Name = sqlReader.IsDBNull(1) ? string.Empty : sqlReader.GetString(1),
                                    ImgUri = sqlReader.IsDBNull(2) ? string.Empty : sqlReader.GetString(2),
                                    Price = sqlReader.IsDBNull(3) ? 0 : sqlReader.GetDecimal(3),
                                    Description = sqlReader.IsDBNull(4) ? string.Empty : sqlReader.GetString(4)
                                };
                            }
                        }
                    }
                    catch (Exception eX)
                    {
                        string declaringType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                        string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                        throw new Exception($"Error in {declaringType} / {methodName}.", eX);
                    }
                }
            }

            return this.mapper.Map<Domain.Entities.Product>(product);
        }

        /// <inheritdoc/>
        public IList<Domain.Entities.Product> GetProducts()
        {
            List<Entities.Product> products = new List<Entities.Product>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand myCmd = connection.CreateCommand())
                {
                    myCmd.CommandType = CommandType.StoredProcedure;
                    myCmd.CommandText = "[dbo].[Alza_Products_SelectAll]";
                    try
                    {
                        myCmd.Connection.Open();
                        using (SqlDataReader sqlReader = myCmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (sqlReader.HasRows)
                            {
                                while (sqlReader.Read())
                                {
                                    products.Add(new Entities.Product()
                                    {
                                        Id = sqlReader.IsDBNull(0) ? 0 : sqlReader.GetInt32(0),
                                        Name = sqlReader.IsDBNull(1) ? string.Empty : sqlReader.GetString(1),
                                        ImgUri = sqlReader.IsDBNull(2) ? string.Empty : sqlReader.GetString(2),
                                        Price = sqlReader.IsDBNull(3) ? 0 : sqlReader.GetDecimal(3),
                                        Description = sqlReader.IsDBNull(4) ? string.Empty : sqlReader.GetString(4)
                                    });
                                }
                            }
                            sqlReader.Close();
                        }
                    }
                    catch (Exception eX)
                    {
                        string declaringType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                        string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                        throw new Exception($"Error in {declaringType} / {methodName}.", eX);
                    }
                }
            }

            return this.mapper.Map<IEnumerable<Domain.Entities.Product>>(products).ToList();
        }

        /// <inheritdoc/>
        public IList<Domain.Entities.Product> GetProducts(int page, int pageSize)
        {
            List<Entities.Product> products = new List<Entities.Product>();

            int total = 0;
            int pageIndex = page - 1;
            if (pageIndex < 0) { pageIndex = 0; }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand myCmd = connection.CreateCommand())
                {
                    myCmd.CommandType = CommandType.StoredProcedure;
                    myCmd.CommandText = "[dbo].[Alza_Products_SelectPaged]";
                    myCmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                    myCmd.Parameters.Add("@CurrentPageIndex", SqlDbType.Int).Value = pageIndex;
                    try
                    {
                        myCmd.Connection.Open();
                        using (SqlDataReader sqlReader = myCmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            if (sqlReader.HasRows)
                            {
                                /*
                                  [VirtualItemCount],
                                  [CurrentPageIndex],
                                  [PageSize],
                                  [PagesCount]
                                */
                                sqlReader.Read();
                                total = sqlReader.IsDBNull(0) ? 0 : sqlReader.GetInt32(0);
                            }
                            sqlReader.NextResult();
                            if (sqlReader.HasRows)
                            {
                                while (sqlReader.Read())
                                {
                                    products.Add(new Entities.Product()
                                    {
                                        Id = sqlReader.IsDBNull(0) ? 0 : sqlReader.GetInt32(0),
                                        Name = sqlReader.IsDBNull(1) ? string.Empty : sqlReader.GetString(1),
                                        ImgUri = sqlReader.IsDBNull(2) ? string.Empty : sqlReader.GetString(2),
                                        Price = sqlReader.IsDBNull(3) ? 0 : sqlReader.GetDecimal(3),
                                        Description = sqlReader.IsDBNull(4) ? string.Empty : sqlReader.GetString(4)
                                    });
                                }
                            }
                            sqlReader.Close();
                        }
                    }
                    catch (Exception eX)
                    {
                        string declaringType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                        string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                        throw new Exception($"Error in {declaringType} / {methodName}.", eX);
                    }
                }
            }

            return this.mapper.Map<IEnumerable<Domain.Entities.Product>>(products).ToList();
        }

        /// <inheritdoc/>
        public bool UpdateProduct(Domain.Entities.Product product)
        {
            bool returnSuccess = false;

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand myCmd = connection.CreateCommand())
                {
                    myCmd.CommandType = CommandType.StoredProcedure;
                    myCmd.CommandText = "[dbo].[Alza_Products_Update]";
                    myCmd.Parameters.Add("@ProductID", SqlDbType.Int).Value = product.Id;
                    myCmd.Parameters.Add("@Description", SqlDbType.NVarChar, -1).Value = product.Description;
                    myCmd.Parameters.Add("@IdentityName", SqlDbType.NVarChar, 255).Value = "Web";
                    myCmd.Parameters.Add("@IdentityIP", SqlDbType.NVarChar, 255).Value = "0.0.0.0";
                    try
                    {
                        myCmd.Connection.Open();
                        myCmd.ExecuteNonQuery();
                        myCmd.Connection.Close();
                        returnSuccess = true;
                    }
                    catch (Exception eX)
                    {
                        string declaringType = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
                        string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                        throw new Exception($"Error in {declaringType} / {methodName}.", eX);
                    }
                }
            }

            return returnSuccess;
        }
    }
}