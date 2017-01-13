using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace practiceA.Controllers
{
    public class CalendarController : Controller
    {
        // GET: Calendar
        public ActionResult Index()
        {
            string link = HtmlHelper.GenerateLink(this.ControllerContext.RequestContext, System.Web.Routing.RouteTable.Routes, "Mylink", "Default", "InviteCard", "Calendar", null, null );
            ViewBag.Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            return View();
        }
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "shopName,phone,address,note,time,nickName,color,partyName,participants")] Party party)
        {
            return View(party);
        }*/
        public ActionResult _InviteCard(string nickname, string color)
        {
            ViewBag.Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            return View();
        }

        [HttpPost]
        public ActionResult InviteCard(string partyName, string shopName, string address, string phone, string note, string partyTime)
        {
            ViewBag.partyName = partyName;
            ViewBag.shopName = shopName;
            ViewBag.address = address;
            ViewBag.phone = phone;
            ViewBag.note = note;
            List<string> partyTimeArray = new List<string> { };
            int start = 0, end = 0;
            string str = partyTime;
            while (end != -1)
            {
                end = str.ToString().IndexOf(",");
                if(end != -1)
                {
                    start = 0;
                    partyTimeArray.Add(str.Substring(start, end));
                    start = end + 1;
                    str = str.Substring(start);
                }
                else
                {
                    partyTimeArray.Add(str);
                }
            }
            ViewBag.partyTime = partyTimeArray;
            return View();
            //new { partyName = partyName, position = position, adress = adress, phone = phone, remark = remark, partyTime = partyTime }
            //new { partyName, position, adress, phone, remark, partyTime }
        }
    }
}