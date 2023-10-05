namespace ThesisOct2023.Models
{
	public class NutritionInfoProcessor
	{
		public async Task LoadNutritionFacts(int factNumber = 0)
		{
			string url = "";
			if (factNumber > 0)
			{
				url = $"https://xkcd.com/{factNumber}/info.0.json";
			}
			else
			{
				url = "https://xkcd.com//info.0.json";
			}

			using(HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
			{
				if (response.IsSuccessStatusCode)
				{

				}
			}
		}
	}
}
