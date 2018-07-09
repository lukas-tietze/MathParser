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

        public ConsoleReader()
        {
            this.history = new LinkedList<string>();
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
                        this.currentInput.Clear();
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
                    default:
                        //TODO Mehr Spezialkeys abfangen
                        Console.Write(input.KeyChar);
                        break;
                }

            }

            return this.Commit();
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
                return res;
            }

            return string.Empty;
        }
    }
}