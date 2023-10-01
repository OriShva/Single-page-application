using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex04.Menus.Interfaces
{
    public class MenuItem
    {
        private const string k_HorizontalDivider = "-----------------------";
        private const string k_Exit = "Exit";
        private const string k_Back = "Back";
        private readonly IDoActionWhenClicked r_Action;
        private readonly ObserverReporter<IMenuClickObserver> r_ClickObservers;
        private readonly List<MenuItem> r_SubMenuItems;
        private string m_Title;
        private MenuItem m_MenuAbove;
        private bool m_IsMainMenu;

        public MenuItem(string i_Title, MainMenu i_Observer, IDoActionWhenClicked i_Action = null)
        {
            this.Title = i_Title;
            this.m_MenuAbove = null;
            this.r_Action = i_Action;
            this.r_SubMenuItems = new List<Menus.Interfaces.MenuItem>();
            this.r_ClickObservers = new ObserverReporter<IMenuClickObserver>();
            this.r_ClickObservers.AddObserver(i_Observer);
        }

        public void AddSubMenu(Menus.Interfaces.MenuItem i_MenuItem)
        {
            r_SubMenuItems.Add(i_MenuItem);
            i_MenuItem.m_MenuAbove = this;
        }

        public void RemoveSubMenu(Menus.Interfaces.MenuItem i_MenuItem)
        {
            r_SubMenuItems.Remove(i_MenuItem);
        }

        internal Menus.Interfaces.MenuItem GetItem(int i_Index)
        {
            return r_SubMenuItems[i_Index - 1];
        }

        internal Menus.Interfaces.MenuItem GetMenuAbove()
        {
            return this.m_MenuAbove;
        }

        internal List<Menus.Interfaces.MenuItem> GetSubMenuItems()
        {
            return r_SubMenuItems;
        }

        internal int GetNumbersOfSubMenuItems()
        {
            return r_SubMenuItems.Count();
        }

        internal string Title
        {
            get
            {
                return m_Title;
            }

            set
            {
                m_Title = value;
            }
        }

        internal IDoActionWhenClicked DoAction
        {
            get
            {
                return r_Action;
            }
        }

        internal bool IsMainMenu
        {
            get
            {
                return m_IsMainMenu;
            }

            set
            {
                m_IsMainMenu = value;
            }
        }

        internal void Show()
        {
            r_ClickObservers.NotifyObservers(this);
        }

        internal void PrintMenu()
        {
            StringBuilder printMenu = new StringBuilder();
            printMenu.AppendLine($"**{m_Title}**");
            printMenu.AppendLine(k_HorizontalDivider);

            for (int i = 0; i < r_SubMenuItems.Count; i++)
            {
                printMenu.AppendLine($"{i + 1} -> {r_SubMenuItems[i].m_Title}");
            }

            printMenu.AppendLine(k_HorizontalDivider);
            Console.WriteLine(printMenu.ToString());
        }

        internal int AskForInput()
        {
            string userInput;
            string returnMessage = IsMainMenu ? k_Exit : k_Back;

            do
            {
                Console.WriteLine(string.Format($"Enter your request: 1 to {r_SubMenuItems.Count} or press 0 to {returnMessage}"));
                userInput = Console.ReadLine();
            }
            while (!IsValidChoice(userInput));

            return int.Parse(userInput);
        }

        internal  bool IsValidChoice(string i_UserInput)
        {
            bool isValid = true;

            if ((!int.TryParse(i_UserInput, out int o_userInputInt)) || o_userInputInt < 0 || o_userInputInt > r_SubMenuItems.Count)
            {
                Console.WriteLine("Invalid choice. No such option in the menu!");
                isValid = false;
            }

            return isValid;
        }
    }
}