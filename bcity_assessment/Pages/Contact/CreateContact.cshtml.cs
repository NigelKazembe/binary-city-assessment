using System.ComponentModel.DataAnnotations;
using bcity_assessment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bcity_assessment.Pages.Contact;

//Null checks should be implemented
public class CreateContact : PageModel
{
    private readonly ILogger<CreateContact> _logger;
    private readonly ContactService _contactService;

    public CreateContact(ILogger<CreateContact> logger, ContactService contactService)
    {
        this._logger = logger;
        this._contactService = contactService;
    }

    private ContactMapper Mapper { get; set; }
    private bcity_assessment.Contact ContactB { get; set; }
    //[BindProperty]
    //public bcity_assessment.Contact Contact { get; set; }
    
    [BindProperty]
    public ContactModel ContactModel { get; set; }

    public void OnGet()
    {
        
    }

    public IActionResult OnPost()
    {
        //If the modelstate is invalid, then
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
       Mapper = new ContactMapper();
       
       ContactB= new bcity_assessment.Contact();
       
       ContactB = Mapper.Map(ContactModel);
       
       _contactService.Create(ContactB);

        return RedirectToPage("/Contact/Contact");
    }
}

public class ContactModel
{
    [Required]
    [Display(Name = "Name")]
    [MinLength(2)]
    public string FirstName { get; set; }
    [Required]
    [Display(Name = "Last Name")]
    [MinLength(2)]
    public string LastName { get; set; }
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }
}

public class ContactMapper
{
    public bcity_assessment.Contact Map(ContactModel contactMdl)
    {
        bcity_assessment.Contact contact = new bcity_assessment.Contact();
        contact.Name = contactMdl.FirstName;
        contact.Email = contactMdl.Email;
        contact.Surname = contactMdl.LastName;
        
        return contact;
    }
}