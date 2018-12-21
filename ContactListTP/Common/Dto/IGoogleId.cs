namespace Common.Dto
{
    public interface IGoogleId
    {
        string ResourceName { get; }

        string ETag { get; }
    }
}