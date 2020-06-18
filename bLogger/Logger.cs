using bLogger.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bLogger
{
	public class Logger : IDisposable
	{
		private LogForm _logForm;
		private LogFile _logFile;

		public Logger(string logFile)
		{
			_logForm = new LogForm(this);
			_logForm.FormClosing += _LogForm_FormClosing;
			_logFile = new LogFile(logFile);
		}

		private void _LogForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.ApplicationExitCall) return;
			_logForm.Hide();
			e.Cancel = true;
		}

		private delegate void ParamlesDelegate();

		public void Show()
		{
			if (_logForm.InvokeRequired)
			{
				ParamlesDelegate paramlesDelegate = Show;
				_logForm.Invoke(paramlesDelegate);
			}
			else
			{
				if (!_logForm.Visible)
				{
					_logForm.Show();
				}
			}
		}

		public void Log(String message, [CallerMemberName] String memberName = "")
		{
			message = DateTime.Now.ToString("yyyy/M/d HH:mm:ss | ") + memberName + ": " + message;
			_logForm?.Log(message);
			_logFile?.Log(message);
		}

		public void Dispose()
		{
			if (_logForm != null)
			{
				if (_logForm.InvokeRequired)
				{
					ParamlesDelegate paramlesDelegate = Dispose;
					_logForm.Invoke(paramlesDelegate);
				}
				else
				{
					((IDisposable)_logForm).Dispose();
					_logForm = null;
				}
			}

			if (_logFile != null)
			{
				_logFile.Stop();
				_logFile = null;
			}
		}
	}

	internal class LogFile
	{
		private string _logFileName;
		private StreamWriter _streamWriter = null;

		public LogFile(string logFile)
		{
			_logFileName = logFile;
			AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
			Open();
		}

		public void Open()
		{
			Stop();
			_streamWriter = new StreamWriter(_logFileName, false, Encoding.ASCII);
		}

		private void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
		{
			_streamWriter?.WriteLine("EXCEPTION BEGIN");
			_streamWriter?.WriteLine(e.Exception.ToString());
			_streamWriter?.WriteLine("EXCEPTION END");
			_streamWriter?.Flush();
		}

		public void Stop()
		{
			if (_streamWriter != null)
			{
				_streamWriter.Close();
				_streamWriter = null;
			}
		}

		internal void Log(string message)
		{
			_streamWriter?.WriteLine(message);
			_streamWriter?.Flush();
		}

		~LogFile()
		{
			Stop();
		}
	}
}
