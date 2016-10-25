using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hamazon.Models
{
    public class CommentRequest
    {
        public string  Subject {get;set;}
        public string  Comments {get;set;}
        public string  EmailAddress {get;set;}
        public int ProductId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}