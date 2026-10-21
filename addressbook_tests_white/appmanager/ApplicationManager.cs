using System;
using System.Collections.Generic;
using System.Text;
using TestStack.White;                      //основной namespace, содержит класс Application для запуска приложений
using TestStack.White.UIItems;              //базовые UI-элементы (кнопки, текстовые поля и т.д.)
using TestStack.White.UIItems.WindowItems;  //класс Window для работы с окнами


namespace addressbook_tests_white
{
    public class ApplicationManager
    {
        public static string WINTITLE = "Free Address Book";    //хранит заголовок главного окна тестируемого приложения
        private GroupHelper groupHelper; //Приватное поле — хранит экземпляр хелпера GroupHelper. Инкапсулирован, доступ наружу — только через свойство Groups
        private ContactHelper contactHelper; //Приватное поле — хранит экземпляр хелпера ContactHelper. Инкапсулирован, доступ наружу — только через свойство Contacts

        public ApplicationManager() 
        {
            Application app = Application.Launch
                (@"C:\Users\user\Desktop\FreeAddressBookPortable\AddressBook.exe"); //запускает exe-файл тестируемого приложения.
                                                                                    //White создаёт объект Application, который представляет запущенный процесс
            MainWindow = app.GetWindow(WINTITLE);   //ищет главное окно приложения по заголовку и сохраняет его в свойство MainWindow

            groupHelper = new GroupHelper(this);    //создаёт хелпер для работы с группами, передавая ему ссылку на текущий ApplicationManager
            contactHelper = new ContactHelper(this);    //создаёт хелпер для работы с контактами, передавая ему ссылку на текущий ApplicationManager
        }

        public void Stop()      //Закрывает приложение штатным способом через UI — находит кнопку выхода по её имени и кликает по ней
        {
            MainWindow.Get<Button>("uxExitAddressButton").Click(); 
        }

        public Window MainWindow { get; private set; }  //Публичное свойство с приватным сеттером — то есть снаружи класса можно только читать главное окно, но не менять его напрямую.
                                                        //Устанавливается оно только внутри конструктора.
                                                        //Это даёт тестам доступ к MainWindow, чтобы напрямую искать элементы (кнопки, текстовые поля) внутри теста, если нужно

        public GroupHelper Groups       //предоставляет доступ к приватному полю grouphelper
        {
            get 
            {
                return groupHelper;
            }
        }

        public ContactHelper Contacts       //предоставляет доступ к приватному полю contactHelper
        {
            get
            {
                return contactHelper;
            }
        }
    }
}
