using FRONTTOBACK.Model;
using System.Collections.Generic;

namespace FRONTTOBACK.ViewModel
{
    public class HomeVM
    {
        public List<Slider> Slider { get; set; }
		public SliderContent SliderContent { get; set; }
        public List<Category> Categories { get; set; }
        public List<Product> Products{ get; set; }
    }
}
