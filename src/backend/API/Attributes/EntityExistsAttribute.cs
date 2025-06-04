using API.Filters;
using Domain.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Attributes;

public class EntityExistsAttribute<TService, TDto> : TypeFilterAttribute
    where TService : class, IEntityService<TDto>
{
    public EntityExistsAttribute(string paramName = "id") : base(typeof(EntityExistsFilter<TService, TDto>))
    {
        Arguments = new object[] { paramName };
    }
}