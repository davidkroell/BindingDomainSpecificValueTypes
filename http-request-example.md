The samples used inside the presentation for a raw HTTP request and how it's
mapped to a controller method invokation inside ASP.NET

```http request
GET /api/v2/people/Liebherr/IT?olderThan=18&specialization=Dev HTTP/2
Host: www.example.com
User-Agent: dotnet/6.0.202
Accept: application/json
Authorization: Bearer eyJhbGciOiJ....
```


```csharp
[Authorize]
[Route("api/v2")]
public class CompanyController : ControllerBase
{
    [HttpGet]
    [Route("people/{company}/{department}")]
    public IActionResult GetPeopleWithFilter(string company, string department,
        int olderThan, string specialization)
    {
        // business logic here
        return Ok();
    }
}
```
