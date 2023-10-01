using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex04.Menus.Delegates
{
    public delegate void ClickHandler(MenuItem i_MenuItem);

    public class MenuItem
    {
        private const string k_HorizontalDivider = "-----------------------";
        private const string k_Exit = "Exit";
        private const string k_Back = "Back";
        private readonly List<MenuItem> r_SubMenuItems;
        private string m_Title;
        private MenuItem m_MenuAbove;
        private Action<MenuItem> m_Action;
        private bool m_IsMainMenu;

        public event ClickHandler m_ItemWasClicked;

        public MenuItem(string i_Title, MainMenu i_MainMenu)
        {
            this.Title = i_Title;
            this.m_MenuAbove = null;
            this.r_SubMenuItems = new List<MenuItem>();
            i_MainMenu.AddSubscriber(this);
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

        public void AddSubMenu(MenuItem i_MenuItem)
        {
            if (m_Action != null)
            {
                throw new Exception("subMenu can't hold Action!");
            }
            else
            {
                r_SubMenuItems.Add(i_MenuItem);
                i_MenuItem.m_MenuAbove = this;
            }
        }

        public void RemoveSubMenu(MenuItem i_MenuItem)
        {
            r_SubMenuItems.Remove(i_MenuItem);
        }

        internal MenuItem GetItem(int i_Index)
        {
            return r_SubMenuItems[i_Index - 1];
        }

        internal MenuItem GetMenuAbove()
        {
            return this.m_MenuAbove;
        }

        internal List<MenuItem> GetSubMenuItems()
        {
            return r_SubMenuItems;
        }

        internal int GetNumbersOfSubMenuItems()
        {
            return r_SubMenuItems.Count();
        }

        internal event ClickHandler AfterClicked
        {
            add
            {
                m_ItemWasClicked += value;
            }

            remove
            {
                m_ItemWasClicked -= value;
            }
        }

        public event Action<MenuItem> Action
        {
            add
            {
                if (r_SubMenuItems.Count != 0)
                {
                    throw new Exception("subMenu can't hold Action!");
                }
                else
                {
                    m_Action += value;
                }
            }

            remove
            {
                m_Action -= value;
            }
        }

        protected virtual void OnClick()
        {
            m_ItemWasClicked?.Invoke(this);
        }

        internal void DoInvoke()
        {
            m_Action?.Invoke(this);
        }

        internal void Show()
        {
            OnClick();
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
            string exitOrBack = IsMainMenu ? k_Exit : k_Back;
            do
            {
                Console.WriteLine(string.Format($"Enter your request: 1 to {r_SubMenuItems.Count} or press 0 to {exitOrBack}"));
                userInput = Console.ReadLine();
            }
            while (!IsValid(userInput));

            return int.Parse(userInput);
        }

        private bool IsValid(string i_UserInput)
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