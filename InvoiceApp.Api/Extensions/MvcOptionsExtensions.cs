using Microsoft.AspNetCore.Mvc;
using InvoiceApp.Api.Conventions;

namespace InvoiceApp.Api.Extensions;

public static class MvcOptionsExtensions
{
    public static void UsePrefix(this MvcOptions options, string prefix)
        => options.Conventions.Add(new RoutePrefixConvention(new RouteAttribute(prefix)));
}