
// описание всего что мы хотим выхватить у пользователя с тг
public class RegistrationState
{
    public long ChatId{  get; set; } 
    public int Step { get; set; } = 0;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Phone {  get; set; } = string.Empty;
    public string Country {  get; set; } = string.Empty;
    public string City {  get; set; } = string.Empty;
    public string SpotName { get; set; } = string.Empty;
    public int PositionId { get; set; } = 0;

}