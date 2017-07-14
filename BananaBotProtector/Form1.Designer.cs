namespace BananaBotProtector
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.ControlPanelBox = new System.Windows.Forms.GroupBox();
            this.CounterGrid = new System.Windows.Forms.DataGridView();
            this.address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.packetspert = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SetTimerBtn = new System.Windows.Forms.Button();
            this.TimerNum = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.EnableBtn = new System.Windows.Forms.Button();
            this.Scroller = new System.Windows.Forms.CheckBox();
            this.DisableBtn = new System.Windows.Forms.Button();
            this.AdapterLab = new System.Windows.Forms.Label();
            this.ChangeAdapterBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.TotalSentLab = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ManageFiltersBtn = new System.Windows.Forms.Button();
            this.packetStream = new System.Windows.Forms.GroupBox();
            this.PacketStreamView = new System.Windows.Forms.ListView();
            this.nubmer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sender = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.receiver = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ttl = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.PacketInfoView = new System.Windows.Forms.TreeView();
            this.label4 = new System.Windows.Forms.Label();
            this.ControlPanelBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CounterGrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimerNum)).BeginInit();
            this.packetStream.SuspendLayout();
            this.SuspendLayout();
            // 
            // ControlPanelBox
            // 
            this.ControlPanelBox.Controls.Add(this.label4);
            this.ControlPanelBox.Controls.Add(this.CounterGrid);
            this.ControlPanelBox.Controls.Add(this.groupBox1);
            this.ControlPanelBox.Controls.Add(this.ManageFiltersBtn);
            this.ControlPanelBox.Location = new System.Drawing.Point(12, 12);
            this.ControlPanelBox.Name = "ControlPanelBox";
            this.ControlPanelBox.Size = new System.Drawing.Size(315, 777);
            this.ControlPanelBox.TabIndex = 0;
            this.ControlPanelBox.TabStop = false;
            this.ControlPanelBox.Text = "Панель управления";
            // 
            // CounterGrid
            // 
            this.CounterGrid.AllowUserToAddRows = false;
            this.CounterGrid.AllowUserToDeleteRows = false;
            this.CounterGrid.AllowUserToResizeColumns = false;
            this.CounterGrid.AllowUserToResizeRows = false;
            this.CounterGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.CounterGrid.BackgroundColor = System.Drawing.SystemColors.Control;
            this.CounterGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CounterGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.address,
            this.packetspert});
            this.CounterGrid.Location = new System.Drawing.Point(12, 409);
            this.CounterGrid.Name = "CounterGrid";
            this.CounterGrid.ReadOnly = true;
            this.CounterGrid.RowHeadersVisible = false;
            this.CounterGrid.Size = new System.Drawing.Size(297, 340);
            this.CounterGrid.TabIndex = 12;
            // 
            // address
            // 
            this.address.HeaderText = "Адрес";
            this.address.Name = "address";
            this.address.ReadOnly = true;
            // 
            // packetspert
            // 
            this.packetspert.HeaderText = "Пакетов за t";
            this.packetspert.Name = "packetspert";
            this.packetspert.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SetTimerBtn);
            this.groupBox1.Controls.Add(this.TimerNum);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.EnableBtn);
            this.groupBox1.Controls.Add(this.Scroller);
            this.groupBox1.Controls.Add(this.DisableBtn);
            this.groupBox1.Controls.Add(this.AdapterLab);
            this.groupBox1.Controls.Add(this.ChangeAdapterBtn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TotalSentLab);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(303, 322);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Сниффер";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // SetTimerBtn
            // 
            this.SetTimerBtn.Location = new System.Drawing.Point(261, 275);
            this.SetTimerBtn.Name = "SetTimerBtn";
            this.SetTimerBtn.Size = new System.Drawing.Size(36, 23);
            this.SetTimerBtn.TabIndex = 13;
            this.SetTimerBtn.Text = "ОК";
            this.SetTimerBtn.UseVisualStyleBackColor = true;
            this.SetTimerBtn.Click += new System.EventHandler(this.SetTimerBtn_Click);
            // 
            // TimerNum
            // 
            this.TimerNum.DecimalPlaces = 1;
            this.TimerNum.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.TimerNum.Location = new System.Drawing.Point(150, 275);
            this.TimerNum.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.TimerNum.Name = "TimerNum";
            this.TimerNum.Size = new System.Drawing.Size(105, 20);
            this.TimerNum.TabIndex = 12;
            this.TimerNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 277);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Период обновления (t) (с)";
            // 
            // EnableBtn
            // 
            this.EnableBtn.Location = new System.Drawing.Point(3, 87);
            this.EnableBtn.Name = "EnableBtn";
            this.EnableBtn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.EnableBtn.Size = new System.Drawing.Size(141, 23);
            this.EnableBtn.TabIndex = 7;
            this.EnableBtn.Text = "Старт";
            this.EnableBtn.UseVisualStyleBackColor = true;
            this.EnableBtn.Click += new System.EventHandler(this.EnableBtn_Click);
            // 
            // Scroller
            // 
            this.Scroller.AutoSize = true;
            this.Scroller.Checked = true;
            this.Scroller.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Scroller.Location = new System.Drawing.Point(6, 146);
            this.Scroller.Name = "Scroller";
            this.Scroller.Size = new System.Drawing.Size(179, 17);
            this.Scroller.TabIndex = 10;
            this.Scroller.Text = "Перейти к последнему пакету";
            this.Scroller.UseVisualStyleBackColor = true;
            this.Scroller.CheckedChanged += new System.EventHandler(this.Scroll_CheckedChanged);
            // 
            // DisableBtn
            // 
            this.DisableBtn.Location = new System.Drawing.Point(150, 87);
            this.DisableBtn.Name = "DisableBtn";
            this.DisableBtn.Size = new System.Drawing.Size(147, 23);
            this.DisableBtn.TabIndex = 8;
            this.DisableBtn.Text = "Стоп";
            this.DisableBtn.UseVisualStyleBackColor = true;
            this.DisableBtn.Click += new System.EventHandler(this.DisableBtn_Click);
            // 
            // AdapterLab
            // 
            this.AdapterLab.AutoSize = true;
            this.AdapterLab.Location = new System.Drawing.Point(56, 28);
            this.AdapterLab.Name = "AdapterLab";
            this.AdapterLab.Size = new System.Drawing.Size(0, 13);
            this.AdapterLab.TabIndex = 9;
            // 
            // ChangeAdapterBtn
            // 
            this.ChangeAdapterBtn.Location = new System.Drawing.Point(59, 44);
            this.ChangeAdapterBtn.Name = "ChangeAdapterBtn";
            this.ChangeAdapterBtn.Size = new System.Drawing.Size(104, 23);
            this.ChangeAdapterBtn.TabIndex = 4;
            this.ChangeAdapterBtn.Text = "Сменить";
            this.ChangeAdapterBtn.UseVisualStyleBackColor = true;
            this.ChangeAdapterBtn.Click += new System.EventHandler(this.ChangeAdapterBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Адаптер:";
            // 
            // TotalSentLab
            // 
            this.TotalSentLab.AutoSize = true;
            this.TotalSentLab.Location = new System.Drawing.Point(119, 217);
            this.TotalSentLab.Name = "TotalSentLab";
            this.TotalSentLab.Size = new System.Drawing.Size(13, 13);
            this.TotalSentLab.TabIndex = 3;
            this.TotalSentLab.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 217);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Принято пакетов";
            // 
            // ManageFiltersBtn
            // 
            this.ManageFiltersBtn.Location = new System.Drawing.Point(50, 210);
            this.ManageFiltersBtn.Name = "ManageFiltersBtn";
            this.ManageFiltersBtn.Size = new System.Drawing.Size(165, 23);
            this.ManageFiltersBtn.TabIndex = 1;
            this.ManageFiltersBtn.Text = "Управление фильтрами";
            this.ManageFiltersBtn.UseVisualStyleBackColor = true;
            // 
            // packetStream
            // 
            this.packetStream.Controls.Add(this.PacketStreamView);
            this.packetStream.Controls.Add(this.PacketInfoView);
            this.packetStream.Location = new System.Drawing.Point(333, 12);
            this.packetStream.Name = "packetStream";
            this.packetStream.Size = new System.Drawing.Size(1129, 785);
            this.packetStream.TabIndex = 1;
            this.packetStream.TabStop = false;
            // 
            // PacketStreamView
            // 
            this.PacketStreamView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nubmer,
            this.time,
            this.sender,
            this.receiver,
            this.ttl,
            this.type,
            this.size,
            this.description});
            this.PacketStreamView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.PacketStreamView.FullRowSelect = true;
            this.PacketStreamView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.PacketStreamView.Location = new System.Drawing.Point(6, 0);
            this.PacketStreamView.MultiSelect = false;
            this.PacketStreamView.Name = "PacketStreamView";
            this.PacketStreamView.Size = new System.Drawing.Size(1117, 497);
            this.PacketStreamView.TabIndex = 2;
            this.PacketStreamView.UseCompatibleStateImageBehavior = false;
            this.PacketStreamView.View = System.Windows.Forms.View.Details;
            this.PacketStreamView.SelectedIndexChanged += new System.EventHandler(this.PacketStreamView_SelectedIndexChanged);
            // 
            // nubmer
            // 
            this.nubmer.Text = "Номер";
            this.nubmer.Width = 98;
            // 
            // time
            // 
            this.time.Text = "Время";
            this.time.Width = 108;
            // 
            // sender
            // 
            this.sender.Text = "Отправитель";
            this.sender.Width = 111;
            // 
            // receiver
            // 
            this.receiver.Text = "Получатель";
            this.receiver.Width = 117;
            // 
            // ttl
            // 
            this.ttl.Text = "TTL";
            this.ttl.Width = 46;
            // 
            // type
            // 
            this.type.Text = "Протокол";
            this.type.Width = 65;
            // 
            // size
            // 
            this.size.Text = "Размер";
            this.size.Width = 66;
            // 
            // description
            // 
            this.description.Text = "Описание";
            this.description.Width = 485;
            // 
            // PacketInfoView
            // 
            this.PacketInfoView.LabelEdit = true;
            this.PacketInfoView.Location = new System.Drawing.Point(6, 503);
            this.PacketInfoView.Name = "PacketInfoView";
            this.PacketInfoView.Size = new System.Drawing.Size(1117, 274);
            this.PacketInfoView.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 370);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Монитор HTTP ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 809);
            this.Controls.Add(this.packetStream);
            this.Controls.Add(this.ControlPanelBox);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "BotProtector";
            this.ControlPanelBox.ResumeLayout(false);
            this.ControlPanelBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CounterGrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimerNum)).EndInit();
            this.packetStream.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ControlPanelBox;
        private System.Windows.Forms.Label TotalSentLab;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ManageFiltersBtn;
        private System.Windows.Forms.Button ChangeAdapterBtn;
        private System.Windows.Forms.GroupBox packetStream;
        private System.Windows.Forms.Button EnableBtn;
        private System.Windows.Forms.Button DisableBtn;
        private System.Windows.Forms.Label AdapterLab;
        private System.Windows.Forms.CheckBox Scroller;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown TimerNum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button SetTimerBtn;
        private System.Windows.Forms.TreeView PacketInfoView;
        private System.Windows.Forms.ListView PacketStreamView;
        private System.Windows.Forms.ColumnHeader nubmer;
        private System.Windows.Forms.ColumnHeader time;
        private System.Windows.Forms.ColumnHeader sender;
        private System.Windows.Forms.ColumnHeader receiver;
        private System.Windows.Forms.ColumnHeader ttl;
        private System.Windows.Forms.ColumnHeader type;
        private System.Windows.Forms.ColumnHeader size;
        private System.Windows.Forms.ColumnHeader description;
        private System.Windows.Forms.DataGridView CounterGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn address;
        private System.Windows.Forms.DataGridViewTextBoxColumn packetspert;
        private System.Windows.Forms.Label label4;
    }
}

