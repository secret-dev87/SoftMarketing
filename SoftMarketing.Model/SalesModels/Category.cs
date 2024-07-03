using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contrib = Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;

namespace SoftMarketing.Model.SalesModels
{
	[Contrib.Table("listing_category_type")]
	public class CategoryType
	{
		[Contrib.Key]
		public int id { get; set; }
		public string type { get; set; }
	}
	public class MainCategory
	{
		public int id { get; set; }
		public string name { get; set; }
	}
	public class ChildCategory
	{
		public int id { get; set; }
		public string name { get; set; }
		public bool has_child { get; set; }
	}
	public class UserCategory
	{
		public int? listing_category_detailId { get; set; }
		public int? marketing_user_template_id { get; set; }
		public int? marketing_template_detail_id { get; set; }
		public bool? assigned_to_user { get; set; }
		public string? name { get; set; }
		public string? template { get; set; }
		public string? image { get; set; }
    }
	//public class UserCategory
	//{
	//	public int p_listing_category_detailId { get; set; }
	//}

}
