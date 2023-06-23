using System.Text;
using Example1.IServices;
using Example1.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Example1;

public class Index : PageModel
{
    private readonly IService _service;

    public Index(IService  service)
    {
        _service = service;
    }

    public void OnGet()
    {
        
    }
}