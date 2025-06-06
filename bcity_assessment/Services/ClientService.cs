using Microsoft.EntityFrameworkCore;

namespace bcity_assessment.Services;

public class ClientService
{
    private readonly BcityAssessmentContext _context;

    public ClientService(BcityAssessmentContext context)
    {
        this._context = context;
    }

    //Implement code to check for duplicate entries before adding since you aren't using a HashSet
    public void AddClient(Client client)
    {
        _context.Clients.Add(client);
        _context.SaveChanges();
    }

    public Task<List<Client>> GetClients()
    {
        return this._context.Clients.ToListAsync();
    }

    public Task<Client> GetClient(int id)
    {
        return _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
    }

    public void LinkContactToClient(int clientId, params int[] contact)
    {
        var client = GetClient(clientId).Result;
        Contact contact_ = null;
        
        foreach (var contactId in contact)
        {
            contact_ = _context.Contacts.FirstOrDefault(c => c.Id == contactId);
            if (contact_ != null)
            {
                //contact_.Clients.Add(client);
                client.Contacts.Add(contact_);
                _context.SaveChanges();
            }
        }
        
    }
}