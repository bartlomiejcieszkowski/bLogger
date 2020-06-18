using System;
using System.Text;
using System.Windows.Forms;

namespace bLogger.Forms
{
	public partial class LogForm : Form
	{
		private Logger logger;
		public LogForm(Logger logger)
		{
			this.logger = logger;
			InitializeComponent();
			Load += LogForm_Load;
		}

		private void LogForm_Load(object sender, EventArgs e)
		{

		}

		private const int MaxEntries = 1024;
		private delegate void LogDelegate(string message);

		public void Log(string message)
		{
			if (logBox.InvokeRequired)
			{
				LogDelegate logDelegate = Log;
				logBox.Invoke(logDelegate, message);
			}
			else
			{
				logBox.Items.Insert(0, message);
				if (logBox.Items.Count > MaxEntries)
				{
					logBox.Items.RemoveAt(logBox.Items.Count - 1);
				}

				//logBox.SelectedIndices
				//logBox.SelectedIndex = 0;
				logBox.ClearSelected();
			}
		}

		/*
		private void DpiChangedHandler(object sender, DpiChangedEventArgs e)
		{
			HandleDpiChange(e.DeviceDpiNew, e.DeviceDpiOld);
			e.Cancel = true;
		}

		public void HandleDpiChange(int newDpi, int oldDpi)
		{
			//Font = Font.Resize(scaling); // DONT it will scale windows second time.
			logBox.Font = logBox.Font.Resize(newDpi, oldDpi);
		}
		*/

		private void LogBox_KeyDown(object sender, KeyEventArgs e)
		{
			if ((e.Modifiers == Keys.Control && e.KeyCode == Keys.ControlKey) || (e.Modifiers == Keys.Shift && e.KeyCode == Keys.ShiftKey))
			{
				return;
			}

			if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
			{
				logBox.Visible = false;
				for (int i=0; i<logBox.Items.Count; ++i)
				{
					logBox.SetSelected(i, true);
				}
				logBox.Visible = true;
				return;
			}

			if ((e.Modifiers == Keys.Control && e.KeyCode == Keys.C) || (e.KeyCode == Keys.Apps))
			{
				CopyToClipboard();
				return;
			}
		}

		private void LogBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				return;
			}

			if (e.Button  == MouseButtons.Right)
			{
				CopyToClipboard();
				return;
			}
			logger.Log($"{e.Button}");
		}

		private void CopyToClipboard()
		{
			StringBuilder sb = new StringBuilder(4096);
			int i = 0;
			foreach (var item in logBox.SelectedItems)
			{
				sb.AppendLine(item.ToString());
				++i;
			}

			if (sb.Length == 0)
			{
				return;
			}

			Clipboard.SetText(sb.ToString());
			logger.Log($"{i} log lines in clipboard.");
		}
	}
}
