using R52_Ex_07_Evidence.Models;
using R52_Ex_07_Evidence.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace R52_Ex_07_Evidence.Controllers
{
    public class CandidatesController : Controller
    {
        CandidateDbContext db = new CandidateDbContext();
        // GET: Candidates
        public ActionResult Index()
        {
            return View(db.Candidates.Include(x=> x.Qualifications).ToList());
        }
        /// CREATE
        /// ////////////////////////////
        /// 
        public ActionResult Create()
        {
            ViewBag.PostList = new List<SelectListItem>
            {
                new SelectListItem{ Text="Select", Value="", Selected=true},
                new SelectListItem{Text="Executive", Value="Executive"},
                new SelectListItem{Text="Marketing Executive", Value="Marketing Executive"},
                new SelectListItem{Text="Bell boy", Value="Bell boy"}
            };
            return View();
        }
     
        [HttpPost]
        public ActionResult Create(CandidateInputModel data)
        {
            if (ModelState.IsValid)
            {
                Candidate c = new Candidate
                {
                    CandidateName = data.CandidateName,
                    BirthDate = data.BirthDate,
                    AppliedFor = data.AppliedFor,
                    ExpectedSalary = data.ExpectedSalary,
                    WorkFromHome = data.WorkFromHome,
                    Picture = data.Picture
                };
                foreach(var q in data.Qualifications)
                {
                    c.Qualifications.Add(q);
                }
                db.Candidates.Add(c);
                db.SaveChanges();
                return Json(new {id=c.CandidateId});
            }
            return Json(null);
        }
        public PartialViewResult AddQuationToForm(CandidateInputModel c=null,int? index=null)
        {
            if(c== null) c = new CandidateInputModel();
            if (index.HasValue)
            {
                if(index < c.Qualifications.Count)
                {
                    c.Qualifications.RemoveAt(index.Value);
                }
            }
            else
            {
                c.Qualifications.Add(new Qualification());
            }
            
            return PartialView("_QualificationForm", c);
        }
        public ActionResult UploadImage(int id, ImageUpload pic)
        {
            if(ModelState.IsValid)
            {
                if(pic.Picture != null)
                {
                    Candidate c = db.Candidates.First(x=> x.CandidateId== id);
                    string ext = Path.GetExtension(pic.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())+ext;
                    string savePath = Path.Combine(Server.MapPath("~/Pictures"), fileName);
                    pic.Picture.SaveAs(savePath);
                    c.Picture = fileName;
                    db.SaveChanges();
                    return Json(fileName);
                }
            }
            return Json(null);
        }
        ///Create
        
        ////Edit
        /// ////////////////////////////////////////////////////
        ///  
        public ActionResult Edit( int id)
        {
            ViewBag.PostList = new List<SelectListItem>
            {
                new SelectListItem{ Text="Select", Value="", Selected=true},
                new SelectListItem{Text="Executive", Value="Executive"},
                new SelectListItem { Text = "Marketing Executive", Value = "Marketing Executive" },
                new SelectListItem { Text = "Bell boy", Value = "Bell boy" }
            };
            var candidate=db.Candidates.Include(c => c.Qualifications).First(c=> c.CandidateId== id);

            return View(
                new CandidateEditModel
                {
                    CandidateId = candidate.CandidateId,
                    CandidateName = candidate.CandidateName,
                    BirthDate = candidate.BirthDate,
                    AppliedFor = candidate.AppliedFor,
                    ExpectedSalary = candidate.ExpectedSalary,
                    WorkFromHome = candidate.WorkFromHome,
                    Picture= candidate.Picture,
                    Qualifications = candidate.Qualifications.ToList()
                }
                ); 
        }
        [HttpPost]
        public ActionResult Edit(CandidateEditModel model)
        {
            var existing = db.Candidates.First(c=>c.CandidateId== model.CandidateId);
            if (ModelState.IsValid)
            {
                existing.CandidateName= model.CandidateName;
                existing.BirthDate= model.BirthDate;
                existing.AppliedFor= model.AppliedFor;
                existing.WorkFromHome= model.WorkFromHome;
                existing.ExpectedSalary = model.ExpectedSalary;
                db.Qualifications.RemoveRange(existing.Qualifications.ToList());
                foreach(var q in model.Qualifications)
                {
                    q.CandidateId= existing.CandidateId;
                    db.Qualifications.Add(q);
                }
                db.SaveChanges();
                return Json(existing.CandidateId);
            }
            return Json(null);
        }
        public PartialViewResult AddQuationToEditForm(CandidateEditModel c,  int? index = null)
        {
          
            if (index.HasValue)
            {
                if (index < c.Qualifications.Count)
                {
                    c.Qualifications.RemoveAt(index.Value);
                }
            }
            

            return PartialView("_QualificationEditForm", c);
        }
        public PartialViewResult AddMore(CandidateEditModel c , int? index = null)
        {
            if (c == null) c = new CandidateEditModel();
            if (index.HasValue)
            {
                if (index < c.Qualifications.Count)
                {
                    c.Qualifications.RemoveAt(index.Value);
                }
            }
            else
            {
                c.Qualifications.Add(new Qualification());
            }

            return PartialView("_QualificationEditForm", c);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var existing = db.Candidates.FirstOrDefault(c=> c.CandidateId== id);
            if(existing != null)
            {
                db.Candidates.Remove(existing);
                db.SaveChanges();
                return Json(existing.CandidateId);
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}