namespace WindowsFormsApp1
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
            this.cb_period = new System.Windows.Forms.ComboBox();
            this.bt_time = new System.Windows.Forms.Button();
            this.dtp_periodB = new System.Windows.Forms.DateTimePicker();
            this.dtp_periodE = new System.Windows.Forms.DateTimePicker();
            this.tb_login = new System.Windows.Forms.TextBox();
            this.tb_pass = new System.Windows.Forms.TextBox();
            this.lb_2 = new System.Windows.Forms.Label();
            this.lb_3 = new System.Windows.Forms.Label();
            this.cb_crdt = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_plan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cb_period
            // 
            this.cb_period.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_period.FormattingEnabled = true;
            this.cb_period.Items.AddRange(new object[] {
            "Прошлая неделяя",
            "Текущая неделя",
            "Произвольно"});
            this.cb_period.Location = new System.Drawing.Point(103, 74);
            this.cb_period.Name = "cb_period";
            this.cb_period.Size = new System.Drawing.Size(123, 21);
            this.cb_period.TabIndex = 2;
            this.cb_period.SelectedIndexChanged += new System.EventHandler(this.cb_period_SelectedIndexChanged);
            // 
            // bt_time
            // 
            this.bt_time.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bt_time.Location = new System.Drawing.Point(12, 72);
            this.bt_time.Name = "bt_time";
            this.bt_time.Size = new System.Drawing.Size(85, 23);
            this.bt_time.TabIndex = 3;
            this.bt_time.Text = "Выгрузить";
            this.bt_time.UseVisualStyleBackColor = true;
            this.bt_time.Click += new System.EventHandler(this.bt_run_Click);
            // 
            // dtp_periodB
            // 
            this.dtp_periodB.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_periodB.Location = new System.Drawing.Point(232, 75);
            this.dtp_periodB.Name = "dtp_periodB";
            this.dtp_periodB.Size = new System.Drawing.Size(91, 20);
            this.dtp_periodB.TabIndex = 6;
            // 
            // dtp_periodE
            // 
            this.dtp_periodE.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_periodE.Location = new System.Drawing.Point(329, 75);
            this.dtp_periodE.Name = "dtp_periodE";
            this.dtp_periodE.Size = new System.Drawing.Size(91, 20);
            this.dtp_periodE.TabIndex = 7;
            // 
            // tb_login
            // 
            this.tb_login.Location = new System.Drawing.Point(62, 29);
            this.tb_login.Name = "tb_login";
            this.tb_login.Size = new System.Drawing.Size(100, 20);
            this.tb_login.TabIndex = 10;
            // 
            // tb_pass
            // 
            this.tb_pass.Location = new System.Drawing.Point(226, 27);
            this.tb_pass.Name = "tb_pass";
            this.tb_pass.PasswordChar = '*';
            this.tb_pass.Size = new System.Drawing.Size(100, 20);
            this.tb_pass.TabIndex = 11;
            // 
            // lb_2
            // 
            this.lb_2.AutoSize = true;
            this.lb_2.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_2.Location = new System.Drawing.Point(12, 30);
            this.lb_2.Name = "lb_2";
            this.lb_2.Size = new System.Drawing.Size(44, 17);
            this.lb_2.TabIndex = 12;
            this.lb_2.Text = "Логин";
            // 
            // lb_3
            // 
            this.lb_3.AutoSize = true;
            this.lb_3.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lb_3.Location = new System.Drawing.Point(168, 30);
            this.lb_3.Name = "lb_3";
            this.lb_3.Size = new System.Drawing.Size(52, 17);
            this.lb_3.TabIndex = 13;
            this.lb_3.Text = "Пароль";
            // 
            // cb_crdt
            // 
            this.cb_crdt.AutoSize = true;
            this.cb_crdt.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cb_crdt.Location = new System.Drawing.Point(332, 26);
            this.cb_crdt.Name = "cb_crdt";
            this.cb_crdt.Size = new System.Drawing.Size(87, 21);
            this.cb_crdt.TabIndex = 14;
            this.cb_crdt.Text = "Изменить";
            this.cb_crdt.UseVisualStyleBackColor = true;
            this.cb_crdt.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Авторизация";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "Затраченное время";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 17);
            this.label3.TabIndex = 17;
            this.label3.Text = "Оперативный план";
            // 
            // bt_plan
            // 
            this.bt_plan.Font = new System.Drawing.Font("Open Sans", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bt_plan.Location = new System.Drawing.Point(12, 118);
            this.bt_plan.Name = "bt_plan";
            this.bt_plan.Size = new System.Drawing.Size(85, 23);
            this.bt_plan.TabIndex = 18;
            this.bt_plan.Text = "Выгрузить";
            this.bt_plan.UseVisualStyleBackColor = true;
            this.bt_plan.Click += new System.EventHandler(this.bt_plan_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 152);
            this.Controls.Add(this.bt_plan);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_crdt);
            this.Controls.Add(this.lb_3);
            this.Controls.Add(this.lb_2);
            this.Controls.Add(this.tb_pass);
            this.Controls.Add(this.tb_login);
            this.Controls.Add(this.dtp_periodE);
            this.Controls.Add(this.dtp_periodB);
            this.Controls.Add(this.bt_time);
            this.Controls.Add(this.cb_period);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_period;
        private System.Windows.Forms.Button bt_time;
        private System.Windows.Forms.DateTimePicker dtp_periodB;
        private System.Windows.Forms.DateTimePicker dtp_periodE;
        private System.Windows.Forms.TextBox tb_login;
        private System.Windows.Forms.TextBox tb_pass;
        private System.Windows.Forms.Label lb_2;
        private System.Windows.Forms.Label lb_3;
        private System.Windows.Forms.CheckBox cb_crdt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bt_plan;
    }
}

