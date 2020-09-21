using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedemptionNews.Models.Comments
{
    public class Comment
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Message { get; set; }
            public DateTime Created { get; set; }
    }
}
