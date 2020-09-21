using RedemptionNews.Models.Comments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RedemptionNews.Models
{
    public class Post
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Author { get; set; } = "";
        public string Title { get; set; } = "";
        public string SubTitle { get; set; } = "";
        public string Body { get; set; } = "";
        public DateTime Create { get; set; } = DateTime.Now;
        public string Image { get; set; } = "";

        public List<MainComment> MainComments { get; set; }
       
    }
}
