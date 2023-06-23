using Example1.IServices;

namespace Example1.Services;

public class SingletonService :IService
{
    public Guid Guid { get => Guid.NewGuid(); }
}