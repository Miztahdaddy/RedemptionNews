using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedemptionNews.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string SubTitle { get; set; } = "";
        public string Author { get; set; } = "";
        public string Body { get; set; } = "";
        public string CurrentImage { get; set; }
        public IFormFile Image { get; set; } = null;
    }
}
