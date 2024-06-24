using Kungumam.Models;
using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace Kungumam.Interface
{
    public interface IRoadBlockService
    {
        string RoadBlockCRUD(List<IFormFile> file, RoadBlock cy);
        DataTable GetAllRoadBlock(string strStatus);
        string StatusDeleteMR(string tag, int id);
        DataTable GetEditRoadBlock(string id);
    }
}