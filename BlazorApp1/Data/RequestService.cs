﻿using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    public class RequestService
    {
        public Task<RequestList> GetRequestsAsync()
        {
            using (var connection = new SqlConnection("Data Source=(local)\\SQLExpress2016;Initial Catalog=Page;User ID=sa;Password=pallmall"))
            {
                return Task.FromResult(GetRequests());
            }
        }

        public RequestList GetRequests()
        {
            using (var connection = new SqlConnection("Data Source=(local)\\SQLExpress2016;Initial Catalog=Page;User ID=sa;Password=pallmall"))
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
            using (var connection = new SqlConnection("Data Source=(local)\\SQLExpress2016;Initial Catalog=Page;User ID=sa;Password=pallmall"))
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
            using (var connection = new SqlConnection("Data Source=(local)\\SQLExpress2016;Initial Catalog=Page;User ID=sa;Password=pallmall"))
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

            using (var connection = new SqlConnection("Data Source=(local)\\SQLExpress2016;Initial Catalog=Page;User ID=sa;Password=pallmall"))
            {
                connection.Open();
                connection.Execute(sql, param);
            }

            request.IsResolved = true;
        }
    }
}
