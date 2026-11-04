using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
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
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Navnet skal være mellem 3 og 100 tegn.")]
        public string Name { get; set; } = string.Empty;
        
        [BindProperty]
        [Required]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Beskrivelsen skal være mellem 10 og 250 tegn.")]
        public string Description { get; set; } = string.Empty;

       

        public IActionResult OnPostCreate()
        {

            Activity newActivity = new Activity(0, Name, Description);

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
                _repo.Create(newActivity);
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