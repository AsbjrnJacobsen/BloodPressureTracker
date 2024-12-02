
namespace Data_and_Models;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class SkipFeatureFlagFilter : Attribute
{
    public string FeatureFlagToSkip { get; }
    public SkipFeatureFlagFilter(string featureFlagToSkip)
    {
        FeatureFlagToSkip = featureFlagToSkip;
    }
}