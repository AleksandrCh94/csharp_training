using System;                               // Подключение основных системных компонентов .NET
using System.Collections.Generic;           // Подключение поддержки работы с коллекциями данных
using System.Linq;                          // Подключение инструментов для удобной работы с коллекциями
using LinqToDB.Mapping;

namespace WebAddressbookTests               // Пространство имен проекта
{
    [Table(Name = "group_list")]
    public class GroupData : IEquatable<GroupData>, 
        IComparable<GroupData>              // Класс-модель, описывающий сущность "Группа контактов"
    {
        public GroupData()                  // Конструктор класса без параметров
        {
        }

        public GroupData(string name)       // Конструктор класса, требующий название группы при её создании
        {
            Name = name;                    // Записываем переданное имя в автоматическое свойство Name
        }

        public bool Equals(GroupData other)             // Метод интерфейса IEquatable для проверки равенства текущей группы с другой группой GroupData
        {
            if (Object.ReferenceEquals(other, null))    // Проверка: если объект для сравнения не инициализирован (равен null)
            {
                return false;                           // Объекты гарантированно не равны
            }
            if (Object.ReferenceEquals(this, other))    // Проверка на идентичность ссылок: ведут ли обе переменные на один и тот же объект в памяти
            {
                return true;                            // Это один и тот же объект, они абсолютно равны
            }
            return Name == other.Name;                  // Бизнес-логика сравнения: группы считаются равными, если у них совпадают названия (Name)
        }

        public override int GetHashCode()       // Переопределение базового метода получения хэш-кода (необходимо для корректного поиска групп в хэш-таблицах и коллекциях)
        {
            return Name.GetHashCode();          // Возвращаем хэш-код строки названия группы
        }

        public override string ToString()       // Переопределение метода приведения к строке (используется NUnit для вывода понятных логов при падении тестов в Assert)
        {
            return "name= " + Name 
                + "\nheader= " + Header 
                + "\nfooter= " + Footer;        // Возвращаем текстовое представление всех свойств группы
        }

        public int CompareTo(GroupData other)           // Метод интерфейса IComparable для реализации правил сортировки списков групп по алфавиту
        {
            if (Object.ReferenceEquals(other, null))    // Проверка: если объект сравнения отсутствует (null)
            {       
                return 1;                               // Текущий объект признается «большим» и при сортировке сдвигается в конец списка
            }
            return Name.CompareTo(other.Name);          // Стандартное лексикографическое (алфавитное) сравнение строк названий двух групп
        }

        [Column(Name = "group_name")]
        public string Name { get; set; }    // Публичное автоматическое свойство для работы с названием группы (поле name)

        [Column(Name = "group_header")]
        public string Header { get; set; }  // Публичное автоматическое свойство для работы с заголовком группы (поле header)

        [Column(Name = "group_footer")]
        public string Footer { get; set; }  // Публичное автоматическое свойство для работы с нижним колонтитулом группы (поле footer)

        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }      // Публичное автоматическое свойство для хранения уникального идентификатора группы с сайта
    
        public static List<GroupData> GetAll()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from g in db.Groups select g).ToList();
            }    
        }

        public List<ContactData> GetContacts()
        {
            using (AddressBookDB db = new AddressBookDB())
            {
                return (from c in db.Contacts
                        from gcr in db.GCR.Where(p => p.GroupId == Id 
                            && p.ContactId == c.Id)
                        select c).Distinct().ToList();
            }
        }
    }
}