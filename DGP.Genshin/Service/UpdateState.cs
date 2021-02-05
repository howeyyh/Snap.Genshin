namespace DGP.Genshin.Service
{
    public enum UpdateState
    {
        NeedUpdate = 0,
        IsNewestRelease = 1,
        IsInsiderVersion = 2,
        NotAvailable = 3
    }
}
