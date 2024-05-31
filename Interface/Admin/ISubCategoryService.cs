using Kungumam.Models;
using Kungumam.Models;
using System.Data;

namespace Kungumam.Interface.Admin
{
    public interface ISubCategoryService
    {
        string SubCategoryCRUD(SubCategory Cy);
        string StatusDeleteMR(string tag, int id);

        DataTable GetAllListCategory(string strStatus);
        DataTable GetMagazine();
        DataTable GetAllCategory(string id);
    }
}
