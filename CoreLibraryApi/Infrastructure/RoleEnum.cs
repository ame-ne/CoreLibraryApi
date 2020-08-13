using System.ComponentModel.DataAnnotations;

namespace CoreLibraryApi.Infrastructure
{
    public enum RoleEnum
    {
        [Display(Name = "Администратор")]
        Administrator = 1,
        [Display(Name = "Библиотекарь")]
        Librarian = 2,
        [Display(Name = "Кладовщик")]
        Storekeeper = 3,
        [Display(Name = "Читатель")]
        Reader = 4
    }

    //TODO это должно быть тут?
    public static class RoleEnumHepler
    {
        public static RoleEnum GetDefaultRole()
        {
            return RoleEnum.Reader;
        }
    }
}
