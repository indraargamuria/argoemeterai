namespace OpexNow.Api.Models;

public class Customer
{
    public int Id { get; set; }

    public string CustomerCode { get; set; } = "";

    public string Name { get; set; } = "";

    public string PinHash { get; set; } = "";
}