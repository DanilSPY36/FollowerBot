public class User
{
    public int Id { get; set; } // id в бд
    public string? tgName { get; set; } = string.Empty; // userName - имя пользлвателя
    public string? tgFirstName { get; set; } = string.Empty; // введенное имя пользлвателя
    public string? tgLastName { get; set; } = string.Empty; // введенная фамилия пользлвателя
    public string? SpotId {  get; set; } = string.Empty; // Id спота
    public string? FirstName {  get; set; } = string.Empty; // имя 
    public string? LastName {  get; set; } = string.Empty; // фамилия
    public string? Phone {  get; set; } = string.Empty; // номер телефона
    public long tgChatId { get; set; } = 0; // идентификатор чата 
    public long tgUserId { get; set; } = 0;
    public string? PositionId {  get; set; } = string.Empty; // должность на споте (управ, бариста, стажер)
    public bool IsAdmin { get; set; } = false;
    public byte AccessUser { get; set; } = 0; // 0 - отказ //  1 - управ  2 - наставник // 3 - бариста // 4 - стажерик 

    public User(int id, string? tgName, string? tgFirstName, string? tgLastName, string? spotId, string? firstName, string? lastName, string? phone, long tgChatId, long tgUserId, string? positionId, byte accessUser)
    {
        Id = id;
        this.tgName = tgName;
        this.tgFirstName = tgFirstName;
        this.tgLastName = tgLastName;
        SpotId = spotId;
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        this.tgChatId = tgChatId;
        this.tgUserId = tgUserId;
        PositionId = positionId;
        AccessUser = 0;
    }
}