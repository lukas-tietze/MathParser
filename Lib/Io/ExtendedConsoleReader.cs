using System;
using System.Collections.Generic;
using System.Text;

namespace Matheparser.Io
{
    public class ExtendedConsoleReader : ConsoleReader
    {
        private LinkedList<string> history;
        private LinkedListNode<string> currentItem;
        private StringBuilder currentInput;
        private string lastLineClearer;

        private string lineClearer
        {
            get
            {
                if(string.IsNullOrEmpty(this.lastLineClearer) || this.lastLineClearer.Length != Console.BufferWidth - 1)
                {
                    this.lastLineClearer = "\r" + new string(' ', Console.BufferWidth - 1);
                }

                return this.lastLineClearer;
            }
        }

        public ExtendedConsoleReader()
        {
            this.history = new LinkedList<string>();
            this.currentItem = null;
            this.currentInput = new StringBuilder();
            this.lastLineClearer = "\r";
        }

        public event EventHandler<AutoCompleteEventArgs> AutoCompleteRequired;

        public override void Dispose()
        {
            this.history.Clear();
            this.currentInput = null;
            this.currentInput.Clear();
        }

        public override string ReadLine()
        {
            var input = default(ConsoleKeyInfo);
            var stop = false;

            while (!stop)
            {
                input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.Enter:
                        stop = true;
                        break;
                    case ConsoleKey.Delete:
                        this.HandleDelete();
                        break;
                    case ConsoleKey.Backspace:
                        this.HandleBackspace();
                        break;
                    case ConsoleKey.Escape:
                        this.HandleEscape();
                        break;
                    case ConsoleKey.UpArrow:
                        this.HandleMoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        this.HandleMoveDown();
                        break;
                    case ConsoleKey.Tab:
                        this.HandleTab();
                        break;
                    case ConsoleKey.End:
                        this.HandleEnd();
                        break;
                    case ConsoleKey.Help:
                        this.HandleHelp();
                        break;
                    case ConsoleKey.LeftArrow:
                        this.HandleLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        this.HandleRight();
                        break;
                    case ConsoleKey.A:
                    case ConsoleKey.B:
                    case ConsoleKey.C:
                    case ConsoleKey.D:
                    case ConsoleKey.E:
                    case ConsoleKey.F:
                    case ConsoleKey.G:
                    case ConsoleKey.H:
                    case ConsoleKey.I:
                    case ConsoleKey.J:
                    case ConsoleKey.K:
                    case ConsoleKey.L:
                    case ConsoleKey.M:
                    case ConsoleKey.N:
                    case ConsoleKey.O:
                    case ConsoleKey.P:
                    case ConsoleKey.Q:
                    case ConsoleKey.R:
                    case ConsoleKey.S:
                    case ConsoleKey.T:
                    case ConsoleKey.U:
                    case ConsoleKey.V:
                    case ConsoleKey.W:
                    case ConsoleKey.X:
                    case ConsoleKey.Y:
                    case ConsoleKey.Z:
                    case ConsoleKey.Spacebar:
                    case ConsoleKey.Add:
                    case ConsoleKey.Decimal:
                    case ConsoleKey.Divide:
                    case ConsoleKey.Multiply:
                    case ConsoleKey.Separator:
                    case ConsoleKey.Subtract:
                    case ConsoleKey.NumPad0:
                    case ConsoleKey.NumPad1:
                    case ConsoleKey.NumPad2:
                    case ConsoleKey.NumPad3:
                    case ConsoleKey.NumPad4:
                    case ConsoleKey.NumPad5:
                    case ConsoleKey.NumPad6:
                    case ConsoleKey.NumPad7:
                    case ConsoleKey.NumPad8:
                    case ConsoleKey.NumPad9:
                        this.HandleInputChar(input);
                        break;
                    case ConsoleKey.Applications:
                    case ConsoleKey.Attention:
                    case ConsoleKey.BrowserBack:
                    case ConsoleKey.BrowserFavorites:
                    case ConsoleKey.BrowserForward:
                    case ConsoleKey.BrowserHome:
                    case ConsoleKey.BrowserRefresh:
                    case ConsoleKey.BrowserSearch:
                    case ConsoleKey.BrowserStop:
                    case ConsoleKey.Clear:
                    case ConsoleKey.CrSel:
                    case ConsoleKey.D0:
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                    case ConsoleKey.D6:
                    case ConsoleKey.D7:
                    case ConsoleKey.D8:
                    case ConsoleKey.D9:
                    case ConsoleKey.EraseEndOfFile:
                    case ConsoleKey.Execute:
                    case ConsoleKey.ExSel:
                    case ConsoleKey.F1:
                    case ConsoleKey.F2:
                    case ConsoleKey.F3:
                    case ConsoleKey.F4:
                    case ConsoleKey.F5:
                    case ConsoleKey.F6:
                    case ConsoleKey.F7:
                    case ConsoleKey.F8:
                    case ConsoleKey.F9:
                    case ConsoleKey.F10:
                    case ConsoleKey.F11:
                    case ConsoleKey.F12:
                    case ConsoleKey.F13:
                    case ConsoleKey.F14:
                    case ConsoleKey.F15:
                    case ConsoleKey.F16:
                    case ConsoleKey.F17:
                    case ConsoleKey.F18:
                    case ConsoleKey.F19:
                    case ConsoleKey.F20:
                    case ConsoleKey.F21:
                    case ConsoleKey.F22:
                    case ConsoleKey.F23:
                    case ConsoleKey.F24:
                    case ConsoleKey.Home:
                    case ConsoleKey.Insert:
                    case ConsoleKey.LaunchApp1:
                    case ConsoleKey.LaunchApp2:
                    case ConsoleKey.LaunchMail:
                    case ConsoleKey.LaunchMediaSelect:
                    case ConsoleKey.LeftWindows:
                    case ConsoleKey.MediaNext:
                    case ConsoleKey.MediaPlay:
                    case ConsoleKey.MediaPrevious:
                    case ConsoleKey.MediaStop:
                    case ConsoleKey.NoName:
                    case ConsoleKey.Oem1:
                    case ConsoleKey.Oem102:
                    case ConsoleKey.Oem2:
                    case ConsoleKey.Oem3:
                    case ConsoleKey.Oem4:
                    case ConsoleKey.Oem5:
                    case ConsoleKey.Oem6:
                    case ConsoleKey.Oem7:
                    case ConsoleKey.Oem8:
                    case ConsoleKey.OemClear:
                    case ConsoleKey.OemComma:
                    case ConsoleKey.OemMinus:
                    case ConsoleKey.OemPeriod:
                    case ConsoleKey.OemPlus:
                    case ConsoleKey.Pa1:
                    case ConsoleKey.PageDown:
                    case ConsoleKey.PageUp:
                    case ConsoleKey.Pause:
                    case ConsoleKey.Packet:
                    case ConsoleKey.Play:
                    case ConsoleKey.Print:
                    case ConsoleKey.PrintScreen:
                    case ConsoleKey.Process:
                    case ConsoleKey.RightWindows:
                    case ConsoleKey.Select:
                    case ConsoleKey.Sleep:
                    case ConsoleKey.VolumeDown:
                    case ConsoleKey.VolumeMute:
                    case ConsoleKey.VolumeUp:
                    case ConsoleKey.Zoom:
                        this.HandleDefault(input);
                        break;
                    default:
                        throw new NotSupportedException();
                }

            }

