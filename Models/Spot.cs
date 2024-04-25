public class Spot
{
    public int Id {  get; set; }
    public string? Name {  get; set; } = string.Empty;
    public string? Description {  get; set; } = string.Empty;
    public string? Phone {  get; set; } = string.Empty;
    public string? Country {  get; set; } = string.Empty;
    public string? City {  get; set; } = string.Empty;
    public string? Region { get; set; } = string.Empty;
    public List<User>? users { get; set; } = null!;
}