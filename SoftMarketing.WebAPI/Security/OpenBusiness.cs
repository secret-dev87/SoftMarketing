using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SoftMarketing.WebAPI.Security
{
    public class OBusiness_CPT_Request
    {
        public int planId { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int? seller_number { get; set; }
        public string password { get; set; }
        public string confirmed_password { get; set; }
        public int countryId { get; set; }
        public int phone_countryId { get; set; }
        public string name { get; set; }
    }
    public class OBusiness_CPT_Response
    {
        public string amount { get; set; }
        public string currency { get; set; }
        public string mtx { get; set; }
        public int attempts { get; set; }
        public string sub_accounts_id { get; set; }
        public string id { get; set; }
        public string entity { get; set; }
        public string status { get; set; }
        public udf udf { get; set; }
        public customer customer { get; set; }
        public string error { get; set; } //1.mtx already used
        public string error_code { get; set; }//1.PT_MTX_D
        public string request_id { get; set; }
    }
    public class OBusiness_Webhook
    {
        public string amount { get; set; }
        public string currency { get; set; }
        public string mtx { get; set; }
        public int attempts { get; set; }
        public string sub_accounts_id { get; set; }
        public string id { get; set; }
        public string entity { get; set; }
        public string status { get; set; }
        public udf udf { get; set; }
        public customer customer { get; set; }
        [JsonProperty("event")]
        public string Event { get; set; }
        public Payment payment { get; set; }//1.PT_MTX_D
    }
    public class Payment
    {
        public string amount { get; set; }
        public string currency { get; set; }
        public string payment_error_code { get; set; }
        public string payment_error_description { get; set; }
        public string id { get; set; }
        public string entity { get; set; }
        public string status { get; set; }
        public PaymentInstrument payment_instrument { get; set; }
        public customer customer { get; set; }
        public Card card { get; set; }
    }
    public class PaymentInstrument
    {
        public string entity { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int type_id { get; set; }
        public string type_name { get; set; }
    }
    public class udf
    {
        public string seller_number { get; set; }
        public string snv { get; set; }
        public int countryId { get; set; }
        public int phone_countryId { get; set; }
        public string name { get; set; }
        public int planId{ get; set; }

    }
    public class customer
    {
        public string contact_number { get; set; }
        public string email_id { get; set; }
        public string id { get; set; }
        public string entity { get; set; }
    }
    public class Card
    {
        public string customer_id { get; set; }
        public string card_type { get; set; }
        public string card_network { get; set; }
        public string id { get; set; }
        public string entity { get; set; }
    }
}
