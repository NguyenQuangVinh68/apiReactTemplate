namespace apiReact.Models
{
    public class ParentMenu
    {
        public string pgm_no { set; get; }
        public string pgm_nm { set; get; }
        public string pgm_component { set; get; }
        public string pgm_lbl { set; get; }
        public string pgm_plc { set; get; }
        public List<ChildMenu> items { get; set; } = new List<ChildMenu>();
    }
}
