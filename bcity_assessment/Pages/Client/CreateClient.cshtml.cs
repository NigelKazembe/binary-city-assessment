using System.ComponentModel.DataAnnotations;
using bcity_assessment.Pages.Contact;
using bcity_assessment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace bcity_assessment.Pages.Client;

public class CreateClient : PageModel
{
    private readonly ILogger<CreateClient> _logger;
    private readonly ClientService _clientService;
    private readonly ClientCodeService _clientCodeService;

    public CreateClient(ILogger<CreateClient> logger, ClientService clientService, ClientCodeService clientCodeService)
    {
        _logger = logger;
        _clientService = clientService;
        _clientCodeService = clientCodeService;
    }
    
    private ClientMapper Mapper { get; set; }
    private bcity_assessment.Client ClienrB { get; set; }
    
    [BindProperty]
    public ClientModel ClientModel { get; set; }
    
    public void OnGet()
    {
        
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        Mapper = new ClientMapper();
       
        ClienrB= new bcity_assessment.Client();
       
        ClienrB = Mapper.Map(ClientModel);
        
        ClienrB.ClientCode = _clientCodeService.GenerateClientCode(ClientModel.FirstName);
       
        _clientService.AddClient(ClienrB);
        
        return RedirectToPage("/Client/Client");
    }
}

public class ClientModel
{
    [Required]
    [MinLength(2)]
    public string FirstName { get; set; }
    
}

public class ClientMapper
{
    public bcity_assessment.Client Map(ClientModel clientMdl)
    {
        bcity_assessment.Client client = new bcity_assessment.Client();
        client.Name = clientMdl.FirstName;
       
        
        return client;
    }
}