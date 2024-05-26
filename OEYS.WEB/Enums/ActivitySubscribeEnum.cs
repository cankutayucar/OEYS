using System.ComponentModel.DataAnnotations;

namespace OEYS.WEB.Enums
{
    public enum ActivitySubscribeEnum
    {
        [Display(Name = "Beklemede")]
        pending = 0,
        [Display(Name = "Onaylandı")]
        approved = 1,
        [Display(Name = "Reddedildi")]
        rejected = 2
    }
}
