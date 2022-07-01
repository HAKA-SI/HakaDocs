using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using API.Interfaces;
using API.Core.Interfaces;

namespace API.Helpers
{
  public class CachedAttribute : Attribute, IAsyncActionFilter
  {
    private readonly int _timeToLiveSeconds;
    public CachedAttribute(int timeToLiveSeconds)
    {
      _timeToLiveSeconds = timeToLiveSeconds;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
      var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

      var cacheKey = GenerateCacheKeyFromRequest(context);
      var cachedResponse = await cacheService.GetCachedResponseAsync(cacheKey);

      if (!string.IsNullOrEmpty(cachedResponse))
      {
        var contentResult = new ContentResult
        {
          Content = cachedResponse,
          ContentType = "application/json",
          StatusCode = 200
        };

        context.Result = contentResult;

        return;
      }

      var executedContext = await next(); // move to controller

      if (executedContext.Result is OkObjectResult okObjectResult)
      {
        await cacheService.CacheResponseAsync(cacheKey, okObjectResult.Value, TimeSpan.FromDays(_timeToLiveSeconds));
      }
    }

    private string GenerateCacheKeyFromRequest(ActionExecutingContext context)
    {
      var _request = context.HttpContext.Request;

      var subDomain = "";
      string[] fullAddress = context.HttpContext?.Request?.Headers?["Host"].ToString()?.Split('.');
      if (fullAddress != null)
      {
        subDomain = fullAddress[0].ToLower();
        if (subDomain == "localhost:5000" || subDomain == "test2")
        {
          subDomain = "educnotes";
        }
        else if (subDomain == "test1" || subDomain == "www" || subDomain == "educnotes")
        {
          subDomain = "demo";
        }
      }

      var keyBuilder = new StringBuilder();

      keyBuilder.Append($"{_request.Path}");

      foreach (var (key, value) in _request.Query.OrderBy(x => x.Key))
      {
        keyBuilder.Append($"|{key}-{value}");
      }

      return subDomain + "_" + keyBuilder.ToString();
    }
  }
}