using API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace API.Attributes;

public class ValidateImageFileAttribute: ServiceFilterAttribute
{
    public ValidateImageFileAttribute(): base(typeof(ValidateImageFileFilter)) { }

}