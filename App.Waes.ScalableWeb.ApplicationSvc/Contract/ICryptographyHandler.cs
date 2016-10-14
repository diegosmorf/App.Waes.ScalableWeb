namespace App.Waes.ScalableWeb.Application.Contract
{
    public interface ICryptographyHandler
    {
        string Encode(string content);
        string Decode(string content);
    }
}