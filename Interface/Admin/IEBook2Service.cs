using Kungumam.Models;
using System.Data;
namespace Kungumam.Interface.Admin
{
    public interface IEBook2Service
    {
        string StatusDeleteMR(string tag, int id);
        string EBook2CRUD(EBook2 Cy);

        DataTable GetMagazine();
        DataTable GetAllEBook2(string strStatus);
    }
}
