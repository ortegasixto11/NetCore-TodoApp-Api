using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TasksApp.ApiSqlServer.Helpers
{
    public class ResponseApiHelper
    {
        public ResponseApiModel<object> Error(string message)
        {
            return new ResponseApiModel<object> { StatusCode = 400, Error = true, Message = message };
        }

        public ResponseApiModel<object> Error(int statusCode, string message)
        {
            return new ResponseApiModel<object> { StatusCode = statusCode, Error = true, Message = message };
        }

        public ResponseApiModel<object> Success(string message)
        {
            return new ResponseApiModel<object> { StatusCode = 200, Error = false, Message = message };
        }

        public ResponseApiModel<T> Success<T>(List<T> data)
        {
            return new ResponseApiModel<T> { StatusCode = 200, Error = false, Message = "", Data = data };
        }



    }

    public class ResponseApiModel<T>
    {
        public int StatusCode { get; set; }
        public bool Error { get; set; }
        public string Message { get; set; }
        public IEnumerable<T> Data { get; set; } = new List<T>();
    }
}
