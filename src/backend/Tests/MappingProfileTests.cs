using API.Mappings;
using AutoMapper;
using Infrastructure.Database.Mappings;

namespace Tests;

public class MappingProfileTests
{
    private readonly IConfigurationProvider _apiConfiguration;
    private readonly IConfigurationProvider _infrastructureConfiguration;
    private readonly IMapper _apiMapper;
    private readonly IMapper _infrastructureMapper;

    public MappingProfileTests()
    {
        _apiConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ApiMappingProfile>();
        });

        _infrastructureConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<InfrastructureMappingProfile>();
        });

        _apiMapper = _apiConfiguration.CreateMapper();
        _infrastructureMapper = _infrastructureConfiguration.CreateMapper();
    }

    [Fact]
    public void AutoMapper_ApiConfiguration_IsValid()
    {
        _apiConfiguration.AssertConfigurationIsValid();
    }

    [Fact]
    public void AutoMapper_InfrastructureConfiguration_IsValid()
    {
        _infrastructureConfiguration.AssertConfigurationIsValid();
    }
}