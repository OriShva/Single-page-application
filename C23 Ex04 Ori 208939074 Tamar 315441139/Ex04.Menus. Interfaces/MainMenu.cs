using System;

namespace Ex04.Menus.Interfaces
{
    public class MainMenu : IMenuClickObserver
    {
        private MenuItem m_MainMenu;

        public MainMenu(string i_Title)
        {
            m_MainMenu = new MenuItem(i_Title, this) { IsMainMenu = true };
        }

        public MenuItem MainItem
        {
            get
            {
                return m_MainMenu;
            }

            set
            {
                m_MainMenu = value;
            }
        }

        public void AddSubMenu(MenuItem i_MenuItem)
        {
            m_MainMenu.AddSubMenu(i_MenuItem);
        }

        public void RemoveSubMenu(MenuItem i_MenuItem)
        {
            m_MainMenu.RemoveSubMenu(i_MenuItem);
        }

        void IMenuClickObserver.ReportClicked(MenuItem i_MenuItem)
        {
            Console.Clear();
            i_MenuItem.DoAction.DoAction();
        }

        public void Show()
        {
            MenuItem currentItem = m_MainMenu;

            while (true)
            {
                Console.Clear();
                currentItem.PrintMenu();
                int userChoice = currentItem.AskForInput();

                if (userChoice == 0 && currentItem.IsMainMenu)
                {
                    return;
                }
                else if (userChoice == 0)
                {
                    currentItem = currentItem.GetMenuAbove();
                }
                else if (userChoice >= 1 && userChoice <= currentItem.GetNumbersOfSubMenuItems())
                {
                    MenuItem selectedItem = currentItem.GetItem(userChoice);

                    if (selectedItem.GetNumbersOfSubMenuItems() == 0)
                    {
                        selectedItem.Show();
                        System.Threading.Thread.Sleep(3000);
                    }
                    else
                    {
                        currentItem = selectedItem;
                    }
                }
            }
        }
    }
}