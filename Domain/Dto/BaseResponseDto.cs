using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Dto
{
    public class BaseResponseDto<TData>
    {
        public BaseResponseDto()
        {
            Errors = new Dictionary<String, String>();
        }

        public bool HasError => Errors.Any();
        public Dictionary<String,String> Errors { get; set; }
        public int Total { get; set; }
        public TData Data { get; set; }
    }
}
