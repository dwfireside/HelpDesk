using Dapper;
using Dapper.Contrib.Extensions;
using HelpDesk.Shared.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace HelpDeskServer.Server.Services
{
    public class RequestService
    {
        private readonly IConfiguration _config;


        public RequestService(IConfiguration config)
        {
            _config = config;
        }

        public string SqlConnectionString { 
            get { 
                return _config.GetConnectionString("DefaultConnection");  
            } 
        }

        public Task<RequestList> GetRequestsAsync()
        {
            using (var connection = new SqlConnection(SqlConnectionString))
            {
                return Task.FromResult(GetRequests());
            }
        }

        public RequestList GetRequests()
        {
            using (var connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();

                var sql = "SELECT r.RequestID, e.EventName, [Description], [CreatedOn], " +
                          "(SELECT COUNT(*) FROM [Notification] n WHERE n.RequestID = r.RequestID ) As NotificationCount," +
                          "ResolvedIssue As IsResolved " +
                          "FROM [Page].[dbo].[Request] r INNER JOIN Event e on e.EventID = r.EventID " +
                          "WHERE Description IS NOT NULL";
 
                return new RequestList(connection.Query<RequestEx>(sql));
            }
        }

        public Task<IEnumerable<ResponseMessage>> GetResponsesAsync(int requestID)
        {
            using (var connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();

                var sql = "SELECT n.NotificationID, n.RequestID, n.Description, e.FirstName + ' ' + e.LastName As UserName, n.EditedOn As CreatedDate " +
                          "FROM [Notification] n INNER JOIN [Employee] e on n.EditedBy = e.EmployeeIdNumber " +
                          "WHERE n.RequestID = @requestID";

                return Task.FromResult(connection.Query<ResponseMessage>(sql, new { requestID } ));
            }
        }

        public void AddReponse(ResponseMessage message)
        {
            using (var connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();

                var note = new Notification()
                {
                    RequestId = message.RequestId,
                    Description = message.Description,
                    EditedOn = DateTime.UtcNow,
                    EditedBy = 114
                };

                connection.Insert<Notification>(note);
            }
        }

        public void CloseRequest(RequestEx request)
        {
            var sql = "UPDATE Request Set ResolvedIssue=1, EditedOn=@editDate WHERE RequestID=@requestID";
            var param = new { editDate = DateTime.UtcNow, request.RequestID };

            using (var connection = new SqlConnection(SqlConnectionString))
            {
                connection.Open();
                connection.Execute(sql, param);
            }

            request.IsResolved = true;
        }
    }
}
