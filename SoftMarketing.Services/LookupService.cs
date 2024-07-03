using SoftMarketing.DAL.DataAccess;
using SoftMarketing.DAL.UnitOfWork;
using SoftMarketing.Model;
using SoftMarketing.Model.SalesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.Service
{
    public class LookupService
    {
        public List<CommonApp> GetMessagingApp()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                LookupDAL LookupDAL = (LookupDAL)unitOfWork.Repository<LookupDAL>();
                return LookupDAL.GetCommonApp(1);
            }
        }
        public List<CommonApp> GetSocialApp()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                LookupDAL LookupDAL = (LookupDAL)unitOfWork.Repository<LookupDAL>();
                return LookupDAL.GetCommonApp(0);
            }
        }
        public List<CategoryType> GetCategoryTypes()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                LookupDAL LookupDAL = (LookupDAL)unitOfWork.Repository<LookupDAL>();
                return LookupDAL.GetCategoryTypes();
            }
        }

		public List<MainCategory> GetMainCategoryList(int categoryTypeId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                LookupDAL LookupDAL = (LookupDAL)unitOfWork.Repository<LookupDAL>();
                return LookupDAL.GetMainCategoryList(categoryTypeId);
            }
        }
		public List<ChildCategory> GetChildCategoryList(int categoryDetailId)
		{
            using (var unitOfWork = new UnitOfWork())
            {
                LookupDAL LookupDAL = (LookupDAL)unitOfWork.Repository<LookupDAL>();
                return LookupDAL.GetChildCategoryList(categoryDetailId);
            }
        }
    }
}
