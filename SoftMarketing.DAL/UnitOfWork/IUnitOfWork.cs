using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftMarketing.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        IDataAccessBase Repository<T>() where T : class, new();
        IDataAccessBase GenericRepository(Type T);
        void Dispose();
    }

    public interface IDataAccessBase
    {
        IDbConnection Connection { get; set; }
        DbTransaction DbTransaction { get; set; }

    }

    public abstract class DataAccessBase : IDataAccessBase
    {
        protected MySqlConnection Connection { get; set; }
        protected MySqlTransaction DbTransaction { get; set; }

        IDbConnection IDataAccessBase.Connection
        {
            get
            {
                return Connection;
            }

            set
            {
                Connection = (MySqlConnection)value;
            }
        }

        DbTransaction IDataAccessBase.DbTransaction
        {
            get
            {
                return DbTransaction;
            }

            set
            {
                DbTransaction = (MySqlTransaction)value;
            }
        }

        public static bool ColumnExists(IDataReader reader, string columnName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        //public static RequestMaster Get<T>(Func<T> action, OracleDataReader reader, RequestMaster masterEntity = null, bool mapMasterRequestFlag = false)
        //{
        //    T value;

        //    int columnIndx = 0;

        //    if (mapMasterRequestFlag && masterEntity != null)
        //    {
        //        if (ColumnExists(reader, DbStaticNames.ID_OUT_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.ID_OUT_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.Id = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }

        //        if (ColumnExists(reader, DbStaticNames.SERVICE_TRANS_ID_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.SERVICE_TRANS_ID_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.ServiceTransId = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }


        //        if (ColumnExists(reader, DbStaticNames.REQUEST_DATE_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.REQUEST_DATE_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.CreationDate = Convert.ToDateTime(reader[columnIndx]);
        //            }
        //        }


        //        if (ColumnExists(reader, DbStaticNames.CREATED_BY_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.CREATED_BY_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.CreatedBy = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }

        //        if (ColumnExists(reader, DbStaticNames.LOOKUP_PRIORITY_ID_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.LOOKUP_PRIORITY_ID_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.PriorityId = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.REF_REQUEST_ID_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.REF_REQUEST_ID_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.ReferenceRequestId = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.APPLICANT_ID_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.APPLICANT_ID_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.ApplicantId = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.APPLICANT_INFORMATION_ID_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.APPLICANT_INFORMATION_ID_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.ApplicantInformationId = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }

        //        if (ColumnExists(reader, DbStaticNames.LOOKUP_APPLICATION_ID_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.LOOKUP_APPLICATION_ID_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.ApplicationId = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }

        //        if (ColumnExists(reader, DbStaticNames.REQUEST_DRAFT_ID_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.REQUEST_DRAFT_ID_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.RequestDraftId = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.ESTABLISHMENT_ID_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.ESTABLISHMENT_ID_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.EstablishmentId = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.LOOKUP_ADMIN_REGION_ID_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.LOOKUP_ADMIN_REGION_ID_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.AdministrativeRegionId = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.ARABIC_APPLICANT_NAME_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.ARABIC_APPLICANT_NAME_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.ArabicApplicantName = Convert.ToString(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.ENGLISH_APPLICANT_NAME_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.ENGLISH_APPLICANT_NAME_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.EnglishApplicantName = Convert.ToString(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.LOOKUP_PREFERED_LANGUAGE_ID_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.LOOKUP_PREFERED_LANGUAGE_ID_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.PreferedLanguageId = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.APPLICANT_MOBILE_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.APPLICANT_MOBILE_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.ApplicantMobile = Convert.ToString(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.APPLICANT_EMAIL_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.APPLICANT_EMAIL_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.ApplicantEmail = Convert.ToString(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.IBAN_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.IBAN_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.IBAN = Convert.ToString(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.NUMBER_OF_YEARS_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.NUMBER_OF_YEARS_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.NumberOfYears = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.LOOKUP_MODULE_TRANS_REASON_ID_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.LOOKUP_MODULE_TRANS_REASON_ID_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.TransactionReasonId = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.ID_OUT_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.ID_OUT_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.Id = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.APPLICANT_EIDA_NUMBER_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.APPLICANT_EIDA_NUMBER_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.ApplicantIdentityNumber = Convert.ToString(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.IS_IDENTITY_SCANNED_FLAG_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.IS_IDENTITY_SCANNED_FLAG_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.IsIdentityScanned = Convert.ToBoolean(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.MASTER_INSURANCE_POLICY_NUMBER_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.MASTER_INSURANCE_POLICY_NUMBER_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.MasterInsurancePolicyNumber = Convert.ToString(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.IS_USING_SPONSOR_IDENTITY_FLAG_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.IS_USING_SPONSOR_IDENTITY_FLAG_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.IsUsingSponsorIdentityCard = Convert.ToBoolean(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.LOOKUP_FREE_DEPOSIT_REASON_ID_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.LOOKUP_FREE_DEPOSIT_REASON_ID_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.FreeDepositCaseId = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.FEES_EXEMPTION_REASON_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.FEES_EXEMPTION_REASON_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.ExemptionReason = Convert.ToString(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.FEES_EXEMPTION_FLAG_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.FEES_EXEMPTION_FLAG_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.isExempted = Convert.ToBoolean(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.LAST_SYNC_WITH_EXTERNAL_TIME_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.LAST_SYNC_WITH_EXTERNAL_TIME_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                //no need to map this prop now
        //                //   masterEntity.last = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.IS_EXCEPTIONAL_REQUEST_FLAG_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.IS_EXCEPTIONAL_REQUEST_FLAG_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.IsExceptionalRequestFlag = Convert.ToBoolean(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.LOOKUP_EXTERNAL_SYSTEM_ID_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.LOOKUP_EXTERNAL_SYSTEM_ID_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.LookupExternalSystemId = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.LOOKUP_EXEMPTION_TYPES_ID_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.LOOKUP_EXEMPTION_TYPES_ID_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.ExemptionTypeId = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.LOOKUP_TRAVEL_TRANS_TYPE_ID_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.LOOKUP_TRAVEL_TRANS_TYPE_ID_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                //  masterEntity.TravelTransTypeId = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.LOOKUP_EXTERNAL_SYNC_FLAG_ID_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.LOOKUP_EXTERNAL_SYNC_FLAG_ID_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                //  masterEntity.SyncStatusId = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.HAS_FINE_EXEMPTION_FLAG_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.HAS_FINE_EXEMPTION_FLAG_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.IsBeneficiariesofFinesExemptions = Convert.ToBoolean(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.EXTERNAL_REFERENCE_NUMBER_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.EXTERNAL_REFERENCE_NUMBER_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                // masterEntity.exter = Convert.ToInt32(reader[columnIndx]);
        //            }
        //        }
        //        if (ColumnExists(reader, DbStaticNames.REQUEST_NUMBER_OUT_COLUMN_NAME))
        //        {
        //            columnIndx = reader.GetOrdinal(DbStaticNames.REQUEST_NUMBER_OUT_COLUMN_NAME);
        //            if (!reader.IsDBNull(columnIndx))
        //            {
        //                masterEntity.RequestNumber = Convert.ToString(reader[columnIndx]);
        //            }
        //        }




        //        //last to run the lambda func
        //        value = action();
        //    }
        //    else
        //    {
        //        //without mapping the request master just run the callback func
        //        value = action();
        //    }


        //    return masterEntity;
        //}

        //public static RequestMaster GetMasterEntity(OracleDataReader reader, RequestMaster entity, System.Action<OracleDataReader, RequestMaster> callback, bool mapMasterRequestFlag = false)
        //{
        //    int columnIndx = 0;
        //    columnIndx = reader.GetOrdinal("ID");
        //    Type t = (entity).GetType();

        //    if (!reader.IsDBNull(columnIndx))
        //        entity.Id = Convert.ToInt32(reader[columnIndx]);

        //    var x = new ExpandoObject() as IDictionary<string, Object>;
        //    //x.Add((typeof(T)
        //    //.GetProperties(), "10");


        //    UserDto userDto = new UserDto();

        //    Mapper<RequestMaster>.Map(x as ExpandoObject, entity, reader);
        //    entity = null;
        //    return null;
        //}

        //protected R Call<R>(IProcedureCaller<R> caller, IProcedureBuilder builder)
        //{
        //    return caller.Call(builder, Connection);
        //}

        //protected void Call(IProcedureCaller caller, IProcedureBuilder builder)
        //{
        //    caller.Call(builder, Connection);
        //}

        //protected List<string> GetReaderColumnNames(OracleDataReader reader)
        //{
        //    var readerColumns = new List<string>();

        //    for (int i = 0; i < reader.FieldCount; i++)
        //    {
        //        readerColumns.Add(reader.GetName(i));
        //    }

        //    return readerColumns;
        //}

        //public static string CheckIsArLang(string value)
        //{
        //    int errorCounter = System.Text.RegularExpressions.Regex.Matches(value, @"[a-zA-Z]").Count;
        //    if (errorCounter == 0)
        //        return value;
        //    else
        //        return string.Empty;

        //}

        //public static bool CheckValidPhone(string value)
        //{
        //    if (string.IsNullOrEmpty(value))
        //        return true;
        //    else if (value == "0")
        //        return false;
        //    else if (value == "00")
        //        return false;
        //    else
        //        return true;

        //}

        //public static T GetDynamicEntityFromReaderByDBFeildNameAttr<T>(IDataReader reader)
        //{
        //    T entity = (T)Activator.CreateInstance(typeof(T));
        //    for (int i = 0; i < reader.FieldCount; i++)
        //    {
        //        try
        //        {
        //            var prop = (from property in entity.GetType().GetProperties()
        //                        from attrib in property.GetCustomAttributes(typeof(Entities.CustomAttribute.DBFeildName), false).Cast<Entities.CustomAttribute.DBFeildName>()
        //                        where attrib.name == reader.GetName(i)
        //                        select property).FirstOrDefault();
        //            if (prop != null && !reader.IsDBNull(i))
        //            {
        //                var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
        //                var targetValue = Convert.ChangeType(reader[i], targetType);
        //                entity.GetType().GetProperty(prop.Name).SetValue(entity, targetValue);
        //            }
        //        }
        //        catch (Exception)
        //        {

        //        }
        //    }
        //    return entity;
        //}
    }
}
