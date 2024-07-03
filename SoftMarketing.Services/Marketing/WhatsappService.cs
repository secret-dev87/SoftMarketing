//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using SoftMarketing.DAL.DataAccess.MarketingDAL;
//using SoftMarketing.DAL.UnitOfWork;
//using SoftMarketing.Model.MarketingModels;
//using SoftMarketing.Model;

//namespace SoftMarketing.Services.Marketing
//{
//    public class WhatsappService
//    {
//        public string Add(Whatsapp whatsapp)
//        {
//            string id = string.Empty;
//            try
//            {
                
//                using (var unitOfWork = new UnitOfWork(true))
//                {
//                    WhatsappDAL whatsappDAL = (WhatsappDAL)unitOfWork.Repository<WhatsappDAL>();
//                    id = whatsappDAL.Add(whatsapp);
//                    unitOfWork.SaveChanges();
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
            
//            return id;
//        }
//        public int Update(Whatsapp whatsapp)
//        {
//            int numOfEffectedRows = -1;
//            using (var unitOfWork = new UnitOfWork(true))
//            {
//                WhatsappDAL whatsappDAL = (WhatsappDAL)unitOfWork.Repository<WhatsappDAL>();
//                numOfEffectedRows = whatsappDAL.Update(whatsapp);
//                unitOfWork.SaveChanges();
//            }
//            return numOfEffectedRows;
//        }
//        public int Delete(int id, int userId)
//        {
//            int numOfEffectedRows = -1;
//            using (var unitOfWork = new UnitOfWork(true))
//            {
//                WhatsappDAL whatsappDAL = (WhatsappDAL)unitOfWork.Repository<WhatsappDAL>();
//                numOfEffectedRows = whatsappDAL.Delete(id, userId);
//                unitOfWork.SaveChanges();
//            }
//            return numOfEffectedRows;
//        }
//        public Whatsapp Get(int userId)
//        {
//            int numOfEffectedRows = -1;
//            using (var unitOfWork = new UnitOfWork())
//            {
//                WhatsappDAL whatsappDAL = (WhatsappDAL)unitOfWork.Repository<WhatsappDAL>();
//                return whatsappDAL.Get(userId);
//            }

//        }
//        public List<Whatsapp> GetAll()
//        {
//            int numOfEffectedRows = -1;
//            using (var unitOfWork = new UnitOfWork())
//            {
//                WhatsappDAL whatsappDAL = (WhatsappDAL)unitOfWork.Repository<WhatsappDAL>();
//                return whatsappDAL.GetAll();
//            }

//        }
//    }
//}
