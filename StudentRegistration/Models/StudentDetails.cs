using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StudentRegistration.Models
{
    public class StudentDetails
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobile number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid mobile number.")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "State is required.")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "City is required.")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "About yourself is required.")]
        [MaxLength(250, ErrorMessage = "Only 250 characters are allowed.")]
        public string TextAbout { get; set; }
        [Required(ErrorMessage = "Photo is required.")]
        public HttpPostedFileBase Photo{ get; set; }
        public string ImagePath { get; set; }
        public string StateName { get; set; }

        public string CityName { get; set; }
        public List<SelectListItem> States { get; set; }
        public List<SelectListItem> Cities { get; set; }
    }
}