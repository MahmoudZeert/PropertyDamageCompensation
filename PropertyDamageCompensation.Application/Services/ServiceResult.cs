using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyDamageCompensation.Application.Services
{
    public class ServiceResult<T>
    {
        public bool IsSuccess;
        public T Result;
        public string ErrorMessaage;
        public ServiceResult(bool isSuccess,T? result,string? errorMessage)
        {
            IsSuccess = isSuccess;
            Result = result;
            ErrorMessaage = errorMessage;
        }
        public static ServiceResult<T> Success(T result)
        {
            return new ServiceResult<T>(true,result,null);
        }
        public static ServiceResult<T> Fail(string errorMessage)
        {
            return new ServiceResult<T>(false,default(T),errorMessage);
        }

    }
}
