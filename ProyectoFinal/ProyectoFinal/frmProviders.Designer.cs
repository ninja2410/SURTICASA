namespace ProyectoFinal
{
    partial class frmProviders
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProviders));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
            this.ribbonProvider = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.btnNewProvider = new DevExpress.XtraBars.BarButtonItem();
            this.btnSaveChanges = new DevExpress.XtraBars.BarButtonItem();
            this.btnDeleteProvider = new DevExpress.XtraBars.BarButtonItem();
            this.btnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.gridControlProvider = new DevExpress.XtraGrid.GridControl();
            this.gridviewProvider = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonProvider
            // 
            this.ribbonProvider.ExpandCollapseItem.Id = 0;
            this.ribbonProvider.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonProvider.ExpandCollapseItem,
            this.btnNewProvider,
            this.btnSaveChanges,
            this.btnDeleteProvider,
            this.btnRefresh});
            this.ribbonProvider.Location = new System.Drawing.Point(0, 0);
            this.ribbonProvider.MaxItemId = 5;
            this.ribbonProvider.Name = "ribbonProvider";
            this.ribbonProvider.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonProvider.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2007;
            this.ribbonProvider.Size = new System.Drawing.Size(707, 141);
            // 
            // btnNewProvider
            // 
            this.btnNewProvider.Caption = "Nuevo";
            this.btnNewProvider.Glyph = ((System.Drawing.Image)(resources.GetObject("btnNewProvider.Glyph")));
            this.btnNewProvider.Id = 1;
            this.btnNewProvider.Name = "btnNewProvider";
            this.btnNewProvider.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            toolTipTitleItem1.Text = "Nuevo Proveedor";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Agrega un nuevo proveedor al registro.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.btnNewProvider.SuperTip = superToolTip1;
            this.btnNewProvider.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnNewProvider_ItemClick);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Caption = "Guardar Cambios";
            this.btnSaveChanges.Glyph = ((System.Drawing.Image)(resources.GetObject("btnSaveChanges.Glyph")));
            this.btnSaveChanges.Id = 2;
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            toolTipTitleItem2.Text = "Guardar Cambios";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Guarda los cambios hechos en la fila seleccionada.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.btnSaveChanges.SuperTip = superToolTip2;
            this.btnSaveChanges.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSaveChanges_ItemClick);
            // 
            // btnDeleteProvider
            // 
            this.btnDeleteProvider.Caption = "Eliminar";
            this.btnDeleteProvider.Glyph = ((System.Drawing.Image)(resources.GetObject("btnDeleteProvider.Glyph")));
            this.btnDeleteProvider.Id = 3;
            this.btnDeleteProvider.Name = "btnDeleteProvider";
            this.btnDeleteProvider.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            toolTipTitleItem3.Text = "Eliminar";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Elimina el proveedor seleccionado.";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.btnDeleteProvider.SuperTip = superToolTip3;
            this.btnDeleteProvider.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnDeleteProvider_ItemClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Caption = "Actualizar";
            this.btnRefresh.Glyph = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Glyph")));
            this.btnRefresh.Id = 4;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.RibbonStyle = ((DevExpress.XtraBars.Ribbon.RibbonItemStyles)(((DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText) 
            | DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText)));
            toolTipTitleItem4.Text = "Actualizar";
            toolTipItem4.LeftIndent = 6;
            toolTipItem4.Text = "Actualiza la vista de los proveedores que se tienen actualmente en el registro.";
            superToolTip4.Items.Add(toolTipTitleItem4);
            superToolTip4.Items.Add(toolTipItem4);
            this.btnRefresh.SuperTip = superToolTip4;
            this.btnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnRefresh_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Opciones Proveedor";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.AllowTextClipping = false;
            this.ribbonPageGroup1.ItemLinks.Add(this.btnNewProvider);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnSaveChanges);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnDeleteProvider);
            this.ribbonPageGroup1.ItemLinks.Add(this.btnRefresh);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Opciones";
            // 
            // gridControlProvider
            // 
            this.gridControlProvider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlProvider.Location = new System.Drawing.Point(0, 141);
            this.gridControlProvider.LookAndFeel.SkinName = "Visual Studio 2013 Blue";
            this.gridControlProvider.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Office2003;
            this.gridControlProvider.MainView = this.gridviewProvider;
            this.gridControlProvider.MenuManager = this.ribbonProvider;
            this.gridControlProvider.Name = "gridControlProvider";
            this.gridControlProvider.Size = new System.Drawing.Size(707, 265);
            this.gridControlProvider.TabIndex = 1;
            this.gridControlProvider.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridviewProvider});
            // 
            // gridviewProvider
            // 
            this.gridviewProvider.Appearance.Row.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridviewProvider.Appearance.Row.Options.UseFont = true;
            this.gridviewProvider.GridControl = this.gridControlProvider;
            this.gridviewProvider.Name = "gridviewProvider";
            this.gridviewProvider.OptionsFind.AlwaysVisible = true;
            this.gridviewProvider.OptionsFind.FindMode = DevExpress.XtraEditors.FindMode.Always;
            this.gridviewProvider.OptionsView.HeaderFilterButtonShowMode = DevExpress.XtraEditors.Controls.FilterButtonShowMode.Button;
            this.gridviewProvider.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridviewProvider_FocusedRowChanged);
            this.gridviewProvider.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridviewProvider_CellValueChanged);
            // 
            // frmProviders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 406);
            this.Controls.Add(this.gridControlProvider);
            this.Controls.Add(this.ribbonProvider);
            this.Name = "frmProviders";
            this.Text = "Proveedores";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmProviders_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridviewProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonProvider;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem btnNewProvider;
        private DevExpress.XtraBars.BarButtonItem btnSaveChanges;
        private DevExpress.XtraBars.BarButtonItem btnDeleteProvider;
        private DevExpress.XtraGrid.GridControl gridControlProvider;
        private DevExpress.XtraGrid.Views.Grid.GridView gridviewProvider;
        private DevExpress.XtraBars.BarButtonItem btnRefresh;
    }
}