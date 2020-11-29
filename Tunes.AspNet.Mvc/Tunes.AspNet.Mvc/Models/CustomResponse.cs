using System.Collections.Generic;

namespace Tunes.AspNet.Mvc.Models
{
    public class CustomResponse<TEntity>
    {
        public bool Success { get; set; }
        public TEntity Data { get; set; }
        public List<string> Errors { get; set; }
    }
}
