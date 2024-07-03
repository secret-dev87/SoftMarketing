using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contrib = Dapper.Contrib.Extensions;

namespace SoftMarketing.Model
{
    public class Templates
    {
        public string usertemplate_id { get; set; }

        public int sales_userId { get; set; }

        public string name { get; set; }

        public string template { get; set; }

        public string templatetype { get; set; }

        public int templatetypeid { get; set; }
        public string sending_date { get; set; }
        public string sending_yr { get; set; }
    }

    public class Template
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Plese insert template name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Plese insert template Message.")]
        public string Message { get; set; }

        [Required(ErrorMessage = "Plese select template Type.")]
        public TemplateTypes Type { get; set; }

        public byte[] Photo { get; set; }
        public string PhotoUrl { get; set; }
    }

    /// <summary>
    /// This class used to get all country templates for a specific country
    /// assigned_to_user to know if has a subscribtion on this country template 
    /// </summary>
    public class UserTemplateCountry
    {
        public int id { get; set; }
        public int common_countryId { get; set; }
        public string name { get; set; }
        public string template { get; set; }
        public string date { get; set; }
        public bool assigned_to_user { get; set; }
        public int user_template_id { get; set; }
    }

    /// <summary>
    /// /This class used to subscribe the user in a country template  
    /// </summary>
    [Contrib.Table("marketing_user_template")]
    public class SubscriptionTemplate
    {
        [Contrib.Key]
        public int id { get; set; }
        public int sales_userId { get; set; }
        //public int? marketing_template_detailId { get; set; }
        public int? marketing_template_countryId { get; set; }
        //public string? template { get; set; }
        //public string? name { get; set; }
    }

    /// <summary>
    /// This class used to :
    /// 1-update predefined template ex: country template or custom template
    /// 2-insert new custom template
    /// 3-get user template
    /// </summary>
    public class User_Template
    {
        public int? usertemplate_id { get; set; }
        public int sales_userId { get; set; }
        [Required]
        public string? name { get; set; }
        [Required]
        public string? template { get; set; }
        [Required]
        public string? templatetype { get; set; }
        public int? templatetypeid { get; set; }
        public string? image { get; set; }
        public string? sending_date { get; set; }
        public string? sending_year { get; set; }
        public bool add_customer { get; set; }
        public bool add_owner { get; set; }
        public bool add_business { get; set; }
        public bool send_text_with_image { get; set; }
    }
    /// <summary>
    /// This class used to :
    /// to retrive template dates
    /// </summary>
    public class TemplateDate
    {
        public int? id { get; set; }
        public int marketing_user_template_id { get; set; }
        public string? date { get; set; }
    }
}
