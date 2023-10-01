using System;

namespace Ex04.Menus.Delegates
{
    public class MainMenu
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

        public void AddSubscriber(MenuItem i_MenuItem)
        {
            i_MenuItem.AfterClicked += Report_Clicked;
        }

        private void Report_Clicked(MenuItem i_MenuItem)
        {
            Console.Clear();
            i_MenuItem.DoInvoke();
            System.Threading.Thread.Sleep(3000);
        }

        public void Show()
        {
            MenuItem currentItem = m_MainMenu;

            while (true)
            {
                Console.Clear();
                currentItem.PrintMenu();
                int userchoice = currentItem.AskForInput();

                if (userchoice == 0 && currentItem.IsMainMenu == true)
                {
                    return;
                }
                else if (userchoice == 0)
                {
                    currentItem = currentItem.GetMenuAbove();
                }
                else if (userchoice >= 1 && userchoice <= currentItem.GetNumbersOfSubMenuItems())
                {
                    MenuItem selectedItem = currentItem.GetItem(userchoice);

                    if (selectedItem.GetNumbersOfSubMenuItems() == 0)
                    {
                        selectedItem.Show();
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