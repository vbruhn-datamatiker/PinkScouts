using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PSClassLib.Interface;
using PSClassLib.Model;

namespace PinkScouts.Pages
{
    public class ActivitiesModel : PageModel
    {
        private readonly IRepo<Activity> _repo;

        public List<Activity> Activities { get; private set; } = new List<Activity>();
        public string? ErrorMessage { get; private set; }

        public ActivitiesModel(IRepo<Activity> repo)
        {
            _repo = repo;
        }

        [BindProperty]
        public Activity NewActivity { get; set; } = new Activity();

        public IActionResult OnPostCreate()
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    Activities = _repo.ReadAll();
                }
                catch (Exception)
                {
                    ErrorMessage = "Fejl";
                }
                return Page();
            }
            try
            {
                _repo.Create(NewActivity);
                return RedirectToPage();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"kunne ikke oprette {ex.Message}";
                try { Activities = _repo.ReadAll(); } catch { }
                return Page();

            }
        }
    


        public void OnGet()
        {
            try
            {
                Activities = _repo.ReadAll();
            }
            catch (Exception)
            {
                ErrorMessage = "Vi kunne ikke hente aktiviteterne lige nu. Prøv igen senere.";
            }
        }
    }
}