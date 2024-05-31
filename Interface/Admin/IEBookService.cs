using Kungumam.Models;
using System.Data;
namespace Kungumam.Interface.Admin
{
    public interface IEBookService
    {
        
        string StatusDeleteMR(string tag, int id);
        string EBookCRUD(EBook Cy);
       
        DataTable GetMagazine();
        DataTable GetAllEBook(string strStatus);
    }
}