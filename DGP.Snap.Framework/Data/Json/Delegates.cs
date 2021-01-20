namespace DGP.Snap.Framework.Data.Json
{
    internal delegate void ExporterFunc(object obj, JsonWriter writer);
    public delegate void ExporterFunc<T>(T obj, JsonWriter writer);

    internal delegate object ImporterFunc(object input);
    public delegate TValue ImporterFunc<TJson, TValue>(TJson input);

    public delegate IJsonWrapper WrapperFactory();
}
