using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Helpers.Models {
	public class PaginationResult<T> {
		public ICollection<T> Data { get; set; }
		public int Index { get; set; }
		public int PageSize { get; set; }
		public int PagesTotal { get; set; }
		public int ItemsTotal { get; set; }
		public bool IsLastPage => (Index + 1) >= PagesTotal;
	}
}
