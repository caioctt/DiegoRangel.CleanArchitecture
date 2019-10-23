namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Security
{
    public interface IRandomizeProvider
    {
        string GenerateUniqueKey(int maxSize = 30);
    }
}