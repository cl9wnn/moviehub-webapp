using API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class MovieExistsAttribute: ServiceFilterAttribute
{
    public MovieExistsAttribute(): base(typeof(MovieExistsFilter)) { }
}