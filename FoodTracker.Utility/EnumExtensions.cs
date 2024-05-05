using FoodTracker.Models.IModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodTracker.Utility
{
    static class EnumExtentions
    {
        public static IEnumerable<SelectListItem> ToSelectList<ISelectabl>(this IEnumerable<ISelectable> selectables)
        {
            foreach (ISelectable s in selectables)
            {
                yield return new SelectListItem
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                };
            }
        }
    }
}
