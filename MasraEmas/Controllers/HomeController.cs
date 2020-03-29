using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasraEmas.Models;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using Newtonsoft.Json;

namespace MasraEmas.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }




        [HttpPost]
        public JsonResult GetOwnerData()

        {
            List<EmasOwner> result = new List<EmasOwner>();
            SqlConnection sqlcon = new SqlConnection(@"Data Source=(local);Initial Catalog=Masra2;Integrated Security=true");
            if (sqlcon.State == ConnectionState.Closed)

                sqlcon.Open();
            SqlCommand sqlCmd = new SqlCommand("OwnerViewAll", sqlcon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = sqlCmd.ExecuteReader();
            while (dr.Read())
            {
                EmasOwner owner = new EmasOwner()
                {
                    ID = Convert.ToInt32(dr["ID"].ToString()),
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    KnownAs = dr["KnownAs"].ToString(),
                    ICNo = dr["ICNo"].ToString(),
                    ContactNo = dr["ContactNo"].ToString(),
                    HouseNo = dr["HouseNo"].ToString(),
                    RoadName = dr["RoadName"].ToString(),
                    Zone = dr["Zone"].ToString(),
                    Notice = dr["Notice"].ToString(),
                };
                result.Add(owner);
            }
            ViewBag.result = result;

            return Json(result, JsonRequestBehavior.AllowGet);


        }


        [HttpPost]
        public JsonResult Save(EmasOwner owner)
        {
            SqlConnection sqlcon = new SqlConnection(@"Data Source=(local);Initial Catalog=Masra2;Integrated Security=true");
            if (sqlcon.State == ConnectionState.Closed)

                sqlcon.Open();
            SqlCommand sqlCmd = new SqlCommand("CreateNewOwner", sqlcon);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@FirstName", owner.FirstName);
            sqlCmd.Parameters.AddWithValue("@LastName", owner.LastName);
            sqlCmd.Parameters.AddWithValue("@KnownAs", owner.KnownAs);
            sqlCmd.Parameters.AddWithValue("@ICNo", owner.ICNo);
            sqlCmd.Parameters.AddWithValue("@ContactNo", owner.ContactNo);
            sqlCmd.Parameters.AddWithValue("@HouseNo", owner.HouseNo);
            sqlCmd.Parameters.AddWithValue("@RoadName", owner.RoadName);
            sqlCmd.Parameters.AddWithValue("@Zone", owner.RoadName);
            sqlCmd.Parameters.AddWithValue("@Notice", owner.RoadName);
            sqlCmd.ExecuteNonQuery();
            sqlcon.Close();
            return Json("success", JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            MasraContext db = new MasraContext();
            EmasOwner owner = db.EmasOwners.Where(a => a.ID == id).FirstOrDefault();
            db.EmasOwners.Remove(owner);
            db.SaveChanges();
            return Json("success", JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetEdit(int id)
        {
            MasraContext db = new MasraContext();
            var owner = db.EmasOwners.Where(a => a.ID == id).FirstOrDefault();
            
            return Json(new { owner, message = "Data updated successfully" }, JsonRequestBehavior.AllowGet);
        }


        /*=================Edit Action Start==================*/
        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new EmasOwner());
            else
            {
                using (MasraContext db = new MasraContext())
                {
                    return View(db.EmasOwners.Where(x => x.ID == id).FirstOrDefault<EmasOwner>());
                }
            }
        }

        [HttpPost]
        public ActionResult AddOrEdit(EmasOwner owner)
        {
            using (MasraContext db = new MasraContext())
            {
                if (owner.ID == 0)
                {
                    db.EmasOwners.Add(owner);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(owner).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }


        }
        /*=================Edit Action End==================*/

        public JsonResult postdata(EmasOwner owner)
        {
            if (owner.ID > 0)
            {
                MasraContext db = new MasraContext();
                EmasOwner crud = db.EmasOwners.Where(m => m.ID == owner.ID).FirstOrDefault<EmasOwner>();
                crud.FirstName = owner.FirstName;
                crud.LastName = owner.LastName;
                crud.KnownAs = owner.KnownAs;
                crud.ICNo = owner.ICNo;
                crud.ContactNo = owner.ContactNo;
                crud.HouseNo = owner.HouseNo;
                crud.RoadName = owner.RoadName;
                crud.Zone = owner.Zone;
                db.SaveChanges();

                return Json(new { result = true, message = "Data updated successfully" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                MasraContext db = new MasraContext();
                EmasOwner crud = new EmasOwner();
                crud.FirstName = owner.FirstName;
                crud.LastName = owner.LastName;
                crud.KnownAs = owner.KnownAs;
                crud.ICNo = owner.ICNo;
                crud.ContactNo = owner.ContactNo;
                crud.HouseNo = owner.HouseNo;
                crud.RoadName = owner.RoadName;
                crud.Zone = owner.Zone;
                db.EmasOwners.Add(crud);
                db.SaveChanges();

                return Json(new { result = true, message = "Data saved successfully" }, JsonRequestBehavior.AllowGet);
            }

            }
        }
    }
