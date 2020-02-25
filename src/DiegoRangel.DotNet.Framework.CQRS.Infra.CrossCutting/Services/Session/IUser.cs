namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session
{
    public interface IUser<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }

    public interface IUser : IUser<int>
    {
        
    }
}