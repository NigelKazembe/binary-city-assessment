using bcity_assessment.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;


namespace bcity_assessment.Pages.Client;

public class Client : PageModel
{
    
    private readonly ILogger<Client> _logger;
    private readonly ClientCodeService _service;
    private readonly ClientService _clientService;

    public Client(ClientCodeService service, ILogger<Client> logger, ClientService clientService)
    {
        _service = service;
        _logger = logger;
        this._clientService = clientService;
    }
    
    [BindProperty]
    public List<bcity_assessment.Client> Clients { get; set; }
    
    public void OnGet()
    {
        _logger.LogInformation("It works-ish");
        Clients = new List<bcity_assessment.Client>();

        Clients = _clientService.GetClients().Result;
    }
}