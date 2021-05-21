using System;
using Inventor;
using JetBrains.Annotations;

namespace InventorDemoApi.ButtonBase
{

    /// <summary>
    /// taken from the inventor 'SimpleAddIn' Sample
    /// </summary>
    public abstract class Button
    {
        #region Data Members

        private ButtonDefinition buttonDefinition;

        #endregion Data Members

        #region "Properties"

        protected abstract ButtonDescriptionContainer GetButtonDescription();

        public ButtonDefinition ButtonDefinition => buttonDefinition;

        #endregion "Properties"

        #region "Methods"

        protected Button([NotNull] Application application)
        {
            Initialize(application);
        }

        private void Initialize([NotNull] Application app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            var descriptionContainer = GetButtonDescription();

            if (descriptionContainer is IconButtonDescriptorContainer ibdc && ibdc.StandardIcon != null && ibdc.LargeIcon != null)
            {
                buttonDefinition = app.CommandManager.ControlDefinitions.AddButtonDefinition(
                    ibdc.DisplayName,
                    ibdc.InternalName,
                    ibdc.CommandType,
                    ibdc.ClientId,
                    ibdc.Description,
                    ibdc.Tooltip,
                    PictureDispConverter.ToIPictureDisp(ibdc.StandardIcon),
                    PictureDispConverter.ToIPictureDisp(ibdc.LargeIcon),
                    ibdc.ButtonDisplayType);
            }
            else
            {
                buttonDefinition = app.CommandManager.ControlDefinitions.AddButtonDefinition(
                    descriptionContainer.DisplayName,
                    descriptionContainer.InternalName,
                    descriptionContainer.CommandType,
                    descriptionContainer.ClientId,
                    descriptionContainer.Description,
                    descriptionContainer.Tooltip,
                    Type.Missing,
                    Type.Missing,
                    descriptionContainer.ButtonDisplayType);
            }

            buttonDefinition.Enabled = true;
            buttonDefinition.OnExecute += ButtonDefinition_OnExecute;
        }

        protected abstract void ButtonDefinition_OnExecute(NameValueMap context);

        #endregion "Methods"
    }
}