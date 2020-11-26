using Hansot_kiosk;
using Hansot_kiosk.Common;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace UIManager
{
    public class UIStateManager
    {
        private Dictionary<UICategory, UserControl> DicUserControl = new Dictionary<UICategory, UserControl>();
        public Stack<UserControl> UIStack = new Stack<UserControl>();

        public UIStateManager()
        {
            App.InitDeleGate += AllPop;
        }

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
            if (DicUserControl.ContainsKey(category))
            {
                DicUserControl.Remove(category);
            }
            DicUserControl.Add(category, uc);
            uc.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// 현재 최상위 유저컨트롤을 꺼내서 삭제한다.
        /// </summary>
        /// <returns>if true, visible</returns>
        public bool Pop()
        {
            UserControl uc = UIStack.Peek();

            if (uc == null)
            {
                return false;
            }

            SetVisible(uc, Visibility.Collapsed);
            UIStack.Pop();
            return true;
        }

        public void AllPop()
        {
            while(UIStack.Count > 1)
            {
                Pop();
            }
        }

        /// <summary>
        /// visible을 조정한다
        /// </summary>
        /// <param name="ctrl">user control</param>
        /// <param name="visible">ui Visiblity</param>
        public void SetVisible(UserControl ctrl, Visibility visible)
        {
            ctrl.Visibility = visible;
        }
    }
}
