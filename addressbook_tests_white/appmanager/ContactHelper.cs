using System;
using System.Collections.Generic;
using System.Windows.Automation;          // ControlType — из UI Automation API Windows
using TestStack.White;
using TestStack.White.InputDevices;       // для работы с клавиатурой (класс Keyboard)
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;    // для SearchCriteria — поиск элементов по критериям
using TestStack.White.UIItems.TableItems;
using TestStack.White.UIItems.TreeItems;  // для работы с деревом (Tree, TreeNode)
using TestStack.White.UIItems.WindowItems;
using TestStack.White.WindowsAPI;         // константы типа KeyboardInput.SpecialKeys

namespace addressbook_tests_white
{
    public class ContactHelper : HelperBase
    {
        public static string CONTACTEDITORWINTITLE = "Group editor";    //Заголовок модального окна редактора групп.
                                                                //Используется, чтобы найти это окно после его открытия

        public ContactHelper(ApplicationManager manager) : base(manager) //Вызывает конструктор базового класса HelperBase, передавая manager.
                                                                         //Сам по себе ничего дополнительного не делает — вся инициализация (this.manager = manager) происходит в HelperBase.
        {
        }

        public List<GroupData> GetContactList(Window window)
        {
            List<GroupData> list = new List<GroupData>();       //Создаёт пустой список

            Table uxAddressGrid = window.Get<Table>(
                SearchCriteria.ByAutomationId("uxAddressGrid"));

            return list;
        }

        public void Add(ContactData newContact)
        {
            Window dialogue = OpenContactEditor(); //открывает диалог создания контакта

            FillContactForm(newContact, dialogue);

            Keyboard.Instance.PressSpecialKey
                (KeyboardInput.SpecialKeys.RETURN);     //Имитирует нажатие клавиши Enter на клавиатуре
            System.Threading.Thread.Sleep(400);         // Ждем, пока Windows обработает нажатие клавиши
            CommitNewContact(dialogue);                 // Создает новый контакт и закрывает окно
        }

        private Window OpenContactEditor()
        {
            manager.MainWindow.Get<Button>("uxNewAddressButton").Click();   
            return manager.MainWindow.ModalWindow(CONTACTEDITORWINTITLE);

        }

        private static void FillContactForm(ContactData newContact, Window dialogue)
        {
            TextBox firstNameTextBox = dialogue.Get<TextBox>(SearchCriteria.
                            ByAutomationId("ueFirstNameAddressTextBox"));   // Поиск текстового поля по AutomationID            

            // Очистка поля перед вводом (на случай, если там уже есть значение)
            firstNameTextBox.Text = string.Empty;

            // Ввод нового значения
            firstNameTextBox.Text = newContact.FirstName;

            TextBox lastNameTextBox = dialogue.Get<TextBox>(SearchCriteria.
                ByAutomationId("ueLastNameAddressTextBox"));   // Поиск текстового поля по AutomationID            

            // Очистка поля перед вводом (на случай, если там уже есть значение)
            lastNameTextBox.Text = string.Empty;

            // Ввод нового значения
            lastNameTextBox.Text = newContact.LastName;
        }

        private void CommitNewContact(Window dialogue)
        {
            dialogue.Get<Button>("uxSaveAddressButton").Click();   //Закрывает диалог групп — ищет кнопку закрытия (uxCloseAddressButton)
                                                                    //внутри переданного окна dialogue и кликает по ней.            
            System.Threading.Thread.Sleep(200);     // Даем окну закрыться плавно, чтобы управление вернулось главному окну
        }

        public void Delete(int index)
        {
            Window dialogue = OpenContactEditor(); //открывает диалог групп
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");    //находит элемент управления Tree (дерево) по AutomationId
            TreeNode root = tree.Nodes[0];

            root.Nodes[index].Select();
            dialogue.Get<Button>("uxDeleteAddressButton").Click();

            Window confirmDialogue = dialogue.ModalWindow("Delete group");
            confirmDialogue.Get<Button>("uxOKAddressButton").Click();

            System.Threading.Thread.Sleep(300); // Ожидание удаления из дерева

            //закрывает диалог групп после добавления
        }
    }
}