using practiceA.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace practiceA.Controllers
{
    public class HelloWorldController : Controller
    {
        // GET: HelloWorld
        public ActionResult Index()
        {
            return View();
        }
        // GET: /HelloWorld/Welcome/ 
        public ActionResult Welcome(string name)
        {
            GetLogDLL.GetLog.Service1Client logDll = new GetLogDLL.GetLog.Service1Client();
            int ec = 0; // errorCount
            long count = 0;
            List<GetLogDLL.GetLog.RecordLog> recordlog = logDll.GetLog().ToList();
            List<Log> LogList = new List<Log> { };
            List<Log> Log = new List<Log> { };
            List<bool> LogMsgTag = new List<bool> { };
            foreach (var temp in recordlog)
            {
                var logModel = new Log
                {
                    Api = temp.rlApi,
                    Date = temp.rlDate,
                    DeviceId = temp.rlDeviceId,
                    DeviceType = temp.rlDeviceType,
                    Element = temp.rlElement,
                    Id = temp.rlId,
                    IsShow = temp.rlIsShow,
                    Machine = temp.rlMachine,
                    Msg = temp.rlMsg,
                    MsgTag = temp.rlMsgTag,
                    Project = temp.rlProject,
                    Service = temp.rlService,
                    Solution = temp.rlSolution,
                    Step = temp.rlStep,
                    LogGroup = count
                };
                if (temp.rlMsgTag == "Error")
                {
                    ec++;
                }
                LogList.Add(logModel);
                if (temp.rlStep == "99" || (recordlog.Last() == temp))
                {
                    if (ec > 0)
                        LogMsgTag.Add(true);
                    else
                        LogMsgTag.Add(false);
                    count++;
                    ec = 0;
                }
            }
            //var query = from p in LogList group p.Id by p.LogGroup;
            var query = from p in LogList group p.Id by p.LogGroup 
                        into groups select groups.First();
            foreach (var temp in query)
            {
                Log.Add(LogList[Convert.ToInt32(temp)- Convert.ToInt32(LogList[0].Id)]);
            }
            ViewBag.LogMsgTag = LogMsgTag;
            ViewBag.Log = Log;
            TempData["LogList"] = LogList;

            ViewBag.changeBackgroundColor = new Func<long, bool, string, string>(changeBackgroundColor);
            ViewBag.reCycle = new Func<bool, string>(reCycle);
            //ViewBag.FormatDate = new Func<DateTime, string>(FormatDate);
            return View();
        }
        public ActionResult Detail(int group)
        {
            List<Log> LogList = TempData["LogList"] as List<Log>;
            List<Log> Log = new List<Log> { };
            var query = from p in LogList where p.LogGroup == @group select p;
            foreach (var temp in query)
            {
                Log.Add(temp);
            }
            ViewBag.Log = Log;
            ViewBag.changeBackgroundColor = new Func<long, bool, string, string>(changeBackgroundColor);
            return View();
        }
        [HttpPost]
        public ActionResult MyAction(BasicInf model)
        {
            if (ModelState.IsValid)
            {
                // save to db, for instance
                return RedirectToAction("AnotherAction");
            }
            // model is not valid
            return View("MyView", model);
        }
        private string reCycle(bool b)
        {
            string result = "";
            if (b)
                result = "";
            else
                result = "";
            return result;
        }
        private string FormatDate(DateTime d)
        {
            int dNum = d.ToString().IndexOf(" ");
            return d.ToString().Substring(0, dNum) + d.ToString().Substring(dNum + 3);
        }
        private string changeBackgroundColor(long id, bool eG, string eD)
        {
            string styleStr = "";
            if (id % 2 == 0)
                styleStr = "background-color: #EBEBFF;";
            else
                styleStr = "background-color: #D6D6FF;";
            if (!eG.Equals(null))
                styleStr += RedOrBlack(eG);
            if (eD != null)
                styleStr += RedOrBlack(eD);
            return styleStr;
        }
        private string RedOrBlack(bool eG)
        {
            if (eG)
                return "color:red;";
            else
                return "color:black;";
        }
        private string RedOrBlack(string eD)
        {
            if (eD == "Error")
                return "color:red;";
            else
                return "color:black;";
        }
    }
}