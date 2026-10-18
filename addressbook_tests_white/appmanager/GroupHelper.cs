using System;
using System.Collections.Generic;
using System.Text;
using TestStack.White;
using TestStack.White.InputDevices;       // для работы с клавиатурой (класс Keyboard)
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;    // для SearchCriteria — поиск элементов по критериям
using TestStack.White.UIItems.TreeItems;  // для работы с деревом (Tree, TreeNode)
using TestStack.White.UIItems.WindowItems;
using TestStack.White.WindowsAPI;         // константы типа KeyboardInput.SpecialKeys
using System.Windows.Automation;          // ControlType — из UI Automation API Windows


namespace addressbook_tests_white
{
    public class GroupHelper : HelperBase
    {
        public static string GROUPWINTITLE = "Group editor";    //Заголовок модального окна редактора групп.
                                                                //Используется, чтобы найти это окно после его открытия

        public GroupHelper(ApplicationManager manager) : base(manager) //Вызывает конструктор базового класса HelperBase, передавая manager.
                                                                       //Сам по себе ничего дополнительного не делает — вся инициализация (this.manager = manager) происходит в HelperBase.
        {
        }

        public List<GroupData> GetGroupList()       //метод превращает содержимое Tree-контрола UI в удобный для теста список
        {
            List<GroupData> list = new List<GroupData>();       //Создаёт пустой список
            Window dialogue = OpenGroupsDialogue();             //открывает диалог групп
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");    //находит элемент управления Tree (дерево) по AutomationId
            TreeNode root = tree.Nodes[0];      //берёт первый корневой узел дерева

            foreach (TreeNode item in root.Nodes)  //перебирает все дочерние узлы корня, то есть сами группы
            {
                list.Add(new GroupData()    //Берёт текст узла (item.Text) — название группы и создаёт объект GroupData с этим именем и добавляет в список
                {
                    Name = item.Text
                });
            }
            CloseGroupsDialogue(dialogue);  //закрывает диалог после того, как данные считаны
            return list;                    //Возвращает собранный список
        }

        public void Add(GroupData newGroup)
        {
            Window dialogue = OpenGroupsDialogue(); //открывает диалог групп

            dialogue.Get<Button>("uxNewAddressButton").Click(); //находит кнопку “Новая группа” (по AutomationId) и кликает — после этого появляется текстовое поле для ввода имени новой группы.

            TextBox textBox = (TextBox) dialogue.Get(SearchCriteria.
                ByControlType(ControlType.Edit));   //ищет элемент управления любого типа Edit (текстовое поле) внутри диалога — без привязки к конкретному AutomationId

            textBox.Enter(newGroup.Name);   //вводит в найденное текстовое поле имя новой группы

            Keyboard.Instance.PressSpecialKey
                (KeyboardInput.SpecialKeys.RETURN);     //Имитирует нажатие клавиши Enter на клавиатуре
            System.Threading.Thread.Sleep(400);         // Ждем, пока Windows обработает нажатие клавиши
            CloseGroupsDialogue(dialogue);              //закрывает диалог групп после добавления
        }

        private Window OpenGroupsDialogue()
        {
            manager.MainWindow.Get<Button>("groupButton").Click();  //находит в главном окне кнопку с AutomationId "groupButton" и кликает по ней
            return manager.MainWindow.ModalWindow(GROUPWINTITLE);   //После клика открывается модальное окно (диалог), и manager.MainWindow.ModalWindow(GROUPWINTITLE)
                                                                    //ищет это окно по заголовку "Group editor" и возвращает его.
        }

        private void CloseGroupsDialogue(Window dialogue)
        {
            dialogue.Get<Button>("uxCloseAddressButton").Click();   //Закрывает диалог групп — ищет кнопку закрытия (uxCloseAddressButton)
                                                                    //внутри переданного окна dialogue и кликает по ней.            
            System.Threading.Thread.Sleep(200);     // Даем окну закрыться плавно, чтобы управление вернулось главному окну
        }

        public void EnsureGroupExists()
        {
            if (GetGroupList().Count == 1)                      // пропускаем группу General
            {
                Add(new GroupData() { Name = "testToDelete" });
            }
        }

        public void Delete(int index)
        {
            Window dialogue = OpenGroupsDialogue(); //открывает диалог групп
            Tree tree = dialogue.Get<Tree>("uxAddressTreeView");    //находит элемент управления Tree (дерево) по AutomationId
            TreeNode root = tree.Nodes[0];

            root.Nodes[index].Select();
            dialogue.Get<Button>("uxDeleteAddressButton").Click();

            Window confirmDialogue = dialogue.ModalWindow("Delete group");
            confirmDialogue.Get<Button>("uxOKAddressButton").Click();

            System.Threading.Thread.Sleep(300); // Ожидание удаления из дерева

            CloseGroupsDialogue(dialogue);  //закрывает диалог групп после добавления
        }
    }
}