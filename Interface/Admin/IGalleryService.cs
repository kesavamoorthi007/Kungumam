using Kungumam.Models;
using Kungumam.Models;
using System.Data;
namespace Kungumam.Interface.Admin
{
    public interface IGalleryService
    {
        DataTable GetMagazine();
        DataTable GetAllCategory(string id);
        DataTable GetAllSubCategory(string id);
    }
}