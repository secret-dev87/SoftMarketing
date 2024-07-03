using SoftMarketing.DAL.DataAccess.SalesDAL;
using SoftMarketing.DAL.UnitOfWork;
using SoftMarketing.Model.SalesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Services.Sales
{
    public class CenterUserService
    {
        public CenterUser GetCenterUser(CenterUser CenterUser)
        {
            int numOfEffectedRows = -1;
            using (var unitOfWork = new UnitOfWork())
            {
                CenterUserDAL CenterUserDAL = (CenterUserDAL)unitOfWork.Repository<CenterUserDAL>();
                CenterUser = CenterUserDAL.GetCenterUser(CenterUser.Phone, CenterUser.Password);
            }
            return CenterUser;
        }
    }
}
