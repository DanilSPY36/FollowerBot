﻿public class User
{
    public int Id { get; set; } // id в бд
    public string? Name { get; set; } = string.Empty; // userName - имя пользлвателя
    public string? SpotId {  get; set; } = string.Empty; // Id спота
    public string? firstName {  get; set; } = string.Empty; // имя 
    public string? lastName {  get; set; } = string.Empty; // фамилия
    public long ChatId { get; set; } = 0; // идентификатор чата 
    public long tgUserId { get; set; } = 0;
    public string? Country { get; set; } = string.Empty; // страна 
    public string? City {  get; set; } = string.Empty; // город
    public string? SpotName { get; set; } = string.Empty; // название спота
    public string? Position {  get; set; } = string.Empty; // должность на споте (управ, бариста, стажер)
    public bool IsAdmin { get; set; } = false;
    public byte AccessUser { get; set; } = 0; // 0 - отказ //  1 временный пользлватель (тестировочный) // 2 - супер пользовталеь

    public User(string name, string firstName, string lastName, long chatId, long tgUserId)
    {
        this.Name = name;
        this.firstName = firstName;
        this.lastName = lastName;
        this.ChatId = chatId;
        this.tgUserId = tgUserId;
        this.AccessUser = 0; // 
    }
}