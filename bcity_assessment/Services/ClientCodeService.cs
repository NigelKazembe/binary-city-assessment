using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Update;

namespace bcity_assessment.Services;

public class ClientCodeService
{
    private readonly BcityAssessmentContext _context;
    private readonly ILogger<ClientCodeService> _logger;
    private Random _random = new Random();
    private string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public ClientCodeService(BcityAssessmentContext context, ILogger<ClientCodeService> logger)
    {
        this._context = context;
        this._logger = logger;
    }

    public string GenerateClientCode(string clientName)
    {
        var abb = AbbreviateClientCode(clientName);
        
        var like = _context.Clients.Where(c => EF.Functions
            .Like(c.ClientCode, "%"+abb+"%"))
            .ToList();
        
        if (like.Count == 0)
        {
            abb += "001";
        }
        else
        {
            var count = like.Count;
            count++;
            var temp = ""+count;

            while (temp.Length < 3)
            {
                temp = "0" + temp;
            }

            abb += temp;//count.ToString();
        }
        return abb;
    }

    //Code Below can be improved, should also be modified to utilize DB for numeric part
    /*
     * The implementation code below is quite ugly no lie, and can be improved by extracting some of the code to new
     * methods
     */
    private string AbbreviateClientCode(string clientName)
    {
        var client = new List<string>();
        var n = 0;
        var abb = "";
        
        if (clientName.Contains(' '))
        {
            client = clientName.Split(' ').ToList();
            
            while (n < client.Count && n < 3)
            {
                abb += client[n][0].ToString().ToUpper();
                n++;
                //return abb;
            } 
            
            if (client.Count < 3)
            {
                var c = client.Count;
                //abb = clientName.ToUpper();
                while (c < 3)
                {
                    var randomNumber = _random.Next(_chars.Length);
                    abb += _chars[randomNumber].ToString();
                    c++;
                }
            }

        }
        else
        {
            if (clientName.Length >= 3)
            {
                abb += clientName.Substring(0, 3);
                abb = abb.ToUpper();
            }
            else
            {
                var c = clientName.Length;
                abb = clientName.ToUpper();

                while (c < 3)
                {
                    var randomNumber = _random.Next(_chars.Length);
                    abb += _chars[randomNumber].ToString();
                    //abb += "A";
                    c++;
                }
            }
        }

        return abb;
    }
}