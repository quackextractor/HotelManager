namespace HotelManager;

partial class HlavniOkno
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
        CONNECT = new System.Windows.Forms.Button();
        quit = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // CONNECT
        // 
        CONNECT.Location = new System.Drawing.Point(128, 137);
        CONNECT.Name = "CONNECT";
        CONNECT.Size = new System.Drawing.Size(137, 101);
        CONNECT.TabIndex = 0;
        CONNECT.Text = "connect";
        CONNECT.UseVisualStyleBackColor = true;
        CONNECT.Click += connect_Click;
        // 
        // quit
        // 
        quit.Location = new System.Drawing.Point(324, 137);
        quit.Name = "quit";
        quit.Size = new System.Drawing.Size(137, 101);
        quit.TabIndex = 1;
        quit.Text = "QUIT";
        quit.UseVisualStyleBackColor = true;
        quit.Click += quit_Click;
        // 
        // HlavniOkno
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(quit);
        Controls.Add(CONNECT);
        Text = "Form1";
        Load += HlavniOkno_Load;
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button quit;

    private System.Windows.Forms.Button CONNECT;

    #endregion
}