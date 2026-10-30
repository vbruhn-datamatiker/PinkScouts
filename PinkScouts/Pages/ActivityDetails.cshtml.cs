using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PSClassLib.Interface;
using PSClassLib.Model;
using PSClassLib.Repo;

namespace PinkScouts.Pages
{
	public class ActivityDetailsModel : PageModel
	{
		private readonly IRepo<Activity> _repo;

		public Activity? Activity { get; private set; }
		
		//[BindProperty]
		//public int Id { get; set; }

		public ActivityDetailsModel(IRepo<Activity> repo)
		{
			_repo = repo;
		}

		public IActionResult OnGet(int id)
		{
			try
			{
				Activity = _repo.GetById(id);
				return Page();
			}
			catch (Exception)
			{
				return NotFound();
			}
		}
		public IActionResult OnPostDelete(int id)
		{
		try {
				Activity = _repo.Remove(id);
				return RedirectToPage("Activities");
		}
		catch (Exception){
				return NotFound();
		}
		}
	}
}
