using FeatureHubSDK;
using IO.FeatureHub.SSE.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Data_and_Models;

/// <summary>
/// Since I am using a ServiceFilter, I have setup a Skip Filter too. I have let 2x API calls only require LocationDk = true
/// GetMeasurementsByPatientSSN and GetPatient methods have: [SkipFeatureFlagFilter("isUserDoctor")] applied to them.
/// This in effect means a regular user can use these methods ASLONG as they are in Denmark. This is not a
/// Final Solution to the functionality - it should be handled when login has been created.
/// </summary>


public class FeatureFlagFilter : IActionFilter
{
    private readonly IClientContext _fhub;
    
    public FeatureFlagFilter(IClientContext fhub)
    {
        _fhub = fhub;
        
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        
        var skipCheck = context.ActionDescriptor.EndpointMetadata
            .OfType<SkipFeatureFlagFilter>();
        
        // If LocationDk is NOT Enabled -> Error msg
        if (!_fhub["LocationDk"].IsEnabled)
        {
            context.Result = new BadRequestObjectResult("Access denied: LocationDk is not enabled.");
        }
        // If SkipFeatureFlagFilter true -> Return.
        if (skipCheck.Any(attr => attr.FeatureFlagToSkip == "isUserDoctor"))
        {
            return;    
        }
        // If user is not a Doctor -> Error msg.
        if (!_fhub["isUserDoctor"].IsEnabled)
        {
            context.Result = new BadRequestObjectResult("Access denied: User is not a doctor.");    
        }
        
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        
    }
}