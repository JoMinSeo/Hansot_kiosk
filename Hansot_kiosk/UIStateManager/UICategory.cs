using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImpactUI
{
    public enum UICategory
    {
        HOME,   // 홈화면
        ORDER,  // 주문화면
        PLACE,  // 식사장소선택
        PAYSELECT, //결제선택
        PAYCASH,        //현금결제
        PAYCREDIT, //카드결제
        COMPLETE,  //주문완료
        MAX,
    }
}
