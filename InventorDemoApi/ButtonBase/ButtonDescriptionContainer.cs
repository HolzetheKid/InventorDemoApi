using Inventor;

namespace InventorDemoApi.ButtonBase
{
    public class ButtonDescriptionContainer
    {
        public string DisplayName { get; set; }
        public string InternalName { get; set; }
        public CommandTypesEnum CommandType { get; set; }
        public string ClientId { get; set; }
        public string Description { get; set; }
        public string Tooltip { get; set; }

        public ButtonDisplayEnum ButtonDisplayType { get; set; }
    }
}