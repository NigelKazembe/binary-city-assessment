using Microsoft.EntityFrameworkCore;

namespace bcity_assessment.Services;

public class ContactService
{
    private readonly BcityAssessmentContext _context;

    public ContactService(BcityAssessmentContext context)
    {
        this._context = context;
    }

    //Implement code to check for duplicate entries before adding since you aren't using a HashSet
    public void Create(Contact contact)
    {
        this._context.Add(contact);
        this._context.SaveChanges();
    }

    public async Task<List<Contact>> GetContacts()
    {
        return await this._context.Contacts.ToListAsync();
    }
    
    public async Task<Contact> GetContact(int id)
    {
        return await this._context.Contacts
            .FirstOrDefaultAsync(c => 
                c.Id == id
            );
    }

    public void LinkClientToContact(int contactId, params int[] clientIds)
    {
        var contact = GetContact(contactId).Result;
        Client client = null;
        
        foreach (var clientId in clientIds)
        {
            client = _context.Clients.FirstOrDefault(c => c.Id == clientId);
            if (client != null)
            {
                client.Contacts.Add(contact);
                contact.Clients.Add(client);
            }
        }
        _context.SaveChanges();
    }
    
   
}