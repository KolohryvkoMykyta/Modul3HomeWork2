using System.Collections.Immutable;
using System.Globalization;

namespace Modul3HomeWork2.Models
{
    public class ContactBook
    {
        private const string UknownPartitionName = "#";
        private const string DigitPartitionName = "0-9";
        private readonly string[] _partitionsKeysUkraine = new string[] { "А", "Б", "В", "Г", "Ґ", "Д", "Е", "Є", "Ж", "З", "И", "І", "Ї", "Й", "К", "Л", "М", "Н",
             "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ь", "Ю", "Я", UknownPartitionName, DigitPartitionName };
        private readonly string[] _partitionsKeysEnglish = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R",
             "S", "T", "U", "V", "W", "X", "Y", "Z", UknownPartitionName, DigitPartitionName };
        private readonly string[] _partitionsKeys;
        private readonly IDictionary<string, IList<Contact>> _partitions = new SortedDictionary<string, IList<Contact>>();

        public ContactBook()
        {
            if (CultureInfo.CurrentCulture.ThreeLetterISOLanguageName == "ukr")
            {
                _partitionsKeys = _partitionsKeysUkraine;
            }
            else
            {
                _partitionsKeys = _partitionsKeysEnglish;
            }

            foreach (var key in _partitionsKeys)
            {
                _partitions.Add(key, new List<Contact>());
            }
        }

        public ContactBook(IEnumerable<Contact> contacts) : this()
        {
            AddRangeOfContacts(contacts);
        }

        public Contact? this[string name, string surName]
        {
            get 
            {
                var partition = _partitions[GetPartitionName(name)];

                foreach (var item in partition)
                {
                    if (item.Name == name && item.Surname == surName)
                    {
                        return item;
                    }
                }

                return null;
            }
        }

        public List<Contact> FilterByName(string name)
        {
            var result = new List<Contact>();

            var partition = _partitions[GetPartitionName(name)];

            foreach (var item in partition)
            {
                if (item.Name.StartsWith(name))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public void AddContact(Contact contact)
        {
            var partitionKey = GetPartitionName(contact.Name);
            List<Contact> partition = _partitions[partitionKey].ToList<Contact>();

            partition.Add(contact);

            var partitionArray = partition.ToArray();
            Array.Sort(partitionArray);
            _partitions[partitionKey] = partitionArray;
        }

        public void AddRangeOfContacts(IEnumerable<Contact> contacts)
        {
            foreach (var contact in contacts)
            {
                AddContact(contact);
            }
        }

        private string GetPartitionName(string name)
        {
            if (char.IsDigit(name[0]))
            {
                return DigitPartitionName;
            }

            foreach (var key in _partitionsKeys)
            {
                if (key == char.ToUpper(name[0]).ToString())
                {
                    return key;
                }
            }

            return UknownPartitionName;
        }

        public IImmutableDictionary<string, IList<Contact>> Partitions => _partitions.ToImmutableDictionary();


    }
}
