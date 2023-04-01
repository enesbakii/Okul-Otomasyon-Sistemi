using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Dtos
{
    public class StudentListDto
    {
        public int Id { get; set; }
        public string StudentIdentity { get; set; }
        public string StdentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string StudentUserName { get; set; }
        public string StudentPassword { get; set; }
        public string ImagePath { get; set; }
        public string ClassRoom { get; set; }
        public string ParentIdentity { get; set; }
        public string ParentFirstName { get; set; }
        public string ParentLastName { get; set; }
        public string Grade { get; set; }
    }
}
