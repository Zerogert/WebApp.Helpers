using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Helpers.Models {
	public class PaginationResult<T> {
		public ICollection<T> Data { get; set; }
	}
}
