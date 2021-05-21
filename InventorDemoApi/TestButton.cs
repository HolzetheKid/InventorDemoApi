using Inventor;
using InventorDemoApi.ButtonBase;
using JetBrains.Annotations;

namespace InventorDemoApi
{
    public class TestButton : Button
    {
        private readonly string addinId;


        public TestButton([NotNull] Application application, string addinId) : base(application)
        {
            this.addinId = addinId;

        }

        protected override ButtonDescriptionContainer GetButtonDescription()
        {
            return new ButtonDescriptionContainer()
            {
                ButtonDisplayType = ButtonDisplayEnum.kNoTextWithIcon,
                CommandType = CommandTypesEnum.kShapeEditCmdType,
                Description = "Description",
                InternalName = "InternalName",
                DisplayName = "MyCreatedButton",
                ClientId = addinId,
                Tooltip = "this should be a ToolTip",
               
            };
        }

        protected override void ButtonDefinition_OnExecute(NameValueMap context)
        {
            var frm = new ShowTextFrm();
            frm.ShowDialog();
        }
    }
}