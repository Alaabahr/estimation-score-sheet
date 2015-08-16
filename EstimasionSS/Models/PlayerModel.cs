using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EstimasionSS.Models
{
    public class PlayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public string UserId { get; set; }
    }
}