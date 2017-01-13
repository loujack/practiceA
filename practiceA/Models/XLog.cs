using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace practiceA.Models
{
    public class Log
    {
        public string Api { get; set; }
        public DateTime? Date { get; set; }
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string Element { get; set; }
        public long Id { get; set; }
        public string IsShow { get; set; }
        public string Machine { get; set; }
        public string Msg { get; set; }
        public string MsgTag { get; set; }
        public string Project { get; set; }
        public string Service { get; set; }
        public string Solution { get; set; }
        public string Step { get; set; }
        public long LogGroup { get; set; }
    }
}