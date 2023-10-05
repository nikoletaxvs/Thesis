namespace ThesisOct2023.Models
{
	public class ApiHelper
	{
		// The whole application shares one http client , that's why its a static prop
		public static HttpClient ApiClient { get; set; }

		public static void InitializeClient() { 
			ApiClient= new HttpClient();
			ApiClient.DefaultRequestHeaders.Accept.Clear();
			ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
		}
	}
}
