using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RedemptionNews.ViewModels
{
    public class CommentViewModel
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public int MainCommentId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
