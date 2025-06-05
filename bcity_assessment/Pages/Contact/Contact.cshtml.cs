using bcity_assessment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bcity_assessment.Pages.Contact;

public class Contact : PageModel
{
    private readonly BcityAssessmentContext _context;
    private readonly ILogger<Contact> _logger;
    private readonly ContactService _contactService;

    public Contact(BcityAssessmentContext context, ILogger<Contact> logger, ContactService contactService)
    {
        _context = context;
        _logger = logger;
        _contactService = contactService;
    }
    
    [BindProperty]
    public List<bcity_assessment.Contact> Contacts { get; set; } = new List<bcity_assessment.Contact>();
    
    public void OnGet()
    {
        Contacts = _contactService.GetContacts().Result;
    }
}