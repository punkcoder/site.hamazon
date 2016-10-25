using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using Hamazon.Models;
using Umbraco.Web.WebApi;

namespace Hamazon.Controllers
{
    public class CommentController : UmbracoApiController
    {
        [HttpPost]
        public bool AddComment([FromBody] CommentRequest IncomingComment)
        {
            try
            {
                var insertText = "INSERT INTO [dbo].[cmsComments] ([ProductId] ,[CommentSubject] ,[CommentText] ,[CommentBy] ,[CreatedBy] ,[CreatedOn]) VALUES (@ProductId, @CommentSubject, @CommentText, @CommentBy, @CreatedBy, @CreatedOn)";

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["umbracoDbDSN"].ConnectionString);
                SqlCommand cmd = new SqlCommand(insertText, conn);

                cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = IncomingComment.ProductId;
                cmd.Parameters.Add("@CommentSubject", SqlDbType.NVarChar).Value = IncomingComment.Subject;
                cmd.Parameters.Add("@CommentText", SqlDbType.NText).Value = IncomingComment.Comments;
                cmd.Parameters.Add("@CommentBy", SqlDbType.NVarChar).Value = IncomingComment.EmailAddress;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar).Value = IncomingComment.EmailAddress;
                cmd.Parameters.Add("@CreatedOn", SqlDbType.DateTime).Value = DateTime.UtcNow;

                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                cmd.ExecuteNonQuery();
                if (conn.State != System.Data.ConnectionState.Closed)
                    conn.Close();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        [HttpGet]
        public dynamic GetComments(int ProductId)
        {
            var outputlist = new List<CommentRequest>();
            var select = "SELECT [Id] ,[ProductId] ,[CommentSubject] ,[CommentText] ,[CommentBy] ,[CreatedOn] FROM cmsComments WHERE ProductId = @ProductId ORDER BY CreatedOn Desc";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["umbracoDbDSN"].ConnectionString);
            SqlCommand cmd = new SqlCommand(select, conn);

            cmd.Parameters.Add("@ProductId", SqlDbType.Int).Value = ProductId;

            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                outputlist.Add(
                new CommentRequest()
                {
                    ProductId = reader.GetInt32(1),
                    Subject = reader.GetString(2),
                    Comments = reader.GetString(3),
                    EmailAddress = reader.GetString(4),
                    CreatedOn = reader.GetDateTime(5)
                });
            }

            if (conn.State != System.Data.ConnectionState.Closed)
                conn.Close();

            return new {comments = outputlist};
        }
    }
}