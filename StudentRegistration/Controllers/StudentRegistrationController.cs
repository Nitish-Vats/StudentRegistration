using StudentRegistration.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentRegistration.Controllers
{
    public class StudentRegistrationController : Controller
    {
        // GET: StudentRegistration
        private readonly EmployeeDBEntities entities;
        public StudentRegistrationController()
        {
            entities = new EmployeeDBEntities();
        }
        public ActionResult Index(string SearchText = "", int page = 1, int pageSize = 4, string userID = "")
        {
            var students = entities.Students.Include("State").Include("City").ToList();
            if (page < 1)
                page = 1;
            var studentDetails = students.Select(s => new StudentDetails
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Mobile=s.Mobile,
                TextAbout=s.TextAbout,
                ImagePath=s.PhotoAddress,
                StateName = s.State?.Name, // Assuming State has a Name property
                CityName = s.City?.Name,   // Assuming City has a Name property
                                           // Map other properties as needed
            }).ToList();

            var query = studentDetails.AsQueryable();
            if (!string.IsNullOrEmpty(SearchText))
            {
                query = studentDetails.Where(p => p.Name.Contains(SearchText)).AsQueryable();
            }
            int totalRecords = query.Count();
            var data = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var pager = new Pager(totalRecords, page, pageSize, "StudentRegistration", "Index");
            ViewBag.Pager = pager;


            return View(data);
           
        }
        public ActionResult Create()
        {
            ViewBag.State = new SelectList(entities.States.ToList(), "Id", "Name");
            
            var model = new StudentDetails();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(StudentDetails model)
        {
            ViewBag.State = new SelectList(entities.States.ToList(), "Id", "Name");
            
            if (!ModelState.IsValid)
            {
                
                return View(model);
            }

            if (model.Photo.ContentLength > 250 * 1024 ||
                !(new[] { ".jpg",".jpeg", ".png" }.Contains(Path.GetExtension(model.Photo.FileName).ToLower())))
            {
                ModelState.AddModelError("Photo", "Photo must be .jpg or .png and less than 250 KB.");
                
                return View(model);
            }

            var student = new Student
            {
                Name = model.Name,
                Email = model.Email,
                Mobile = model.Mobile,
                StateId = model.StateId,
                CityId = model.CityId,
                TextAbout = model.TextAbout,
                PhotoAddress = SavePhoto(model.Photo)
            };

            entities.Students.Add(student);
            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult GetCities(int stateId)
        {
            var cities = entities.Cities.Where(c => c.StateId == stateId)
                .Select(c => new { c.Id, c.Name }).ToList();
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        private string SavePhoto(HttpPostedFileBase photo)
        {
            var fileName = Path.GetFileNameWithoutExtension(photo.FileName);
            var extension = Path.GetExtension(photo.FileName);
            var finalName = $"{fileName}_{Guid.NewGuid()}{extension}";
            var path = Path.Combine(Server.MapPath("~/Content"), finalName);
            photo.SaveAs(path);
            return $"/Content/{finalName}";
        }


        public ActionResult Update(int Id)
        {
            var student = entities.Students.Include("State").Include("City")
                .FirstOrDefault(s => s.Id == Id);

            if (student == null)
            {
                return RedirectToAction("Index");
            }

            
            var studentViewModel = new StudentDetails
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Mobile = student.Mobile,
                StateId = (int)student.StateId,
                CityId = (int)student.CityId,
                TextAbout = student.TextAbout,
                ImagePath = student.PhotoAddress,
        };
            studentViewModel.Cities = entities.Cities
                .AsEnumerable() 
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
            ViewBag.State = new SelectList(entities.States.ToList(), "Id", "Name");

            return View(studentViewModel);
        }


        [HttpPost]
        public ActionResult Update(StudentDetails model)
        {
            ViewBag.State = new SelectList(entities.States.ToList(), "Id", "Name");
            model.Cities = entities.Cities
                .AsEnumerable()
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
            if (!ModelState.IsValid)
            {

                return View(model);
            }

            if (model.Photo.ContentLength > 250 * 1024 ||
                !(new[] { ".jpg", ".jpeg", ".png" }.Contains(Path.GetExtension(model.Photo.FileName).ToLower())))
            {
                ModelState.AddModelError("Photo", "Photo must be .jpg or .png and less than 250 KB.");

                return View(model);
            }
            var student = entities.Students.FirstOrDefault(s => s.Id == model.Id);
            if (student == null)
            {
                return View(model);
            }
            student.Name = model.Name;
            student.Email = model.Email;
            student.Mobile = model.Mobile;
            student.StateId = model.StateId;
            student.CityId = model.CityId;
            student.TextAbout = model.TextAbout;
            student.PhotoAddress = SavePhoto(model.Photo);

            entities.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                
                var student = entities.Students.FirstOrDefault(s => s.Id == id);
                if (student != null)
                {
                    entities.Students.Remove(student);
                    entities.SaveChanges();
                }
                else
                {
                    return HttpNotFound("Student not found");
                }

                return RedirectToAction("Index"); 
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(500, "Internal server error");
            }
        }


    }
}