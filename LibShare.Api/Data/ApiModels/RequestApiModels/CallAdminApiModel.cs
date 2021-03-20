using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibShare.Api.Data.ApiModels.RequestApiModels
{
    public class CallAdminApiModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        //public string Phone { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
    }
}
