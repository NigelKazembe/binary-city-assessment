namespace bcity_assessment.Services;

public class ClientCodeService
{
    private readonly BcityAssessmentContext _context;
    public ClientCodeService(BcityAssessmentContext context)
    {
        this._context = context;
    }

    public string GenerateClientCode(string clientName)
    {

        var abb = AbbreviateClientCode(clientName);
        
        for (var i = 0; i < 3; i++)
        {
            abb += i.ToString();
        }

        return abb;
    }

    //Code Below can be improved, should also be modified to utilize DB for numeric part
    /*
     * you could potentially do it this way, where you implement code to search if there is any client code that
     * is like/matches the abbreviation, and if so if it is less than 3, then access the last used sequence value to
     * get the new one
     * if equal to 3 and it matches an abb in the db, then if it does, retreive it then access the number at the end and
     * increment it then store the clientcode as a new one for this new client
     */
    private string AbbreviateClientCode(string clientName)
    {
        var client = new List<string>();
        var n = 0;
        var abb = "";
        
        if (clientName.Contains(' '))
        {
            client = clientName.Split(' ').ToList();
            while (n < 3)
            {
                abb += client[n][0].ToString().ToUpper();
                n++;
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
                    abb += "A";
                }
            }
        }

        return abb;
    }
}