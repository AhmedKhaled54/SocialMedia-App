using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.Wrappers
{
    public  class Pagination<T>
    {
        public Pagination(List<T> data )
        {
             Data= data;
        }
        public Pagination() { }
        public Pagination(bool success ,List<T> data=default, List<string> message =null,int pagesize =10,int count =0,int page=1 )
        {

            Data = data;
            CurrentPage = page;
            TotalCount = count;
            PageSize = pagesize;
            Message = message;
            Success = success;
            TotalPages = (int)Math.Ceiling(count / (double)pagesize);

        }

        public static Pagination<T> PaginationSuccess(List<T> data, int count, int page, int pagesize)
            => new(true, data, null, pagesize, count, page);

        public List<T> Data { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool PerviousPage => CurrentPage > 1;
        public bool NextPage =>CurrentPage< TotalCount;
        public bool Success {  get; set; }
        public List<string> Message { get; set; } = new();
    }
}
