using System;

namespace FileFunnel.Models;

public class FileModel {
    public string Name { get; set; }
    public string Path { get; set; }
    public string Type { get; set; }
    public long Size { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public string Author { get; set; }

    public FileModel(string name, string path, string type, long size, DateTime createdAt, DateTime modifiedAt, string author) {
        Name = name;
        Path = path;
        Type = type;
        Size = size;
        CreatedAt = createdAt;
        ModifiedAt = modifiedAt;
        Author = author;
    }
}