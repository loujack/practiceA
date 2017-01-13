using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using practiceA.Models;
using Newtonsoft.Json;

namespace practiceA.Controllers
{
    public class PartyController : Controller
    {
        private PartyDBContext db = new PartyDBContext();
        private ManChooseDataContext dmc = new ManChooseDataContext();
        // GET: Party
        public ActionResult Index()
        {
            return View(db.Party.ToList());
        }

        // GET: Party/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Party party = db.Party.Find(id);
            if (party == null)
            {
                return HttpNotFound();
            }
            return View(party);
        }

        // GET: Party/Create
        public ActionResult Create()
        {
            ViewBag.Date = DateTime.Now.ToString("yyyy/MM/dd HH:mm");
            return View();
        }

        // POST: Party/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "no,shopName,phone,address,note,time,nickName,color,partyName,participants")] Party party)
        {
            if (ModelState.IsValid)
            {
                var manChoose = new ManChoose
                {
                    nickName = party.nickName,
                    partyName = party.partyName,
                    time = party.time
                };
                var manColor = new ManColor
                {
                    nickName = party.nickName,
                    color = party.color
                };
                dmc.ManColor.InsertOnSubmit(manColor);
                dmc.ManChoose.InsertOnSubmit(manChoose);
                dmc.SubmitChanges();
                db.Party.Add(party);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(party);
        }

        // GET: Party/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Party party = db.Party.Find(id);
            if (party == null)
            {
                return HttpNotFound();
            }
            return View(party);
        }

        // POST: Party/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "no,shopName,phone,address,note,time,nickName,color,partyName,participants")] Party party)
        {
            if (ModelState.IsValid)
            {
                db.Entry(party).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(party);
        }

        // GET: Party/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Party party = db.Party.Find(id);
            if (party == null)
            {
                return HttpNotFound();
            }
            return View(party);
        }

        // POST: Party/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Party party = db.Party.Find(id);
            db.Party.Remove(party);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult InviteCard(int no)
        {
            return View(db.Party.Where(o => o.no == no).ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InviteCard(FormCollection form)
        {
            string strName = form["item.nickName"], color = form["item.color"], time = form["item.time"], partyName = form["item.partyName"];
            if (ModelState.IsValid)
            {
                var queryA = (from o in dmc.ManChoose where o.nickName == strName select o).ToArray();
                if (queryA.Length == 0)
                {
                    var manChoose = new ManChoose
                    {
                        nickName = strName,
                        time = time,
                        partyName = partyName
                    };
                    dmc.ManChoose.InsertOnSubmit(manChoose);
                }
                else
                {
                    //更新
                    foreach (var temp in queryA)
                    {
                        if (temp.time != time)
                        {
                            temp.time = time;
                        }
                    }
                }
                var queryB = (from o in dmc.ManColor where o.nickName == strName select o).ToArray();
                if (queryB.Length == 0)
                {
                    var manColor = new ManColor
                    {
                        nickName = strName,
                        color = color
                    };
                    dmc.ManColor.InsertOnSubmit(manColor);
                }
                else
                {
                    //更新
                    foreach (var temp in queryB)
                    {
                        temp.color = color;
                    }
                }
                var queryC = (from o in dmc.Parties where o.partyName == partyName select o).ToArray();
                foreach (var temp in queryC)
                {
                    if(temp.participants.IndexOf(strName) == -1)
                    {
                        temp.participants += "," + strName;
                    }
                }
                dmc.SubmitChanges();

                /*
                RestaurantsEntities db = new RestaurantsEntities();
                RESTAURANT restDetails = (from RESTAURANT in db.RESTAURANTs
                                        where RESTAURANT.REST_ID == RestID
                                        select RESTAURANT).Single();
                restDetails.HOURS_ID = HoursID;
                restDetails.REST_WEBSITE = Web;
                restDetails.REST_DESC = Desc;
                db.SaveChanges();
                var manColor = new ManColor
                {
                    nickName = party.nickName,
                    color = party.color
                };
                dmc.ManColor.InsertOnSubmit(manColor);
                dmc.SubmitChanges();
                */
                return RedirectToAction("Index");
            }

            return View();
        }
        public JsonResult MyProperty()
        {
            var query = from t1 in dmc.ManChoose
                        join t2 in dmc.Parties on t1.partyName equals t2.partyName
                        join t3 in dmc.ManColor on t1.nickName equals t3.nickName
                        select new { t1.nickName, t1.partyName, t1.time, t3.color };
            List<string> resuslt = new List<string> { };
            foreach (var item in query)
            {
                var temp = new
                {
                    nickName = item.nickName,
                    partyName = item.partyName,
                    time = item.time,
                    color = item.color
                };
                resuslt.Add(JsonConvert.SerializeObject(temp));
            }
            return Json(JsonConvert.SerializeObject(resuslt));
        }
    }
}
