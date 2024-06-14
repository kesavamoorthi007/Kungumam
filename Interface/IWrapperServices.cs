using Kungumam.Models;
using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace Kungumam.Interface
{
    public interface IWrapperService
    {
        string WrapperCRUD(List<IFormFile> file, Wrapper cy);
        DataTable GetAllWrapper(string strStatus);
        string StatusDeleteMR(string tag, int id);

        DataTable GetMagazine();
        DataTable GetEditWrapper(string id);
     
    }
}
