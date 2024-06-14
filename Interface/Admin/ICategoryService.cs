using Kungumam.Models;
using System.Data;

namespace Kungumam.Interface.Admin
{
    public interface ICategoryService
    {
        string CategoryCRUD(Category Cy);
        string StatusDeleteMR(string tag, int id);
        DataTable GetAllCategory(string strStatus);
        DataTable GetMagazine();
        DataTable GetEditCategory(string id);
    }
}
