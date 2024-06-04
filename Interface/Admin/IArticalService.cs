using Kungumam.Models;

using System.Data;

namespace Kungumam.Interface.Admin
{
    public interface IArticalService
    {
      
        DataTable GetMagazine();
        DataTable GetAllCategory();
    }
}