            return this.Commit();
        }

        private void HandleDefault(ConsoleKeyInfo input)
        {
        }

        private void HandleInputChar(ConsoleKeyInfo input)
        {
            if ((input.Modifiers & ConsoleModifiers.Alt) == 0 &&
                (input.Modifiers & ConsoleModifiers.Control) == 0)
            {
                Console.Write(input.KeyChar);
                this.currentInput.Append(input.KeyChar);
            }
        }

        private void HandleEscape()
        {
            this.currentInput.Clear();
            Console.Write(this.lineClearer);
        }

        private void HandleRight()
        {
        }

        private void HandleLeft()
        {
        }

        private void HandleHelp()
        {
        }

        private void HandleEnd()
        {
        }

        private void HandleTab()
        {
        }

        private void HandleMoveDown()
        {
        }

        private void HandleMoveUp()
        {
        }

        private void HandleBackspace()
        {
        }

        private void HandleDelete()
        {
        }

        private string Commit()
        {
            if (this.currentInput.Length > 0)
            {
                var res = this.currentInput.ToString();
                this.currentInput.Clear();
                this.history.AddLast(res);
                Console.WriteLine();
                return res;
            }

            return string.Empty;
        }

        public class AutoCompleteEventArgs : EventArgs
        {
            private HashSet<string> suggestions;
            private string input;

            public AutoCompleteEventArgs(string input)
            {
                this.input = input;
                this.suggestions = new HashSet<string>();
            }

            public string Input
            {
                get
                {
                    return this.input;
                }
            }

            public IReadOnlyCollection<string> Suggestions
            {
                get
                {
                    return this.suggestions;
                }
            }

            public void AddSuggestion(string suggestion)
            {
                if (!this.suggestions.Contains(suggestion))
                {
                    this.suggestions.Add(suggestion);
                }
            }
        }
    }
}