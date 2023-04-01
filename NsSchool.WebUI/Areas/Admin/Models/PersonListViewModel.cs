﻿using NsSchool.Data.Enums;

namespace NsSchool.WebUI.Areas.Admin.Models
{
    public class PersonListViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityPerson { get; set; }
        public string Email { get; set; }
        public bool Gender { get; set; }
        public string Branch { get; set; }
        public DateTime BirthDay { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? ImagePath { get; set; }
		public UserTypeEnum UserType { get; set; }

		public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
