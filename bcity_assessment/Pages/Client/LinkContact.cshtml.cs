using bcity_assessment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bcity_assessment.Pages.Client;

public class LinkContact : PageModel
{
    private readonly ILogger<LinkContact> _logger;
    private readonly ClientService _clientService;
    private readonly ContactService _contactService;
    
    public LinkContact(ILogger<LinkContact> logger, ClientService clientService, ContactService contactService)
    {
        _logger = logger;
        _clientService = clientService;
        _contactService = contactService;
    }
    
    [BindProperty]
    public bcity_assessment.Client Client { get; set; } = new bcity_assessment.Client();
    
    [BindProperty]
    public List<bcity_assessment.Contact> Contacts { get; set; } = new List<bcity_assessment.Contact>();
    
    [BindProperty]
    public int ContactId { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public int ClientId { get; set; }

    public void OnGet()//int clientId)
    {
      Client =  _clientService.GetClient(ClientId).Result;
      //Contacts = [];
        
      Contacts = _contactService.GetContacts().Result;
      
    }

    public IActionResult OnPost()//int contactId)
    {
        var contact = _contactService.GetContact(ContactId).Result;
        
        if (Client.Contacts.Contains(contact))
        {
            return RedirectToPage("/Client/Client");
        }
        
        _clientService.LinkContactToClient(ClientId, ContactId);
        
        return RedirectToPage("/Client/LinkContact");
    }
}