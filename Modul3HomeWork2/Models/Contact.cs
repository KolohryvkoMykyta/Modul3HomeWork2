namespace Modul3HomeWork2.Models
{
    public class Contact : IComparable
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int CompareTo(object? obj)
        {
            if (obj is not Contact contact)
            {
                throw new ArgumentException($"Incorect type of argument {nameof(obj)}");
            }

            int result = Name.CompareTo(contact.Name);

            if (result != 0)
            {
                return result;
            }
            else
            {
                return Surname.CompareTo(contact.Surname);
            }
        }
    }
}
