using Microsoft.AspNetCore.Mvc;

namespace Volunterio.Domain.Attributes;

public class RouteV1Attribute(
    string template = "[controller]"
) : RouteAttribute($"api/v1/{template}");