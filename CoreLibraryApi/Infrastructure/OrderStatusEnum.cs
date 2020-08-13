using System.ComponentModel.DataAnnotations;

namespace CoreLibraryApi.Infrastructure
{
    public enum OrderStatusEnum
    {
        [Display(Name = "Ожидает подтверждения")]
        Waiting = 1,
        [Display(Name = "Одобрен")]
        Accepted = 2,
        [Display(Name = "Возврат")]
        Returned = 3
    }
}
