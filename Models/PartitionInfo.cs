public class PartitionInfo
{
    public string PartitionId   { get; private set; }  // e.g. "Disk #1, Partition #0"
    public long   Size          { get; private set; }
    public long   Bootable      { get; private set; }
    public long   Number        { get; private set; }
    public string Type          { get; private set; }  // e.g. "Installable File System"

    public PartitionInfo(string partitionId, long size, long bootable, long number, string type)
    {
        PartitionId = partitionId;
        Size        = size;
        Bootable    = bootable;
        Number      = number;
        Type        = type;
    }
}