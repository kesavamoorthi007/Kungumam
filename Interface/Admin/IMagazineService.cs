using Kungumam.Models;
using System.Collections.Generic;
using System.Collections;
using System.Data;


namespace Kungumam.Interface.Admin
{
	public interface IMagazineService
	{
        DataTable GetMagazine();
        DataTable GetAllMagazineService();
        string MagazineCRUD(Magazine Cy);
	}
}
