namespace Models;

public class ImageData
{
    public int? id { get; set; }
    public byte[] data { get; set; }
    public string contentType { get; set; }

    public ImageData() { }

    public ImageData(byte[] data, string contentType)
    {
        this.data = data;
        this.contentType = contentType;
    }
}