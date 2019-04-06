namespace ContentApi.Domain.Entities
{
    public interface IStorable
    {
        uint? Id { get; set; }
        string Name { get; set; }
    }
}
