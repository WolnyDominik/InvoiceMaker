using AutoMapper;
using InvoiceApp.Domain.Automapper.Profiles;

namespace InvoiceApp.Domain.Automapper;

public static class AutomapperConfiguration
{
    public static IMapper Build()
    {
        var config = new MapperConfiguration(mc =>
            mc.AddProfile(new UserEntityDtoProfile())
        );

        return config.CreateMapper();
    }
}
