using Kiosk.UIManager;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace UIManager
{
    public class UIStateManager
    {

        private Dictionary<UICategory, UserControl> DicUserControl = new Dictionary<UICategory, UserControl>();
        public Stack<UserControl> UIStack = new Stack<UserControl>();

        public void Push(UserControl ctrl)
        {
            UIStack.Push(ctrl);
            SetVisible(ctrl, Visibility.Visible);
        }

        public UserControl Get(UICategory category)
        {
            return DicUserControl[category];
        }

        public void Set(UICategory category, UserControl uc)
        {
            DicUserControl.Add(category, uc);
            uc.Visibility = Visibility.Collapsed;
        }


        /// <summary>
        /// 현재 최상위 유저컨트롤을 꺼내서 보여준다
        /// </summary>
        /// <returns>if true, visible</returns>
        public bool Pop()
        {
            UIStack.Pop();
            UserControl uc = UIStack.Peek();

            if (uc != null)
            {
                return false;
            }

            SetVisible(uc, Visibility.Collapsed);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctrl">user control</param>
        /// <param name="visible">ui Visiblity</param>
        public void SetVisible(UserControl ctrl, Visibility visible)
        {
            ctrl.Visibility = visible;
        }
    }
}
