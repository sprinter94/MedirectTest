using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Application.Model
{
    public class ResultWrapper : IResult
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public static ResultWrapper Ok()
            => New(null, 200, true);

        public static ResultWrapper Created()
            => New(null, 201, true);

        public static ResultWrapper Updated()
            => New(null, 204, true);

        public static ResultWrapper Bad(string message, int code = 400)
            => New(message, code);

        public static ResultWrapper UnAuthorized(string message, int code = 401)
            => New(message, code);

        public static ResultWrapper Forbidden(string message, int code = 403)
            => New(message, code);

        public static ResultWrapper NotFound(string message, int code = 404)
            => New(message, code);

        public static ResultWrapper Failed(string message, int code = 422)
            => New(message, code);

        public static ResultWrapper Internal(string message, int code = 500)
            => New(message, code);

        private static ResultWrapper New(
            string message,
            int code = 422,
            bool success = false)
            => new ResultWrapper() { Message = message, StatusCode = code, Success = success };
    }

    public class ResultWrapper<T> : IResult
    {
        public bool Success { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; }
        public T Data { get; set; }

        public static ResultWrapper<T> Ok(T data)
            => New(null, 200, data, true);

        public static ResultWrapper<T> Created(T data)
            => New(null, 201, data, true);

        public static ResultWrapper<T> Updated(T data = default)
            => New(null, 204, data, true);

        public static ResultWrapper<T> Bad(string message, int code = 400)
            => New(message, code);

        public static ResultWrapper<T> Forbidden(string message, int code = 401)
            => New(message, code);

        public static ResultWrapper<T> UnAuthorized(string message, int code = 403)
            => New(message, code);

        public static ResultWrapper<T> NotFound(string message, int code = 404)
            => New(message, code);

        public static ResultWrapper<T> Failed(string message, int code = 400)
            => New(message, code);

        public static ResultWrapper<T> Internal(string message, int code = 500)
            => New(message, code);

        private static ResultWrapper<T> New(
            string message,
            int code = 400,
            T data = default,
            bool success = false)
            => new ResultWrapper<T>() { Message = message, StatusCode = code, Success = success, Data = data };
    }
}