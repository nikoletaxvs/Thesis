using ThesisOct2023.Models;

namespace ThesisOct2023.Repositories
{
	public interface IMenuRepository
	{
		public Menu GetMenu(int id);
		public void PostMenu(Menu menu);
	}
}
