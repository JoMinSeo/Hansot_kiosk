namespace Hansot_kiosk.Common
{
    public enum ECategory
    {
        ALL,
        MEATMEAT,
        SET,
        BOWLLUNCHBOX,
        SIDEDISH
    }


    public enum EMessageType
    {
        LOGIN,
        COMMONMESSAGE,
        ORDERINFO
    }

    public enum UICategory
    {
        READY,      // 홈화면
        ORDER,      // 주문화면
        PLACE,      // 식사장소선택
        SEATSELECT, // 좌석선택
        PAYSELECT,  // 결제선택
        PAYCASH,    // 현금결제
        PAYCREDIT,  // 카드결제
        COMPLETE,   // 주문완료
        ADMIN,      // 관리자
        STATISTIC,  // 통계
        MAX,
    }
}