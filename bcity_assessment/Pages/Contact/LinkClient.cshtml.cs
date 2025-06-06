using bcity_assessment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bcity_assessment.Pages.Contact;

public class LinkClient : PageModel
{
    private readonly ILogger<LinkClient> _logger;
    private readonly ClientService _clientService;
    private readonly ContactService _contactService;
    
    public LinkClient(ILogger<LinkClient> logger, ClientService clientService, ContactService contactService)
    {
        _logger = logger;
        _clientService = clientService;
        _contactService = contactService;
    }
    
    [BindProperty]
    public bcity_assessment.Contact Contact { get; set; } = new bcity_assessment.Contact();
    
    [BindProperty]
    public List<bcity_assessment.Client> Clients { get; set; } = new List<bcity_assessment.Client>();
    
    [BindProperty]
    public int ClientId { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public int ContactId { get; set; }

    public void OnGet()//int clientId)
    {
        Contact =  _contactService.GetContact(ContactId).Result;
        //Contacts = [];
        
        Clients = _clientService.GetClients().Result;
      
    }

    public IActionResult OnPost()//int contactId)
    {
        var client = _clientService.GetClient(ContactId).Result;
        
        if (Contact.Clients.Contains(client))
        {
            return RedirectToPage("/Contact/Contact");
        }
        
        _contactService.LinkClientToContact(ContactId, ClientId);
        
        return RedirectToPage("/Contact/LinkClient");
    }
    
}