namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Services.Session
{
    public interface IUser<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
    }

    public interface IUser : IUser<int>
    {
        
    }
}