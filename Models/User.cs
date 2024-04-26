public class User
{
    public int Id { get; set; } // id в бд
    public string? tgName { get; set; } = string.Empty; // userName - имя пользлвателя
    public string? tgFirstName { get; set; } = string.Empty; // введенное имя пользлвателя
    public string? tgLastName { get; set; } = string.Empty; // введенная фамилия пользлвателя
    public int? SpotId {  get; set; }  // Id спота
    public string? FirstName {  get; set; } = string.Empty; // имя 
    public string? LastName {  get; set; } = string.Empty; // фамилия
    public string? Phone {  get; set; } = string.Empty; // номер телефона
    public long tgChatId { get; set; } = 0; // идентификатор чата 
    public long tgUserId { get; set; } = 0;
    public int? PositionId {  get; set; } // должность на споте (управ, бариста, стажер)
    public bool IsAdmin { get; set; } = false;
    public byte AccessUser { get; set; } = 0; // 0 - отказ //  1 - управ  2 - наставник // 3 - бариста // 4 - стажерик 

}