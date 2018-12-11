namespace Common.Converters
{
    public interface IConverter<T, TV>
//        where T : class
//        where TV : class
    {
        T Convert(TV source);

        TV Convert(T source);
    }
}