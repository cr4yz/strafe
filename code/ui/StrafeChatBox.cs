using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Linq;

namespace Strafe.UI
{
	public partial class StrafeChatBox : Panel
	{

		static StrafeChatBox Current;

		public Panel Canvas { get; protected set; }
		public TextEntry Input { get; protected set; }

		public event Action<Player, string> OnChat;

		public StrafeChatBox()
		{
			Current = this;

			StyleSheet.Load("/ui/StrafeChatBox.scss");

			Canvas = Add.Panel("chat_canvas");

			Input = Add.TextEntry("");
			Input.AddEventListener("onsubmit", () => Submit());
			Input.AddEventListener("onblur", () => Close());
			Input.AcceptsFocus = true;
			Input.AllowEmojiReplace = true;

			Sandbox.Hooks.Chat.OnOpenChat += Open;
		}

		void Open()
		{
			AddClass("open");
			RemoveClass("closed");
			Input.Focus();
		}

		void Close()
		{
			RemoveClass("open");
			AddClass("closed");
			Input.Blur();
		}

		void Submit()
		{
			Close();

			var msg = Input.Text.Trim();
			Input.Text = "";

			if (string.IsNullOrWhiteSpace(msg))
				return;

			Say(msg);
		}

		public void AddEntry(string name, string message, string avatar)
		{
			var toRemove = Canvas.ChildCount > 50 ? Canvas.ChildCount - 50 : 0;
			Canvas.Children.Take(toRemove).ToList().ForEach(x => x.Delete());

			var e = Canvas.AddChild<StrafeChatEntry>();
			//e.SetFirstSibling();
			e.Message.Text = message;
			e.NameLabel.Text = name + ": ";
			e.Avatar.SetTexture(avatar);

			e.SetClass("noname", string.IsNullOrEmpty(name));
			e.SetClass("noavatar", string.IsNullOrEmpty(avatar));

			Canvas.PreferScrollToBottom = true;
		}

		[ClientCmd("chat_add", CanBeCalledFromServer = true)]
		public static void AddChatEntry(string name, string message, string avatar = null)
		{
			Current?.AddEntry(name, message, avatar);

			// Only log clientside if we're not the listen server host
			if (!Global.IsListenServer)
			{
				Log.Info($"{name}: {message}");
			}
		}

		[ClientCmd("chat_addinfo", CanBeCalledFromServer = true)]
		public static void AddInformation(string message, string avatar = null)
		{
			Current?.AddEntry(null, message, avatar);
		}

		[ServerCmd("say")]
		public static void Say(string message)
		{
			Assert.NotNull(ConsoleSystem.Caller);

			// todo - reject more stuff
			if (message.Contains('\n') || message.Contains('\r'))
			{
				return;
			}

			if (message[0] == '/')
			{
				StrafeGame.OnChatCommand(ConsoleSystem.Caller, message);
				return;
			}

			//Log.Info( $"{ConsoleSystem.Caller}: {message}" );
			AddChatEntry(To.Everyone, ConsoleSystem.Caller.Name, message, $"avatar:{ConsoleSystem.Caller.SteamId}");
		}

	}
}
