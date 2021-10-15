using BusinessLogicLayer.Helpers.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ePlaces.DataLayer.Extensions {
	public static class PaginationExtensions {

		public static IQueryable<T> Pagination<T>(this IQueryable<T> query, int indexPage, int pageSize = 20) {
			var skipCount = indexPage * pageSize;
			return query.Skip(skipCount).Take(pageSize);
		}

		public static IEnumerable<T> Pagination<T>(this IEnumerable<T> query, int indexPage, int pageSize = 20) {
			return query.AsQueryable().Pagination(indexPage, pageSize);
		}

		public static PaginationResult<T> ToPaginationResult<T>(this IQueryable<T> query, int indexPage, int pageSize = 20) {
			var itemsTotal = query.Count();
			var pagesTotal = (int)Math.Ceiling((double)itemsTotal / pageSize);
			var data = query.Pagination(indexPage, pageSize).ToList();
			var paginationResult = new PaginationResult<T>() {
				Data = data,
				Index = indexPage,
				PageSize = pageSize,
				ItemsTotal = itemsTotal,
				PagesTotal = pagesTotal
			};
			return paginationResult;
		}

		public static PaginationResult<T> ToPaginationResult<T>(this IEnumerable<T> query, int indexPage, int pageSize = 20) {
			return query.AsQueryable().ToPaginationResult(indexPage, pageSize);
		}
	}
}
