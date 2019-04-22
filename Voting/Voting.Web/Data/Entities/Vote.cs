namespace Voting.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Vote: IEntity
    {
        public int Id { get; set; }
    }
}
