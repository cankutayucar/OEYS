using System.ComponentModel.DataAnnotations;

namespace OEYS.WEB.Enums
{
    public enum ActivityEnum
    {
        [Display(Name = "Konser")]
        Konser = 1,
        [Display(Name = "Konferans")]
        Konferans = 2,
        [Display(Name = "Atölye")]
        Atolye = 3,
        [Display(Name = "Sergi")]
        Sergi = 4,
        [Display(Name = "Festival")]
        Festival = 5,
        [Display(Name = "Söyleşi")]
        Soylesi = 6,
        [Display(Name = "Seminer")]
        Seminer = 7,
        [Display(Name = "Diğer")]
        Diger = 8
    }
}
