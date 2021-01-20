namespace DGP.Snap.Framework.Data.Json
{
    internal class WriterContext
    {
        public int Count;
        public bool InArray;
        public bool InObject;
        public bool ExpectingValue;
        public int Padding;
    }
}
