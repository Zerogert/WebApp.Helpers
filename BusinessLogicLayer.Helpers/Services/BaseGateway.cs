using BusinessLogicLayer.Helpers.Models;
using MoreLinq;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Helpers.Services {
	public class BaseGateway : RestClient {
		public BaseGateway(string host) : base(host) {
		}

		protected virtual async Task<ServiceResult<IRestResponse>> ExecuteRequestAsync(IRestRequest request) {
			var response = await ExecuteAsync(request);
			return ServiceResult<IRestResponse>.Ok(response);
		}

		protected virtual async Task<ServiceResult<IRestResponse<T>>> ExecuteRequestAsync<T>(IRestRequest request) where T : class {
			var response = await ExecuteAsync<T>(request);
			return ServiceResult<IRestResponse<T>>.Ok(response);
		}

		protected virtual IRestRequest CreateRequest(string resource, Method method, params KeyValuePair<string, string>[] parameters) {
			var request = new RestRequest(resource, method, DataFormat.Json);

			parameters.ForEach(p => request.AddParameter(p.Key, p.Value));
			return request;
		}

		protected virtual IRestRequest CreateRequest<T>(string resource, Method method, T body, params KeyValuePair<string, string>[] parameters) {
			var request = CreateRequest(resource, method, parameters)
			   .AddJsonBody(body);

			return request;
		}

		protected ICollection<KeyValuePair<string, string>> CreateParameter(string key, string value) {
			return new Dictionary<string, string>() { { key, value } };
		}
	}
}
