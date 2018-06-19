namespace XRD_Tool.Properties {
    
    
    // This class allows you to handle specific events on the settings class:
    //  The SettingChanging event is raised before a setting's value is changed.
    //  The PropertyChanged event is raised after a setting's value is changed.
    //  The SettingsLoaded event is raised after the setting values are loaded.
    //  The SettingsSaving event is raised before the setting values are saved.
    internal sealed partial class Settings {
        
        public Settings() {
            // // To add event handlers for saving and changing settings, uncomment the lines below:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
            //

            if (this.PSI_ANGLE == null) 
            {
                this.PSI_ANGLE = new double[] { 0, 15, 30, 45 };
            }
            if (this.X_ANGLE == null)
            {
                this.X_ANGLE = new double[] { 0, 15, 30, 45 };
            }
            if (this.Y_ANGLE == null)
            {
                this.Y_ANGLE = new double[] { 0, 15, 30, 45 };
            }
            if (this.Z_ANGLE == null)
            {
                this.Z_ANGLE = new double[] { 0, 15, 30, 45 };
            }
            if (this.ALPHA_ANGLE == null)
            {
                this.ALPHA_ANGLE = new double[] { 0, 15, 30, 45 };
            }
            if (this.BETA_ANGLE == null)
            {
                this.BETA_ANGLE = new double[] { 0, 15, 30, 45 };
            }
        }
        
        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Add code to handle the SettingChangingEvent event here.
        }
        
        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Add code to handle the SettingsSaving event here.
        }
    }
}
