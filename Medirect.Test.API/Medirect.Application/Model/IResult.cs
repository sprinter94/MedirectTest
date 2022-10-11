using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Application.Model
{
    public interface IResult
    {
        bool Success { get; set; }
        int StatusCode { get; set; }
        string Message { get; set; }
    }
}