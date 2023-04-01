using NsSchool.Business.Dtos;
using NsSchool.Business.Types;
using NsSchool.Data.Entites;
using NsSchool.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NsSchool.Business.Services
{
    public interface IAnnouncementService
    {
        List<AnnouncementDto> GetAnnouncementList();
        ServiceMessage AddAnnouncement(AnnouncementDto announcementDto);
        void DeleteAnnouncement(int id);
        AnnouncementEditDto GetAnnouncement(int id);
        void EditAnnouncement(AnnouncementEditDto announcementEditDto);
        AnnouncementDto GetByIdAnnouncement(int id);

    }
}
