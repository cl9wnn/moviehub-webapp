using API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ActorExistsAttribute: ServiceFilterAttribute
{
    public ActorExistsAttribute(): base(typeof(ActorExistsFilter)) { }

}