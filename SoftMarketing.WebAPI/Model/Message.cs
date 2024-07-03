namespace SoftMarketing.WebAPI.Model
{
    public class Message
    {
        public string api_key { get; set; }
        public string[] messages { get; set; }
        public int user_id { get; set; }
    }
}
