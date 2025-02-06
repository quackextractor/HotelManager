namespace HotelManager.UI;

partial class HlavniOkno
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        CONNECT = new Button();
        quit = new Button();
        btnOpenTestForm = new Button();
        
        SuspendLayout();
        
        // CONNECT
        CONNECT.Location = new Point(128, 137);
        CONNECT.Name = "CONNECT";
        CONNECT.Size = new Size(137, 101);
        CONNECT.TabIndex = 0;
        CONNECT.Text = "connect";
        CONNECT.UseVisualStyleBackColor = true;
        CONNECT.Click += connect_Click;
        
        // quit
        quit.Location = new Point(324, 137);
        quit.Name = "quit";
        quit.Size = new Size(137, 101);
        quit.TabIndex = 1;
        quit.Text = "QUIT";
        quit.UseVisualStyleBackColor = true;
        quit.Click += quit_Click;
        
        // btnOpenTestForm
        btnOpenTestForm.Location = new Point(128, 260);
        btnOpenTestForm.Name = "btnOpenTestForm";
        btnOpenTestForm.Size = new Size(137, 101);
        btnOpenTestForm.TabIndex = 2;
        btnOpenTestForm.Text = "Open Test Form";
        btnOpenTestForm.UseVisualStyleBackColor = true;
        btnOpenTestForm.Enabled = false;  // Initially disabled until connection is established
        btnOpenTestForm.Click += btnOpenTestForm_Click;
        
        // HlavniOkno
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(btnOpenTestForm);
        Controls.Add(quit);
        Controls.Add(CONNECT);
        Name = "HlavniOkno";
        Text = "Hotel Manager";
        Load += HlavniOkno_Load;
        ResumeLayout(false);
    }

    private Button CONNECT;
    private Button quit;
    private Button btnOpenTestForm;
}