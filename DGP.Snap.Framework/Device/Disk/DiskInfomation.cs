using System.IO;

namespace DGP.Snap.Framework.Device.Disk
{
    internal class DiskInfomation
    {
        public string Name { get; set; }
        public string TotalSize { get; set; }
        public string AvailableSize { get; set; }
        public double UsedPersentage { get; set; }

        public static implicit operator DiskInfomation(DriveInfo driveInfo) => new DiskInfomation
        {
            Name = driveInfo.Name.Replace("\\", ""),
            TotalSize = $"{driveInfo.TotalSize / 1024 / 1024 / 1024:#0}G",
            AvailableSize = $"{driveInfo.AvailableFreeSpace / 1024 / 1024 / 1024:#0}G",
            UsedPersentage = (driveInfo.TotalSize - driveInfo.AvailableFreeSpace) * 1.0 / driveInfo.TotalSize
        };
    }
}
