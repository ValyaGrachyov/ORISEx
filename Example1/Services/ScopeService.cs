using Example1.IServices;

namespace Example1.Services;

public class ScopeService : IService
{
    public Guid Guid
    {
        get => Guid.NewGuid();
    }
}