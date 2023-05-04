using Modul3HomeWork2.Models;

var book = new ContactBook();

book.AddRangeOfContacts(new Contact[] 
{
    new Contact
    {
        Name = "Микита",
        Surname = "Кологривко",
        Email = "fdadfa",
        Phone = "0636063402"
    },
    new Contact
    {
        Name = "Данило",
        Surname = "Кологривко",
    },
    new Contact
    {
        Name = "Mykyta",
        Surname = "Kolohryvko"
    },
    new Contact
    {
        Name = "12Mykyta",
        Surname = "Kolohryvko"
    }
});

book.AddContact(new Contact() { Name = "Микита", Surname = "", Email = "gfddgdfg", Phone = "0575467790" });

var myContact = book["Микита", "Кологривко"];

var mykytas = book.FilterByName("Микита");

Console.ReadKey();
