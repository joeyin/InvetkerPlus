using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Invetker.Models
{
    public class PolygonIO
    {
        public class ResponseObject
        {
            public string request_id { get; set; }
            public TickerDetails result { get; set; }
        }

        public class Branding
        {
            public string icon_url { get; set; }
            public string logo_url { get; set; }
        }

        public class TickerDetails
        {
            public Branding branding { get; set; }
        }

    }
}