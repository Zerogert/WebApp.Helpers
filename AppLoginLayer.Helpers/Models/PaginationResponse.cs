namespace AppLoginLayer.Helpers.Models {
	public class PaginationResponse<T> {
		public T Data { get; set; }
		public int Index { get; set; }
		public int PageSize { get; set; }
		public int PageTotal { get; set; }
		public int ItemCount { get; set; }
		public bool IsLastPage => (Index + 1) >= PageTotal;

	}
}
